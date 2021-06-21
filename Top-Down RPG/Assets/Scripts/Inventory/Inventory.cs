using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    public static Inventory instance; //The instance of the inventory which can easily be accessed by other scripts

    #region Singelton
    void Awake()
    {
        if (instance != null) //Checks if the instance does not exist
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }

        instance = this; //Sets the inventory instance to this
    }
    #endregion

    public delegate void OnItemChanged(); //Used when updating items in inventory
    public OnItemChanged onItemChangedCallback; //Instance of OnItemChanged

    public int space = 20; //Represents the amount of items the inventory can fit

    public List<Item> items = new List<Item>(); //The items currently in the player inventory

    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    /// <param name="item">Item to return</param>
    /// <returns>Whether the item could be added or not</returns>
    public bool Add (Item item)
    {
        if (!item.isDefault) //Checks if the item is default
        {
            if (items.Count >= space) //Checks if there is enough space to add item
            {
                Debug.Log("Not enough room!");
                return false;
            }

            items.Add(item); //Adds item to the inventory

            if (onItemChangedCallback != null) //Checks if the item change callback exists
            {
                onItemChangedCallback.Invoke(); //Invokes the item change callback
            }
        }
        return true;
    }

    /// <summary>
    /// Removes the item from the inventory
    /// </summary>
    /// <param name="item">Item to remove</param>
    public void Remove (Item item)
    {
        Debug.Log("Removing " + item.name);
        items.Remove(item); //Removes the item from the inventory

        if (onItemChangedCallback != null) //Checks if the item change callback exists
        {
            onItemChangedCallback.Invoke(); //Invokes the item change callback
        }
    }

}
