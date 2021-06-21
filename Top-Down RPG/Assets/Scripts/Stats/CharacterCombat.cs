using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))] //Makes sure the character also contains a characterstats script
public class CharacterCombat : MonoBehaviour {

    CharacterStats myStats; //The stats of the entity
    public CharacterStats targetsStats; //The stats of the entity's enemy

    public bool wasSuccessful; //Represents if the attack was successful

    public GameObject enemy; //The enemy of the entity

    public int myDamage; //The basedamage of the entity
    public int damageDealt; //The damage which the attack will deliver to the enemy

    void Start()
    {
        PlayerPrefs.SetInt("UsingAttack", 0); //Sets that there are no attacks in use
        
        PlayerPrefs.SetInt("DamageDealt", 0); //Sets that there is no damage being dealt
        PlayerPrefs.SetInt("ManaCost", 0); //Sets that there is no mana cost
        myStats = GetComponent<CharacterStats>(); //Sets the characterstats of the entity to the characterstats component on the entity
    }

    void Update()
    {
        if (gameObject.tag == "Player") //Checks if the entity's tag is player
        {
            enemy = GameObject.FindWithTag("Enemy"); //Sets the enemy as the gameobject with the enemy tag
            if (enemy != null) //Checks if the enemy is not null
            {
                targetsStats = enemy.GetComponent<CharacterStats>(); //Sets the targetstats as the stats of the enemy
            }
            if (enemy != null && PlayerPrefs.GetInt("InFight") == 1 && PlayerPrefs.GetInt("UsingAttack") == 1 && PlayerPrefs.GetInt("Turn") == 1) //Checks if the enemy is not null and if the player is in combat and if the entity is using an attack and if it is the players turn
            {
                wasSuccessful = Attack(targetsStats); //Represents if the attack was successful
            }
        }
        else if (gameObject.tag == "Enemy") //Checks if the entity's tag is enemy
        {
            enemy = GameObject.FindWithTag("Player"); //Sets the enemy as the gameobject with the player tag
            if (enemy != null) //Checks if the enemy is not null
            {
                targetsStats = enemy.GetComponent<CharacterStats>(); //Sets the targetstats as the stats of the enemy
            }
            if (enemy != null && PlayerPrefs.GetInt("InFight") == 1 && PlayerPrefs.GetInt("UsingAttack") == 1 && PlayerPrefs.GetInt("Turn") == 2) //Checks if the enemy is not null and if the player is in combat and if the entity is using an attack and if it is the entity's turn
            {
                wasSuccessful = Attack(targetsStats); //Represents if the attack was successful
            }
        }
        if (wasSuccessful) //Checks if the attack was successful
        {
            PlayerPrefs.SetInt("UsingAttack", 0); //Sets that there is no attack in use
            wasSuccessful = false; //Sets wasSuccessful as false
            if (PlayerPrefs.GetInt("Turn") == 1) //Checks if it is the player's turn
            {
                targetsStats = enemy.GetComponent<CharacterStats>(); //Sets the target stats as the characterstats of the enemy
                PlayerPrefs.SetInt("Turn", 2); //Sets the turn as 2
            }
            else if (PlayerPrefs.GetInt("Turn") == 2) //Checks if it is the enemy's turn
            {
                targetsStats = enemy.GetComponent<CharacterStats>(); //Sets the target stats as the characterstats of the enemy
                PlayerPrefs.SetInt("Turn", 1); //Sets the turn as 1
            }
        }

        myDamage = myStats.damage.GetValue(); //Sets myDamage as the basedamage of the entity
    }

    /// <summary>
    /// Used when attacking target
    /// </summary>
    /// <param name="targetStats">The stats which will be attacked</param>
    /// <returns>Whether the attack was successful or not</returns>
    public bool Attack (CharacterStats targetStats)
    {
        if (myStats.currentMana - PlayerPrefs.GetInt("ManaCost") >= 0) //Checks if the entity affords the manacost of the attack
        {
            if (myStats.currentMana - PlayerPrefs.GetInt("ManaCost") <= myStats.maxMana) //Checks if the manacost will be smaller or equal to the max mana of the entity
            {
                myStats.currentMana = myStats.currentMana - PlayerPrefs.GetInt("ManaCost"); //Removes the manacost from the entity's mana
            }

            damageDealt = myDamage + PlayerPrefs.GetInt("DamageDealt"); //Sets the damage dealt as the basestat plus the damage bonuses from the attack and equipment

            targetStats.TakeDamage(damageDealt); //Deals damage to the target

            PlayerPrefs.SetInt("DamageDealt", 0); //Sets damage dealt as 0
            return true;
        }
        else return false;
    }
}
