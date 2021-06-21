using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

    public PlayerStats instance; //The instance of the playerstats that is easily accessible from other scripts

    public int gold; //The player's gold

    public LoadScene sceneLoader; //Sceneloader that fades in and out from black when changing scenes

    // Use this for initialization
    void Start () {
        instance = this; //Sets the instance to this
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged; //Calls OnEquipmentChanged every time onEquipmentChanged is invoked
	}

    /// <summary>
    /// Loads the respawn scene
    /// </summary>
    public override void FinishDeath()
    {
        base.FinishDeath(); //The base finishdeath

        //sceneLoader.gameObject.SetActive(true);
        sceneLoader.LoadRespawn(); //Loads the respawn scene
    }

    /// <summary>
    /// Changes the armor and damage modifier when changing equipment
    /// </summary>
    /// <param name="newItem">Item to equip</param>
    /// <param name="oldItem">Item to unequip</param>
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null) //Checks if the new item is not null
        {
            armor.AddModifier(newItem.armorModifier); //Adds an armour modifier to the player
            damage.AddModifier(newItem.damageModifier); //Adds a damage modifier to the player
        }

        if (oldItem != null) //Checks if the old item is not null
        {
            armor.RemoveModifier(oldItem.armorModifier); //Removes the modifier given from the item
            damage.RemoveModifier(oldItem.damageModifier); //Removes the modifier given from the item
        }
    }

    /// <summary>
    /// Increases the player's damage modifier
    /// </summary>
    public void IncreaseDamage()
    {
        if (skillPoints >= 1) //Checks if the player has enough skillpoints
        {
            skillPoints -= 1; //Removes a skillpoint
            damageModifier++; //Adds 1 to the damagemodifier
            damage.AddModifier(1); //Adds 1 to the damagemodifier
        }
    }

    /// <summary>
    /// Increases the player's health modifier
    /// </summary>
    public void IncreaseHealth()
    {
        if (skillPoints >= 1) //Checks if the player has enough skillpoints
        {
            skillPoints -= 1; //Removes a skillpoint
            healthModifier++; //Adds 1 to the health modifier
        }
    }

    /// <summary>
    /// Increases the player's mana modifier
    /// </summary>
    public void IncreaseMana()
    {
        if (skillPoints >= 1) //Checks if the player has enough skillpoints
        {
            skillPoints -= 1; //Removes a skillpoint
            manaModifier++; //Adds 1 to the mana modifier
        }
    }
}
