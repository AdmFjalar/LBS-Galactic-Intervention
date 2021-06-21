using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPText : MonoBehaviour {

    public TextMeshProUGUI HP; //The HP text
    public int Health; //The health of the player
    public int MaxHealth; //The max health of the player

    public GameObject player; //The player gameobject
    public CharacterStats playerStats; //The characterstats of the player

	// Update is called once per frame
	void Update () {
        if (player == null) //Checks if the player is null
        {
            player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the player tag
        }
        if (player != null && playerStats == null) //Checks if the player exists and if the playerstats does not
        {
            playerStats = player.GetComponent<CharacterStats>(); //Sets the playerstats to the characterstats component on the player
        }

        if (playerStats != null) //Checks if the playerstats exists
        {
            Health = playerStats.currentHealth; //Sets the health to the health of the player
            MaxHealth = playerStats.maxHealth; //Sets the max health to the max health of the player
        }

        HP.text = "HP: " + Health + " / " + MaxHealth; //Sets the HP text to show the player's current health and their max health
	}
}
