using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance; //The instance of the equipmentmanager which can easily be accessed by other scripts

    private void Awake()
    {
        instance = this; //Sets the equipment manager instance to this
    }

    #endregion

    public Equipment[] currentEquipment; //The currently equipped equipment

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem); //Used when updating equipment
    public OnEquipmentChanged onEquipmentChanged; //Instance of OnEquipmentChanged

    Inventory inventory; //The player's inventory

    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //The amount of spots where different equipment can be equipped
        currentEquipment = new Equipment[numSlots]; //The array containing all equipped equipment
        inventory = Inventory.instance; //Sets the reference to the inventory to the instance of the inventory

        //GameManager.instance.InsertEquipment(); //Calls the InsertEquipment method from the gamemanager to load all saved equipment
    }

    /// <summary>
    /// Equips item to the respective equipment slot
    /// </summary>
    /// <param name="newItem">Item to equip</param>
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot; //Index of the equipment slot used by the item in the equipslot enum

        Equipment oldItem = null; //Removes old item

        if (currentEquipment[slotIndex] != null) //Checks if the item uses a specific equipment slot
        {
            oldItem = currentEquipment[slotIndex]; //Sets the oldItem to the item currently equipped in the equipment slot
            inventory.Add(oldItem); //Adds the oldItem to the player's inventory
        }

        if (onEquipmentChanged != null) //Checks if the equipment change callback exists
        {
            onEquipmentChanged.Invoke(newItem, oldItem); //Invokes the equipment change callback to update UI
        }

        currentEquipment[slotIndex] = newItem; //Equips the new item in the equipment slot
    }

    /// <summary>
    /// Removes item from the respective equipment slot
    /// </summary>
    /// <param name="slotIndex">Equipslot index of item</param>
    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null) //Checks if the item uses a specific equipment slot
        {
            Equipment oldItem = currentEquipment[slotIndex]; //Sets the oldItem to the item at the slot index
            inventory.Add(oldItem); //Adds the item to the inventory

            if (onEquipmentChanged != null) //Checks if the equipment change callback exists
            {
                onEquipmentChanged.Invoke(null, oldItem); //Invokes the equipment change callback to update UI
            }

            currentEquipment[slotIndex] = null; //Removes the item equipped at the slot index
        }
    }

    /// <summary>
    /// Unequips all equipped items
    /// </summary>
    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++) //Goes through every equipment slot
        {
            Unequip(i); //Unequips item at index
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) //Checks if U is pressed
        {
            UnequipAll(); //Calls the unequipall method
        }
    }
}
