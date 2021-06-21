using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour {

    public List<Item> items = new List<Item>(); //The items which the trader will sell

    public bool addedItems = false; //Bool representing whether the shopkeeper has added the items yet or not
    bool containsElement = false; //Bool representing whether the shopkeeper's items includes a certain element yet

    List<int> itemIndexes = new List<int>(); //The indexes of the items

    public GameManager instance; //The instance of the gamemanager
    public ShopManager shopManager; //The instance of the shopmanager
    public GameObject npcManagerObj; //The npcManager gameobject
    public NPCManager npcManager; //The npcManager
	
    // Use this for initialization
	void Start () {
        instance = GameManager.instance; //Sets the gamemanager instance to the gamemanager instance
        shopManager = ShopManager.instance; //Sets the shopmanager to the shopmanager instance
        npcManagerObj = GameObject.FindWithTag("NPCManager"); //Sets the npcManager gameobject as the gameobject with the npcmanager tag
        npcManager = npcManagerObj.GetComponent<NPCManager>(); //Sets the npcManager as the npcManager component on the npcManager gameobject
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (addedItems == false && PlayerPrefs.GetInt("InShop") == 1 && shopManager.items.Count < shopManager.space && gameObject == npcManager.NPCInRange && items.Count > 0) //Checks if the shopkeeper has not added the items yet and if the player is in the shop and if the shopmanager has enough space and if the trader is the npc in range of the player and the shopmanager has more than 0 items
        {
            int rand = Random.Range(0, this.items.Count); //Takes a random value between 0 and the amount of items the shopkeeper has
            containsElement = false; //Sets the containsElement to false
            foreach (int i in itemIndexes) //Goes through all the item indexes
            {
                if (rand == i) //Checks if the random value is equal to the index
                {
                    containsElement = true; //Sets the containsElement to true
                    break;
                }
            }

            if (!containsElement) //Checks if the shopmanager does not contain the element
            {
                itemIndexes.Add(rand); //Adds the index to the item indexes
                shopManager.Add(this.items[rand]); //Adds the item to the shopmanager
            }
        }

        if (addedItems == false && PlayerPrefs.GetInt("InShop") == 1 && shopManager.items.Count >= shopManager.space && gameObject == npcManager.NPCInRange) //Checks if the items has been added and if the player is in the shop and if there is not enough space in the shop and if the trader is the npc in range of the player
        {
            addedItems = true; //Sets that the items has been added
        }
        else if (PlayerPrefs.GetInt("InShop") == 0 && gameObject == npcManager.NPCInRange && shopManager.items != null) //Checks if the player is not in the shop and if the trader is the npc in range of the player and if the shopmanager contains any items
        {
            itemIndexes.Clear(); //Empties the item indexes
            addedItems = false; //Sets addedItems to false
            for (int i = 0; i < shopManager.items.Count; i++) //Goes through all the items in the shopmanager
            {
                shopManager.Remove(shopManager.items[i]); //Removes the item at the index from the shopmanager
            }
        }
	}
}
