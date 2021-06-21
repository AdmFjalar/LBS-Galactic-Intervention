using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaText : MonoBehaviour {

    public TextMeshProUGUI manaText; //The mana text
    public int Mana; //The player's mana
    public int MaxMana; //The player's max mana

    public GameObject player; //The player gameobject
    public CharacterStats playerStats; //The characterstats of the player
    
	// Update is called once per frame
	void Update () {
        if (player == null) //Checks if the player does not exist
        {
            player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the player tag
        }

        if (player != null && playerStats == null) //Checks if the player exists and if the playerstats does not
        {
            playerStats = player.GetComponent<CharacterStats>(); //Sets the playerstats to the characterstats component on the player
        }

        if (playerStats != null) //Checks if the playerstats exists
        {
            Mana = playerStats.currentMana; //Sets the mana to the mana of the player
            MaxMana = playerStats.maxMana; //Sets the max mana to the max mana of the player
        }

        manaText.text = "Mana: " + Mana + " / " + MaxMana; //Sets the mana text to show the players mana and max mana
	}
}
