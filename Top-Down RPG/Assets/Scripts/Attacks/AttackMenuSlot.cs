using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackMenuSlot : MonoBehaviour {

    public Image icon; //Icon to attack, is currently not used
    public TextMeshProUGUI name; //Text where the name is displayed
    public TextMeshProUGUI cost; //Text where the manacost is displayed
    public TextMeshProUGUI damage; //Text where the damage range is displayed

    public int index;

    Attack attack; //Attack that is equipped on the current slot
    public int atkMin; //The minimum damage dealt with all bonuses added
    public int atkMax; //The maximum damage dealth with all bonuses added

    public int myDam; //Base damage of entity
    public int atkRangeMin; //Minimum damage range of attack without bonuses
    public int atkRangeMax; //Maximum damage range of attack without bonuses

    bool isActive = false; //Bool representing if the slot is currently active

    public CharacterStats myStats; //Stats of entity

    public GameObject Player; //Reference to player

	// Update is called once per frame
	void Update ()
    {
        if (isActive) //Checks if the slot is active
        {
            if (Player == null) //Checks if player is null
            {
                Player = GameObject.FindWithTag("Player"); //Sets the player reference to gameobject with player tag
            }
            if (myStats == null) //Checks if stats of entity is null
            {
                myStats = Player.GetComponent<CharacterStats>(); //Sets entity stats to the player's stats
            }
            if (myStats != null && attack != null) //Checks if stats aren't null and if the slot has an equipped attack
            {
                myDam = myStats.damage.GetValue(); //Sets basedamage to the basedamage of the playerstats
                atkRangeMin = attack.damageRangeMin; //Sets the minimum damage range to the minimum of the playerstats
                atkRangeMax = attack.damageRangeMax; //Sets the maximum damage range to the maximum of the playerstats
                atkMin = myDam + atkRangeMin; //Sets the minimum damage dealt to the basedamage plus the minimum range
                atkMax = myDam + atkRangeMax; //Sets the maximum damage dealt to the basedamage plus the maximum range
                name.text = attack.name; //Sets name text to the name of the equipped attack
                cost.text = "Cost: " + attack.manaCost + " Mana"; //Sets the cost text to the manacost of the attack
                damage.text = "Damage: " + atkMin + "-" + atkMax; //Sets the damage text to show the interval in which the final damage dealt will be (not considering armour on enemy)
            }
        }
	}

    /// <summary>
    /// Equips attack to slot
    /// </summary>
    /// <param name="newAttack">Attack to equip</param>
    public void AddAttack (Attack newAttack)
    {
        attack = newAttack; //Equips new attack

        atkMin = myDam + atkRangeMin; //Sets the minimum damage to the basedamage plus the minimum range
        atkMax = myDam + atkRangeMax; //Sets the maximum damage to the basedamage plus the maximum range

        isActive = true; //Sets the slot to active
        name.text = attack.name; //Sets the name text to the name of the attack
        cost.text = "Cost: " + attack.manaCost + " Mana"; //Sets the cost text to the manacost
        damage.text = "Damage: " + atkMin + "-" + atkMax; //Sets the damage text to show the interval in which the final damage dealt will be (not considering armour on enemy)
        //icon.sprite = attack.icon;
        //icon.enabled = true;
    }


    /// <summary>
    /// Empties slot
    /// </summary>
    public void ClearSlot()
    {
        attack = null; //Removes equipped attack

        isActive = false; //Sets the slot to inactive
        name.text = null; //Empties name text
        cost.text = null; //Empties cost text
        damage.text = null; //Empties damage text
        //icon.sprite = null;
        //icon.enabled = false;
    }

    /// <summary>
    /// Uses attack to deal damage to targeted enemy (enemy is not set in this script)
    /// </summary>
    public void UseAttack()
    {
        if (attack != null) //Checks if an attack is equipped
        {
            if (myStats.currentMana >= attack.manaCost)
            {
                Player.GetComponent<Animator>().SetInteger("UsingAttack", index);
            }
            attack.Use(gameObject); //Uses equipped attack
        }
    }
}
