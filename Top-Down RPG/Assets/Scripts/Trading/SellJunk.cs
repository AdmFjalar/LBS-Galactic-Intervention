using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellJunk : MonoBehaviour {

    public Inventory inventory; //The instance of the player inventory

    public int junkValue; //The value of all junk

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance; //Sets the inventory to the instance of the inventory
	}

    /// <summary>
    /// Sells all junk in the player inventory
    /// </summary>
    public void SellAllJunk()
    {
        for (int i = 0; i < inventory.items.Count; i++) //Goes through all the items in the player inventory
        {
            if (inventory.items[i].isJunk == true) //Checks if the item is junk
            {
                GameManager.instance.playerStats.gold += inventory.items[i].price; //Adds the item's value to the player's gold
                inventory.Remove(inventory.items[i]); //Removes the item from the inventory
            }
        }
    }
}
