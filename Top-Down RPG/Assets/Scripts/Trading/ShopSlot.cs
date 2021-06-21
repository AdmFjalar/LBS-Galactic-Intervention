using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour {

    public Image icon; //The icon of the item in the slot
    public int price; //The price of the item in the slot
    public string itemName; //The name of the item in the slot

    public Inventory inventory; //The player's inventory

    Item item; //The item in the slot

    public TextMeshProUGUI priceText; //The text for the price
    public TextMeshProUGUI nameText; //The text for the name

    private void LateUpdate()
    {
        if (priceText != null && nameText != null) //Checks if the pricetext and nametext is not null
        {
            if (item != null) //Checks if the item is not null
            {
                priceText.text = "Cost: " + price; //Sets the pricetext to the price of the item
                nameText.text = "" + itemName; //Sets the nametext to the name of the item
            }
            else if (item == null) //Checks if the item is null
            {
                priceText.text = ""; //Empties the pricetext
                nameText.text = "Empty, come back later!"; //Tells the player to come back when the store is restocked
            }
        }
    }

    /// <summary>
    /// Adds an item to the slot
    /// </summary>
    /// <param name="newItem">Item to add</param>
    public void AddItem (Item newItem)
    {
        item = newItem; //Sets the equipped item to the new item

        itemName = item.name; //Sets the item name to the name of the new item
        price = item.price; //Sets the price to the price of the new item

        icon.sprite = item.icon; //Sets the icon of the item to the icon of the new item
        icon.enabled = true; //Enables the icon
    }

    /// <summary>
    /// Removes any item in the slot
    /// </summary>
    public void ClearSlot ()
    {
        item = null; //Removes the item

        price = 0; //Sets the price to 0

        icon.sprite = null; //Removes the icon
        icon.enabled = false; //Disables the icon
    }

    /// <summary>
    /// Tries to buy the item and add it to the player's inventory
    /// </summary>
    public void PurchaseItem ()
    {
        if (GameManager.instance.playerStats.gold >= price && inventory.items.Count < inventory.space && item != null) //Checks if the player can afford the item and if the player has enough space and if the item is not null
        {
            GameManager.instance.playerStats.gold -= price; //Removes the price of the item from the player's gold
            inventory.Add(item); //Adds the item to the player's inventory
            ClearSlot(); //Clears the slot
        }
        else if (GameManager.instance.playerStats.gold < price) //Checks if the player can't afford the item
        {
            Debug.Log("Not enough gold!");
        }
        else if (inventory.items.Count >= inventory.space) //Checks if the player doesn't have enough space for the item
        {
            Debug.Log("Not enough room in inventory!");
        }
    }
}
