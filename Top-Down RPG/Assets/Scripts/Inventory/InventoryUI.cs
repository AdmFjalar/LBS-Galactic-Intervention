using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent; //The parent of all the item slots
    public GameObject inventoryUI; //The inventory window gameobject

    public TextMeshProUGUI junkValueText; //The text in shops to display value of all junk items
    public int junkValue; //Int representing the value of all junk in inventory

    Inventory inventory; //The instance of the player inventory

    InventorySlot[] slots; //All the item slots in the inventory

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance; //Sets the inventory to the instance of the player inventory
        inventory.onItemChangedCallback += UpdateUI; //Calls UpdateUI every time onItemChangedCallback is invoked

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); //Sets the slots to all the childobjects with the inventoryslot script under the itemsparent gameobject
        UpdateUI(); //Updates the inventory UI
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt("InPauseMenu") == 0) //Checks if the player is not in the pause menu
        {
            if (Input.GetButtonDown("Inventory") && PlayerPrefs.GetInt("InDialogue") == 0 && PlayerPrefs.GetInt("InSkillsMenu") == 0) //Checks if the button to open the inventory is pressed and if the player is not in a dialogue and if the playerr is not in the skills menu
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf); //Changes the inventory window's active/inactive status to its own inverse
            }

            if (inventoryUI.activeSelf == true) //Checks if the inventory is open
            {
                PlayerPrefs.SetInt("InInventory", 1); //Sets that the inventory is open (Used for other UI scripts)
            }
            else
            {
                PlayerPrefs.SetInt("InInventory", 0); //Sets that the inventory is closed (Used for other UI scripts)
            }
        }
	}

    private void LateUpdate()
    {
        if (PlayerPrefs.GetInt("InPauseMenu") == 0) //Checks if the player is not in the pause menu
        {
            junkValueText.text = "Sell All Junk (" + junkValue + " Gold)"; //Changes the junk value text to the value of all junk items in the inventory
        }
    }

    /// <summary>
    /// Used to update the inventory UI and show all new items and remove all items that has been removed from the inventory UI
    /// </summary>
    void UpdateUI()
    {
        junkValue = 0; //Sets the value of the junkvalue to 0
        for (int i = 0; i < slots.Length; i++) //Goes through all the inventory slots
        {
            if (i < inventory.items.Count) //Checks if it current slot index is less than the amount of items in the inventory
            {
                slots[i].AddItem(inventory.items[i]); //Adds the item at that index to the item slot at that index
                if (inventory.items[i].isJunk == true) //Checks if the item is considered junk
                {
                    junkValue += inventory.items[i].price; //Adds the value of the item to the junkvalue
                }
            }
            else
            {
                slots[i].ClearSlot(); //Clears the slot at the index
            }
        }
    }
}
