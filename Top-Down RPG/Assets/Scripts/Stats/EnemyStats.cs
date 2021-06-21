using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : CharacterStats {

    public GameObject loot; //The loot which the player will receive upon killing the enemy

    public int _level; //Level that is editable from the inspector

    public LoadScene levelLoader;

    private void Awake()
    {
        levelLoader = GameObject.FindObjectOfType<LoadScene>();
        level = _level; //Sets the level to the level of this class   
    }

    /// <summary>
    /// Overrides the normal finishdeath to add the loot and the smoke effect when dying among with some other more things
    /// </summary>
    public override void FinishDeath()
    {
        base.FinishDeath();
        
        if (gameObject.name == "Per")
        {
            levelLoader.LoadMainMenu();
        }

        if (loot != null) //Checks if the enemy will give any loot
        {
            Instantiate(loot, transform.position, Quaternion.identity); //Spawns the loot
        }

        GameObject player = GameObject.FindWithTag("Player"); //Sets the player as the gameobject with the tag player
        PlayerStats playerStats = player.GetComponent<PlayerStats>(); //Sets the playerstats as the playerstats component attached to the player

        int levelDifference = Mathf.RoundToInt(level / playerStats.level); //Checks the powerratio between the player and the enemy

        playerStats.xp += Random.Range(levelDifference * 25, levelDifference * 50); //Adds xp to the player that represents the leveldifference between the enemy and player
        playerStats.gold +=Random.Range(levelDifference * 5, levelDifference * 20); //Adds gold to the player
        playerStats.currentMana = playerStats.maxMana; //Restores the players mana
        PlayerPrefs.SetInt("ReachedTarget", 1); //Sets that the target has been reached
        PlayerPrefs.SetInt("InFight", 0); //Sets that the player no longer is in a fight

        Destroy(gameObject); //Removes the enemy
    }

}
