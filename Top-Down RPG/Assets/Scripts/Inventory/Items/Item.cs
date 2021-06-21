using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //Makes it so items can be created from the asset menu
public class Item : ScriptableObject
{
    new public string name = "New Item"; //The name of the item
    public Sprite icon = null; //The icon of the item
    public bool isDefault = false; //Bool representing if the item is default
    public bool isJunk = false; //Bool representing if the item is considered junk
    public bool readable = false; //Bool representing if the item is readable
    public int price = 25; //Price for selling and buying the item

    public Dialogue dialogue; //Dialogue for readable items (tomes, etc)

    public GameObject Player; //The player gameobject
    public GameObject DialogueHolder; //Prefab holding the dialogue trigger in which the dialogue will be fed for readable items

    CharacterStats playerStats; //The characterstats script on the player gameobject

    public int ManaRes; //The amount of mana the item restores
    public int HealthRes; //The amount of health the item restores
    public int XPRes; //The amount of XP the item gives

    void Start()
    {
        if (Player == null) //Checks if the player is null
        {
            Player = GameObject.FindWithTag("Player"); //Sets the player to the item with the player tag
        }
        if (playerStats == null) //Checks if the playerstats is null
        {
            playerStats = Player.GetComponent<CharacterStats>(); //Sets playerstats to the characterstats component on the player
        }
    }

    void Update()
    {
        if (Player == null) //Checks if the player is null
        {
            Player = GameObject.FindWithTag("Player"); //Sets the player to the item with the player tag
        }
        if (playerStats == null) //Checks if the playerstats is null
        {
            playerStats = Player.GetComponent<CharacterStats>(); //Sets playerstats to the characterstats component on the player
        }
    }

    /// <summary>
    /// Uses the item and gives the items effects and also removes it from the inventory
    /// </summary>
    public virtual void Use ()
    {
        //Use the item

        if (Player == null) //Checks if the player is null
        {
            Player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the player tag
        }
        if (playerStats == null) //Checks if the playerstats are null
        {
            playerStats = Player.GetComponent<CharacterStats>(); //Sets the playerstats to the characterstats component on the player
        }

        if (playerStats.currentMana != playerStats.maxMana) //Checks if the players mana is not equal to the max mana
        {
            if (ManaRes != 0 && ManaRes + playerStats.currentMana <= playerStats.maxMana) //Checks if the mana is not 0 and the current mana plus the mana res is less or equal to max mana
            {
                playerStats.currentMana += ManaRes; //Adds manares to the players mana
                Debug.Log("Using " + name);
                RemoveFromInventory(); //Calls the removefrominventory method
            }
            else if (ManaRes != 0 && ManaRes + playerStats.currentMana > playerStats.maxMana) //Checks if manares is not 0 and the players current mana plus mana res is greater than the players maxmana
            {
                playerStats.currentMana = playerStats.maxMana; //Sets the players mana to the max mana
                Debug.Log("Using " + name);
                RemoveFromInventory(); //Calls the removefrominventory method
            }
        }

        if (XPRes != 0) //Checks if xpres is not 0 
        {
            playerStats.xp += XPRes; //Adds xpres to the players xp
            Debug.Log("Using " + name);
            RemoveFromInventory(); //Calls the removefrominventory method
        }

        if (playerStats.currentHealth != playerStats.maxHealth) //Checks if the players health is not the max health
        {
            if (HealthRes != 0 && HealthRes + playerStats.currentHealth <= playerStats.maxHealth) //Checks if healthres is not 0 and the players health plus health res is less or equal to the max health
            {
                playerStats.currentHealth += HealthRes; //Adds the healthres to the players health
                Debug.Log("Using " + name);
                RemoveFromInventory(); //Calls the removefrominventory method
            }
            else if (HealthRes != 0 && HealthRes + playerStats.currentHealth > playerStats.maxHealth) //Checks if healthres is not 0 and the players health plus healthres is greater than max health
            {
                playerStats.currentHealth = playerStats.maxHealth; //Sets the players health to max health
                Debug.Log("Using " + name);
                RemoveFromInventory(); //Calls the removefrominventory method
            }
        }
    }

    /// <summary>
    /// Removes the item from the inventory
    /// </summary>
    public void RemoveFromInventory ()
    {
        if (readable) //Checks if the item is readable
        {

            GameObject DH = Instantiate(DialogueHolder); //Sets a reference DH to a spawned gameobject, DialogueHolder
            DH.GetComponent<DialogueTrigger>().dialogue = dialogue; //Sets the dialogue of the dialogue holder to the dialogue of the item
        }
        Inventory.instance.Remove(this); //Removes this item from the inventory
    }
}
