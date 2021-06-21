using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")] //Makes it so that equipment can be created from the asset menu
public class Equipment : Item {

    public EquipmentSlot equipSlot; //The slot which the equipment will be equiped to

    public int armorModifier; //How much armor the equipment offers
    public int damageModifier; //How much damage boost the equipment offers

    /// <summary>
    /// Equips the equipment
    /// </summary>
    public override void Use()
    {
        base.Use(); //The base method from the Item class
        EquipmentManager.instance.Equip(this); //Equips the equipment in the equipment manager
        RemoveFromInventory(); //Removes the equipment from the inventory
    }
}

public enum EquipmentSlot {Head = 0, Chest = 1, Legs = 2, Weapon = 3, Shield = 4, Feet = 5 } //The different equipment slots that are available