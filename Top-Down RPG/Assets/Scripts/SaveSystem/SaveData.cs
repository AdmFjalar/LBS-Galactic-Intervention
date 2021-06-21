using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //Makes the class serializable
public class SaveData {

    public string[] toi = new string[12]; //Array of strings representing if the item at the index is a normal item or equipment
    public string[] names = new string[18]; //Array of strings representing the item name at the index
    public byte[][] icons = new byte[18][]; //Array of byte arrays representing the icons of the items
    public bool[] isDefault = new bool[18]; //Array of bools representing if the item is default
    public bool[] isJunk = new bool[18]; //Array of bools representing if the item is junk
    public int[] prices = new int[18]; //Array of ints representing the value of the item at the index
    public int[] manaRes = new int[18]; //Array of ints representing how much mana the item at the index restores
    public int[] healthRes = new int[18]; //Array of ints representing how much health the item at the index restores
    public int[] xpRes = new int[18]; //Array of ints representing how much XP the item at the index gives

    public int[] texWidth = new int[18]; //Int array representing width of sprite textures
    public int[] texHeight = new int[18]; //Int array representing height of sprite textures
    public int[] texMipMapCount = new int[18]; //Int array representing amount of mip maps

    public string[] equipSlot = new string[18]; //String array representing which slot the equipment uses
    public int[] armorModifier = new int[18]; //Int array representing the armor bonus of the equipment
    public int[] damageModifier = new int[18]; //Int array representing the damage bonus of the equipment

    public string[] _names = new string[5]; //Names of attacks
    public int[] drmax = new int[5]; //Max damage of attacks
    public int[] drmin = new int[5]; //Min damage of attacks
    public int[] mCost = new int[5]; //Mana cost of attacks

    public int gold; //Player's amount of gold

    public int numberOfItems; //Amount of items in inventory
    public int numberOfEquipment; //Amount of equipped items
    public int numberOfAttacks; //Amount of equipped attacks
    public int level; //Player's level
    public float[] playerPosition; //Position of player
    public int damageMod; //Damage modifier of player
    public int healthMod; //Health modifier of player
    public int manaMod; //Mana modifier of player
    public int skillPoints; //Player's remaining skillpoints
    public int xp; //Player's XP

