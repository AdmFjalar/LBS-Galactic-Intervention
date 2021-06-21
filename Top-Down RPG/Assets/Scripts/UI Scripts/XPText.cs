using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPText : MonoBehaviour
{
    public int neededXP; //The amount of xp needed to level up
    public int XP; //The amount of xp the player has
    public int Level; //The player's level
    public TextMeshProUGUI xPText; //The XP text

    public GameObject player; //The player gameobject
    public CharacterStats playerStats; //The player's characterstats component
    
	// Update is called once per frame
	void Update ()
    {
        if (player == null) //Checks if the player is null
        {
            player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the player tag
        }

        if (player != null && playerStats == null) //Checks if the player exists and if the playerstats does not exist
        {
            playerStats = player.GetComponent<CharacterStats>(); //Sets the playerstats to the characterstats component on the player
        }
        
        if (playerStats != null) //Checks if the playerstats exists
        {
            neededXP = playerStats.neededXp; //Sets the neededXP to the player's needed xp
            XP = playerStats.xp; //Sets the xp to the player's xp
            Level = playerStats.level; //Sets the level to the player's level
        }

        xPText.text = "Level: " + Level + " (" + XP + "XP" + " / " + neededXP + "XP" + ")"; //Sets the xp text to show the player's xp and needed xp to level up
    }
}
