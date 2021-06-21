using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon; //Image where the item icon will be displayed
    public Button removeButton; //Button used to remove item
    public Button sellButton; //Button used to sell item

    Item item; //Item equipped in slot

    void Update()
    {
        if (item != null && item.isDefault || item != null && item.name == "Key")
        {
            removeButton.interactable = false;
        }

        if (item!= null && item.name == "Key")
        {
            sellButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("RemovingItem") == 1 && gameObject.tag == "Current") //Checks if item is to be removed and if this is the slot where the item should be removed from
        {
            PlayerPrefs.SetInt("RemovingItem", 0); //Sets that the item no longer should be removed
            gameObject.tag = "Slot"; //Changes back the slots tag to the normal slot tag
            Inventory.instance.Remove(this.item); //Removes the equipped item from the slot
        }
    }

    /// <summary>
    /// Adds item to the slot
    /// </summary>
    /// <param name="newItem">Item to add</param>
    public void AddItem (Item newItem)
    {
        item = newItem; //Changes the equipped item to the item which is to be added

        icon.sprite = item.icon; //Changes the item icon to the icon of the newly equipped item
        icon.enabled = true; //Enables the icon image
        if (newItem.name != "Key")
        {
            removeButton.interactable = true; //Enables the remove button
        }

        if (item.price > 0) //Checks if the item is not free
        {
            sellButton.interactable = true; //Enables the sell button
        }
    }
    
    /// <summary>
    /// Removes equipped item from the slot
    /// </summary>
    public void ClearSlot()
    {
        item = null; //Removes the equipped item
        icon.sprite = null; //Removes the item icon
        icon.enabled = false; //Disables the icon image
        removeButton.interactable = false; //Disables the remove button
        sellButton.interactable = false; //Disables the sell button
    }

    /// <summary>
    /// Sells the equipped item
    /// </summary>
    public void Sell()
    {
            GameManager.instance.playerStats.gold += this.item.price; //Adds the value of the item to the player's gold
            Inventory.instance.Remove(this.item); //Removes the equipped item from the slot
    }

    /// <summary>
    /// Used to change the tag of the slot when item is to be removed
    /// </summary>
    public void OnRemoveButton ()
    {
        gameObject.tag = "Current"; //Changes the tag to current
    }

    /// <summary>
    /// Uses the item and gives effects and/or bonuses of item
    /// </summary>
    public void UseItem ()
    {
        if (item != null) //Checks if there is an equipped item
        {
            item.Use(); //Calls the use method of the item
        }
    }
}