    /// <summary>
    /// Constructor of savedata used to save all the relevant information for loading later when reentering game
    /// </summary>
    /// <param name="gameManager">Gamemanager in scene</param>
    public SaveData (GameManager gameManager)
    {
        xp = gameManager.playerStats.xp; //Sets the xp to the player's xp
        level = gameManager.playerStats.level; //Sets the level to the player's level
        gold = gameManager.playerStats.gold; //Sets the gold to the player's gold

        numberOfItems = gameManager.inventory.items.Count; //Sets the number of items to the amount of items in the inventory
        numberOfEquipment = gameManager.equipManager.currentEquipment.Length; //Sets the number of equipment to the amount of equipped items
        numberOfAttacks = gameManager.attackMenu.attacks.Count; //Sets the number of attacks to the amount of equipped attacks

        for (int i = 0; i < gameManager.inventory.items.Count; i++) //Goes through all the items in the inventory
        {
            if (gameManager.inventory.items[i] is Equipment) //Checks if the item at the index is equipment
            {
                toi[i] = "Equipment"; //Sets that it is equipment

                SetEquip(gameManager.inventory.items[i] as Equipment, i); //Feeds the current equipment into the setequip method

                names[i] = gameManager.inventory.items[i].name; //Sets the name at the index to the name of the item
                icons[i] = imageToByteArray(gameManager.inventory.items[i].icon, i); //Sets the icon at the index to the icon of the item
                isDefault[i] = gameManager.inventory.items[i].isDefault; //Sets the isDefault bool at the index to the isDefault bool of the item
                isJunk[i] = gameManager.inventory.items[i].isJunk; //Sets the isJunk bool at the index to the isJunk bool of the item
                prices[i] = gameManager.inventory.items[i].price; //Sets the price at the index to the price of the item
                manaRes[i] = gameManager.inventory.items[i].ManaRes; //Sets the mana res at the index to the mana res of the item
                healthRes[i] = gameManager.inventory.items[i].HealthRes; //Sets the health res at the index to the health res of the item
                xpRes[i] = gameManager.inventory.items[i].XPRes; //Sets the xp res at the index to the xp res of the item
            }
            else if (gameManager.inventory.items[i] is Item) //Checks if the item at the index is a normal item
            {
                toi[i] = "Item"; //Sets that it is a normal item

                names[i] = gameManager.inventory.items[i].name; //Sets the name at the index to the name of the item
                icons[i] = imageToByteArray(gameManager.inventory.items[i].icon, i); //Sets the icon at the index to the icon of the item
                isDefault[i] = gameManager.inventory.items[i].isDefault; //Sets the isDefault bool at the index to the isDefault bool of the item
                isJunk[i] = gameManager.inventory.items[i].isJunk; //Sets the isJunk bool at the index to the isJunk bool of the item
                prices[i] = gameManager.inventory.items[i].price; //Sets the price at the index to the price of the item
                manaRes[i] = gameManager.inventory.items[i].ManaRes; //Sets the mana res at the index to the mana res of the item
                healthRes[i] = gameManager.inventory.items[i].HealthRes; //Sets the health res at the index to the health res of the item
                xpRes[i] = gameManager.inventory.items[i].XPRes; //Sets the xp res at the index to the xp res of the item
            }
        }

        for (int i = 12; i - 12 < gameManager.equipManager.currentEquipment.Length; i++) //Goes through all the equipped items
        {
            if (gameManager.equipManager.currentEquipment[i-12] != null) //Checks if there is an item equipped at the current index
            {
                SetEquip(gameManager.equipManager.currentEquipment[i - 12], i); //Calls the setequip method and feeds in the item

                names[i] = gameManager.equipManager.currentEquipment[i - 12].name; //Sets the name at the index to the name of the item
                icons[i] = imageToByteArray(gameManager.equipManager.currentEquipment[i - 12].icon, i); //Sets the icon at the index to the icon of the item
                isDefault[i] = gameManager.equipManager.currentEquipment[i - 12].isDefault; //Sets the isDefault bool at the index to the isDefault bool of the item
                isJunk[i] = gameManager.equipManager.currentEquipment[i - 12].isJunk; //Sets the isJunk bool at the index to the isJunk bool of the item
                prices[i] = gameManager.equipManager.currentEquipment[i - 12].price; //Sets the price at the index to the price of the item
                manaRes[i] = gameManager.equipManager.currentEquipment[i - 12].ManaRes; //Sets the mana res at the index to the mana res of the item
                healthRes[i] = gameManager.equipManager.currentEquipment[i - 12].HealthRes; //Sets the xp res at the index to the xp res of the item
            }
        }

        for (int i = 0; i < numberOfAttacks; i++) //Goes through all equipped attacks
        {
            _names[i] = gameManager.attackMenu.attacks[i].name; //Sets the name at the index to the name of the attack
            drmax[i] = gameManager.attackMenu.attacks[i].damageRangeMax; //Sets the max damage at the index to the max damage of the attack
            drmin[i] = gameManager.attackMenu.attacks[i].damageRangeMin; //Sets the min damage at the index to the min damage of the attack
            mCost[i] = gameManager.attackMenu.attacks[i].manaCost; //Sets the mana cost at the index to the mana cost of the attack
        } 
        
        damageMod = gameManager.playerStats.damageModifier; //Sets the damagemod to the damagemodifier of the player
        healthMod = gameManager.playerStats.healthModifier; //Sets the healthmod to the healthmodifier of the player
        manaMod = gameManager.playerStats.manaModifier; //Sets the manamod to the manamodifier of the player
        skillPoints = gameManager.playerStats.skillPoints; //Sets the skillpoints to the amount of skillpoints of the player
    }
    
    /// <summary>
    /// Converts sprite texture to a byte array
    /// </summary>
    /// <param name="imageIn">Image to convert</param>
    /// <param name="i">Index of image</param>
    /// <returns></returns>
    public byte[] imageToByteArray(Sprite imageIn, int i)
    {
        var texture = imageIn.texture; //Sets the texture to the texture of the image

        texWidth[i] = texture.width; //Sets the width to the width of the image
        texHeight[i] = texture.height; //Sets the height to the height of the image
        texMipMapCount[i] = texture.mipmapCount; //Sets the amount of mip maps to the amount of mip maps of the image

        byte[] bytes = texture.GetRawTextureData(); //Converts the image to a byte array
        return bytes; 
    }

    /// <summary>
    /// Sets the values of the entered equipment
    /// </summary>
    /// <param name="equip">Equipment to save</param>
    /// <param name="i">Index of equipment</param>
    public void SetEquip(Equipment equip, int i)
    {
        equipSlot[i] = equip.equipSlot.ToString(); //Sets the equip slot to the slot of the equipment
        armorModifier[i] = equip.armorModifier; //Sets the armormodifier to the armormodifier of the equipment
        damageModifier[i] = equip.damageModifier; //Sets the damagemodifier to the damagemodifier of the equipment
    }
}
