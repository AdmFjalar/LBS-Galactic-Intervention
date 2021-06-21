using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public static ShopManager instance; //The instance of the shopmanager which is easily accessible from other scripts

    #region Singelton
    private void Awake()
    {
        if (instance != null) //Checks if the instance is not null
        {
            Debug.LogWarning("More than one instance of shop!");
            return;
        }

        instance = this; //Sets the instance to this
    }
    #endregion

    public delegate void OnShopChanged(); //Used when updating the shop UI
    public OnShopChanged onShopChangedCallback; //The instance of the OnShopChanged

    public int space = 5; //The space in the shop

    public List<Item> items = new List<Item>(); //The items in the shop

    /// <summary>
    /// Adds the item to the shop
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <returns>Whether the item was successfully added or not</returns>
    public bool Add (Item item)
    {
        if (!item.isDefault) //Checks if the item is not default
        {
            if (items.Count >= space) //Checks if there is not enough space in the shop
            {
                Debug.Log("Not enough room!");
                return false;
            }

            items.Add(item); //Adds the item to the shop

            if (onShopChangedCallback != null) //Checks if the shop change callback exists
            {
                onShopChangedCallback.Invoke(); //Invokes the shop change callback
            }
        }
        return true;
    }

    /// <summary>
    /// Removes item from the store
    /// </summary>
    /// <param name="item">Item to remove</param>
    public void Remove(Item item)
    {
        items.Remove(item); //Removes the item from the item list

        if (onShopChangedCallback != null) //Checks if the shop change callback exists
        {
            onShopChangedCallback.Invoke(); //Invokes the shop change callback
        }
    }
}
