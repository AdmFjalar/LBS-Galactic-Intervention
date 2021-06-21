using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {

    public Transform slotsParent; //The parent under which all the shop slots are
    public GameObject shopUI; //The UI of the shop

    public ShopManager shopManager; //The shopmanager instance

    ShopSlot[] slots; //The shopslots

    void Start () {
        PlayerPrefs.SetInt("InShop", 0); //Sets that the player is not in the shop
        shopManager = ShopManager.instance; //Sets the shopmanager to the shopmanager instance
        shopManager.onShopChangedCallback += UpdateUI; //Calls the updateUI method every time the shop change callback is invoked

        slots = slotsParent.GetComponentsInChildren<ShopSlot>(); //Sets the slots to the children with the shopslot component under the slotsparent
	}

    private void LateUpdate()
    {
        if (PlayerPrefs.GetInt("InShop") == 1 && Input.GetKeyDown(KeyCode.Escape)) //Checks if the player is in the shop and presses escape
        {
            PlayerPrefs.SetInt("InShop", 0); //Makes the player exit the shop
        }
        if (PlayerPrefs.GetInt("InShop") == 1) //Checks if the player is in the shop
        {
            shopUI.SetActive(true); //Sets the shop window as active
        }
        else if (PlayerPrefs.GetInt("InShop") == 0) //Checks if the player is not in the shop
        {
            shopUI.SetActive(false); //Sets the shop window as inactive
        }
    }

    /// <summary>
    /// Updates the shop UI
    /// </summary>
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) //Goes through all the shop slots
        {
            if (i < shopManager.items.Count) //Checks if the current index is lower than the amount of items in the shopmanager
            {
                slots[i].AddItem(shopManager.items[i]); //Adds the item of the index to the slot
            }
            else
            {
                slots[i].ClearSlot(); //Clears the slot
            }
        }
    }
}
