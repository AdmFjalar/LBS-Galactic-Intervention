using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject player; //The player gameobject
    public PlayerController playerController; //The playercontroller
    public PlayerStats playerStats; //The playerstats component on the player
    public Inventory inventory; //The inventory of the player
    public AttackMenu attackMenu; //The player's attackmenu
    public EquipmentManager equipManager; //The equipment manager
    //public Equipment[] equipment;
    //public List<Attack> attacks;
    public float[] playerPosition; //The player's position
    public ItemCatalog _instance; //The itemcatalog instance

    public AudioSource audioSource;
    public AudioClip defaultMusic;

    SaveData data; //The savedata being loaded

    public static GameManager instance; //The instance of the gamemanager

    void Awake()
    {
        _instance = ItemCatalog.Instance; //Finds the itemcatalog instance
        instance = this; //Sets the gamemanager instance to this
        //Load(); //Loads the savedata
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("InFight") == 0 && PlayerPrefs.GetInt("BattleScreen") == 0)
        {
            audioSource.clip = defaultMusic;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Starts the saving process
    /// </summary>
    public void Save()
    {
        SaveSystem.SaveProgress(this); //Saves the progress through the savesystem
    }

    /// <summary>
    /// Parses out the equipment slot of the inserted equipment
    /// </summary>
    /// <typeparam name="EquipmentSlot">The datatype to parse out</typeparam>
    /// <param name="value">The string to parse</param>
    /// <returns></returns>
    public EquipmentSlot ParseEnum<EquipmentSlot>(string value)
    {
        return (EquipmentSlot)Enum.Parse(typeof(EquipmentSlot), value, true); //Parses the equipmentslot of the string
    }

    /// <summary>
    /// Adds the saved equipment to the equipment manager
    /// </summary>
    public void InsertEquipment()
    {
        for (int i = 12; i - 12 < data.numberOfEquipment; i++) //Goes through all the saved equipment
        {
            if (data.equipSlot[i] != null) //Checks if the equipment exists
            {
                Equipment equipToAdd = Equipment.CreateInstance(typeof(Equipment)) as Equipment; //Creates a buffer equipment

                equipToAdd.name = data.names[i]; //Sets the name to the saved name

                Texture2D tex = new Texture2D(data.texWidth[i], data.texHeight[i], TextureFormat.RGBA32, data.texMipMapCount[i] > 1); //Creates a texture with the saved texture data

                if (data.icons[i] != null) //Checks if the icon exists
                {
                    tex.LoadRawTextureData(data.icons[i]); //Loads the saved sprite onto the texture
                    tex.Apply(); //Applies the texture change
                    Sprite buffSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 32f); //Buffersprite that gets added onto the buffer equipment
                    equipToAdd.icon = buffSprite; //Sets the icon to the saved icon
                }

                equipToAdd.isDefault = data.isDefault[i]; //Sets the isdefault value to the saved isdefault
                equipToAdd.isJunk = data.isJunk[i]; //Sets the isjunk value to the saved isjunk
                equipToAdd.price = data.prices[i]; //Sets the price to the saved price
                equipToAdd.ManaRes = data.manaRes[i]; //Sets the manares to the saved manares
                equipToAdd.HealthRes = data.healthRes[i]; //Sets the healthres to the saved healthres

                equipToAdd.armorModifier = data.armorModifier[i]; //Sets the armor bonus to the saved armor bonus
                equipToAdd.damageModifier = data.damageModifier[i]; //Sets the damage bonus to the saved damage bonus

                equipToAdd.equipSlot = ParseEnum<EquipmentSlot>(data.equipSlot[i]); //Parses the equipment slot of the item

                EquipmentManager.instance.Equip(equipToAdd); //Adds the equipment to the equipment manager
            }
        }
    }
    
    /// <summary>
    /// Loads the saved progress
    /// </summary>
    public void Load()
    {
        data = SaveSystem.LoadData(); //Loads the savedata into the data object
        if (data != null) //Checks if data exists
        {
            playerStats.level = data.level; //Sets the player's level to the saved level
            playerStats.xp = data.xp; //Sets the player's xp to the saved xp
            playerStats.gold = data.gold; //Sets the player's gold to the saved gold
            if (data.numberOfItems > 0) //Checks if the savefile contains any items
            {
                for (int i = 0; i < data.numberOfItems; i++) //Goes through all the saved items
                {
                    Equipment equipToAdd = Equipment.CreateInstance(typeof(Equipment)) as Equipment; //Buffer equipment used when adding to inventory and equipmentmanager
                    Item itemToAdd = Item.CreateInstance(typeof (Item)) as Item; //Buffer item used when adding to inventory

                    if (data.toi[i] == "Item") //Checks if the item is a normal item
                    {
                        itemToAdd.name = data.names[i]; //Sets the name of the item to the saved name

                        Texture2D tex = new Texture2D(data.texWidth[i], data.texHeight[i], TextureFormat.RGBA32, data.texMipMapCount[i] > 1); //Creates a texture with the saved texture data

                        tex.LoadRawTextureData(data.icons[i]); //Loads the saved sprite onto the texture
                        tex.Apply(); //Applies the texture change
                        Sprite buffSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 32f); //Buffersprite that gets added onto the buffer item
                        itemToAdd.icon = buffSprite; //Sets the buffer item's icon to the buffersprite

                        itemToAdd.isDefault = data.isDefault[i]; //Sets the isdefault value to the saved isdefault
                        itemToAdd.isJunk = data.isJunk[i]; //Sets the isjunk value to the saved isjunk
                        itemToAdd.price = data.prices[i]; //Sets the price to the saved price
                        itemToAdd.ManaRes = data.manaRes[i]; //Sets the manares to the saved manares
                        itemToAdd.HealthRes = data.healthRes[i]; //Sets the healthres to the saved healthres
                        itemToAdd.XPRes = data.xpRes[i]; //Sets the xpres to the saved xpres

                        inventory.Add(itemToAdd); //Adds the bufferitem to the inventory
                    }
                    else if (data.toi[i] == "Equipment") //Checks if the item is a piece of equipment
                    {
                        equipToAdd.name = data.names[i]; //Sets the name of the item to the saved name

                        Texture2D tex = new Texture2D(data.texWidth[i], data.texHeight[i], TextureFormat.RGBA32, data.texMipMapCount[i] > 1); //Creates a texture with the saved texture data

                        tex.LoadRawTextureData(data.icons[i]); //Loads the saved sprite onto the texture
                        tex.Apply(); //Applies the texture change
                        Sprite buffSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 32f); //Buffersprite that gets added onto the buffer item
                        equipToAdd.icon = buffSprite; //Sets the buffer item's icon to the buffersprite

                        equipToAdd.isDefault = data.isDefault[i]; //Sets the isdefault value to the saved isdefault
                        equipToAdd.isJunk = data.isJunk[i]; //Sets the isjunk value to the saved isjunk
                        equipToAdd.price = data.prices[i]; //Sets the price to the saved price
                        equipToAdd.ManaRes = data.manaRes[i]; //Sets the manares to the saved manares
                        equipToAdd.HealthRes = data.healthRes[i]; //Sets the healthres to the saved healthres

                        equipToAdd.armorModifier = data.armorModifier[i]; //Sets the armor bonus to the saved armor bonus
                        equipToAdd.damageModifier = data.damageModifier[i]; //Sets the damage bonus to the saved damage bonus

                        equipToAdd.equipSlot = ParseEnum<EquipmentSlot>(data.equipSlot[i]); //Parses the equipment slot of the item

                        inventory.Add(equipToAdd); //Adds the item to the inventory
                    }
                }
            }

            for (int i = 0; i < data.numberOfAttacks; i++) //Goes through all saved attacks
            {
                Attack atk = Attack.CreateInstance(typeof(Attack)) as Attack; //Creates a buffer attack
                atk.name = data._names[i]; //Sets the name to the saved name
                atk.damageRangeMax = data.drmax[i]; //Sets the max range to the saved max range
                atk.damageRangeMin = data.drmin[i]; //Sets the min range to the saved min range
                atk.manaCost = data.mCost[i]; //Sets the mana cost to the saved mana cost

                attackMenu.attacks.Add(atk); //Adds the buffer attack to the attackmenu
            }
            
            playerStats.damageModifier = data.damageMod; //Sets the damagemodifier to the saved damagemodifier
            playerStats.healthModifier = data.healthMod; //Sets the healthmodifier to the saved healthmodifier
            playerStats.manaModifier = data.manaMod; //Sets the manamodifier to the saved manamodifier
            playerStats.skillPoints = data.skillPoints; //Sets the skillpoints to the saved skillpoints
        }
    }
}
