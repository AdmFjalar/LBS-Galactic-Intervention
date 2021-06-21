using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCManager : MonoBehaviour
{
    public bool IsInRange; //Checks if an npc is in range of the player

    public float LInRange; //Sets the range between the closest npc and player
    public float difference = 1; //The ratio between the difference from the player battleposition and the player and the enemy battleposition and the enemy
    public bool moved = false; //Shows whether the camera has been moved or not

    public Vector3 enemyHome = new Vector3 (0,0,0); //The spot to where the enemy will return if the player successfully escapes combat

    public int speed = 1; //The speed at which the entity will move towards their battleposition
    public int HP; //The enemy's health
    public int maxHP; //The enemy's max health
    public int Mana; //The enemy's mana
    public int maxMana; //The enemy's max mana
    public int level; //The enemy's level

    public TextMeshProUGUI HPText; //Health text for enemy
    public TextMeshProUGUI ManaText; //Mana text for enemy
    public TextMeshProUGUI LevelText; //Level text for enemy

    public GameObject InteractMenu; //Interact Menu gameobject
    public GameObject Player; //The player gameobject
    public GameObject NPCInRange; //The closest NPC that is in range of the player
    public GameObject PlayerSpot; //The spot where the player will stand during combat
    public GameObject EnemySpot; //The spot where the enemy will stand during combat
    public GameObject enemy; //The enemy gameobject
    public GameObject _camera; //The main camera

    public CharacterStats enemyStats; //The characterstats script of the enemy gameobject
    public CharacterStats playerStats; //The characterstats script of the player gameobject

    public void Start()
    {
        PlayerPrefs.SetInt("InFight", 0); //Sets that the player is not in fight
        if (InteractMenu == null) //Checks if the interactmenu is null
        {
            InteractMenu = GameObject.Find("InteractMenu"); //Sets the interactmenu to the gameobject with the name InteractMenu
        }
            Player = GameObject.Find("Player"); //Sets the player to the gameobject with the name Player
    }

    /// <summary>
    /// Returns the closest NPC/Interactable
    /// </summary>
    /// <returns>Closest NPC/Interactable</returns>
    public GameObject IsPlayerInRange()
    {
        GameObject[] gos; //Array that will hold all the interactable gameobjects
        gos = GameObject.FindGameObjectsWithTag("Interactable"); //Adds all interactable gameobjects to gos
        float distance = Mathf.Infinity; //Sets distance to infinity
        foreach (GameObject go in gos) //Goes through every gameobject in gos
        {
            float curDistance = Mathf.Sqrt((go.transform.position.x - Player.transform.position.x) * (go.transform.position.x - Player.transform.position.x) + (go.transform.position.y - Player.transform.position.y) * (go.transform.position.y - Player.transform.position.y)); //Sets curdistance to distance between player and gameobject

            if (curDistance < distance) //Checks if the curdistance is lower than distance
            {
                NPCInRange = go; //Sets the NPC in range to the current gameobject
                distance = curDistance; //Sets the distance to curdistance
                LInRange = curDistance; //Sets LInRange to curdistance
            }
        }
        return NPCInRange;
    }

    public void Update()
    {
        if (NPCInRange != null) //Checks if NPC in range is null
        {
            enemyStats = NPCInRange.GetComponent<CharacterStats>(); //Sets the enemystats to the characterstats script on the npc in range
            playerStats = Player.GetComponent<CharacterStats>(); //Sets the playerstats to the characterstats script on the player

            if (enemyStats != null) //Checks if enemystats is not null
            {
                HP = enemyStats.currentHealth; //Sets HP to the enemy's health
                maxHP = enemyStats.maxHealth; //Sets max HP to the enemy's max health
                Mana = enemyStats.currentMana; //Sets mana to the enemy's mana
                maxMana = enemyStats.maxMana; //Sets max mana to the enemy's max mana
                level = enemyStats.level; //Sets level to the enemy's level

                HPText.text = "HP: " + HP + " / " + maxHP; //Sets the HP text to show the current health and max health of the enemy
                ManaText.text = "Mana: " + Mana + " / " + maxMana; //Sets the Mana text to show the current mana and the max mana of the enemy
                LevelText.text = "Level: " + level; //Sets the Level text to show the current level of the enemy
            }
        }

        if (InteractMenu == null) //Checks if the interactmenu is null
        {
            InteractMenu = GameObject.Find("InteractMenu"); //Sets the interactmenu to the gameobject with the name InteractMenu
        }

        if (Player == null) //Checks if the player is null
        {
            Player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the tag player
        }

        PlayerPrefs.SetFloat("Distance", LInRange); //Sets the distance float to LInRange

        if (InteractMenu != null && Player != null && PlayerPrefs.GetInt("InFight") == 0) //Checks if interactmenu is not null and player is not null and player is not in battle
        {

            IsPlayerInRange(); //Calls the IsPlayerInRange method

            if (PlayerPrefs.GetInt("InDialogue") == 1 || PlayerPrefs.GetInt("InFight") == 1 || PlayerPrefs.GetInt("InShop") == 1 || PlayerPrefs.GetInt("BattleScreen") == 1) //Checks if the player is in dialogue
            {
                InteractMenu.SetActive(false); //Sets the interactmenu inactive
            }
            else if (PlayerPrefs.GetInt("InDialogue") == 0) //Checks if the player is not in dialogue
            {
                if (LInRange <= 1.2) //Checks if LInRange is less or equal to 1.2
                {
                    InteractMenu.SetActive(true); //Sets interactmenu as active
                }
                if (LInRange > 1.2) //Checks if LInRange is greater than 1.2
                {
                    InteractMenu.SetActive(false); //Sets interactmenu as inactive
                }
            }
        }

        if (PlayerPrefs.GetInt("InFight") == 1) //Checks if player is in battle
        {
            if (enemyHome == new Vector3(0,0,0)) //Checks if enemyhome is at 0 on all three axes
            {
                enemy = NPCInRange; //Sets enemy to the npc in range
                enemyHome = enemy.transform.position; //Sets the enemyhome to the enemy's position
            }
            
            NPCInRange.tag = "Enemy"; //Sets the npc in range tag to enemy

            /*
            float enemyDistance = Mathf.Sqrt ((NPCInRange.transform.position.x - EnemySpot.transform.position.x) * (NPCInRange.transform.position.x - EnemySpot.transform.position.x) + (NPCInRange.transform.position.y - EnemySpot.transform.position.y) * (NPCInRange.transform.position.y - EnemySpot.transform.position.y)); //Checks the distance between the npc in range and the enemy battleposition

            float playerDistance = Mathf.Sqrt((Player.transform.position.x - PlayerSpot.transform.position.x) * (Player.transform.position.x - PlayerSpot.transform.position.x) + (Player.transform.position.y - PlayerSpot.transform.position.y) * (Player.transform.position.y - PlayerSpot.transform.position.y)); //Checks the distance between the player and the player battleposition

            if (enemyDistance / playerDistance != 0) //Checks if the distance between the enemy and the enemyspot divided by player and playerspot is not 0
            {
                difference = enemyDistance / playerDistance; //Sets the difference to enemydistance divided by playerdistance
            }

            if (enemyDistance / playerDistance == 0) //Checks if the distance between the enemy and the enemyspot divided by player and playerspot is 0
            {
                difference = 1; //Sets the difference to 1
            }

            NPCInRange.transform.position = Vector3.MoveTowards(NPCInRange.transform.position, new Vector3(EnemySpot.transform.position.x, EnemySpot.transform.position.y, 0), step1); //Moves the enemy towards the enemy battleposition

            Player.transform.position = Vector3.MoveTowards(Player.transform.position, new Vector3(PlayerSpot.transform.position.x, PlayerSpot.transform.position.y, 0), step2); //Moves the player towards the player battleposition 
            */
            //NPCInRange.transform.position = new Vector3(EnemySpot.transform.position.x, EnemySpot.transform.position.y, 0); //Moves the enemy to the enemy spot
            if (!moved) //Checks if the camera has been moved
            {
                Vector3 offset = new Vector3(EnemySpot.transform.position.x - enemy.transform.position.x, EnemySpot.transform.position.y - enemy.transform.position.y, 0); //Determines the offset between the enemy spot and the enemy
                _camera.transform.position -= offset; //Moves the camera so that the enemy is in the correct space
                moved = true; //Sets that the camera has been moved
            }
            Player.transform.position = new Vector3(PlayerSpot.transform.position.x, PlayerSpot.transform.position.y, 0); //Moves the player to the player spot
        }

        if (PlayerPrefs.GetInt("InFight") == 1)
        {
            enemy.transform.localScale = new Vector3(2, 2, 1);
            Player.transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }

        if (PlayerPrefs.GetInt("InFight") == 0 && enemy != null && enemy.transform.position != enemyHome) //Checks if player is not in battle and enemy is not null and enemy is not at enemy battleposition
        {
            moved = false; //Sets that the camera has not been moved
            float step = speed * Time.deltaTime; //Sets the speed of the enemy
            Vector3 offset = new Vector3 (EnemySpot.transform.position.x - enemy.transform.position.x, EnemySpot.transform.position.y - enemy.transform.position.y, 0); //Determines the offset between the enemy and the enemy spot
            _camera.transform.position -= offset;
            //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, new Vector3(enemyHome.x, enemyHome.y, 0), step); //Moves the enemy towards the enemy battleposition
        }

        if (PlayerPrefs.GetInt("InFight") == 0 && enemy != null && enemy.transform.position == enemyHome) //Checks if the player is in battle and the enemy is not null and the enemy is at the enemy battleposition
        {
            enemy.transform.localScale = new Vector3(1, 1, 1);
            Player.transform.localScale = new Vector3(1, 1, 1);
            moved = false;
            enemy = null; //Sets enemy to null
            enemyHome = new Vector3(0, 0, 0); //Sets enemyhome to 0 on all axes
        }

        if (PlayerPrefs.GetInt("InFight") == 0 && Player.transform.position == PlayerSpot.transform.position)
        {
            Player.transform.position = Player.GetComponent<PlayerController>().target.transform.position;
        }

        if (PlayerPrefs.GetInt("InFight") == 0)
        {
            Player.transform.localScale = new Vector3(1, 1, 1);
            moved = false;
            enemy = null;
            enemyHome = new Vector3(0, 0, 0); //Sets enemyhome to 0 on all axes
        }
    }
}
