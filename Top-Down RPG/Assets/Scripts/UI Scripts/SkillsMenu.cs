using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsMenu : MonoBehaviour {

    public GameObject skillsMenu; //The skills menu gameobject
    public GameObject player; //The player gameobject

    public TextMeshProUGUI Health; //The healthmodifier text
    public TextMeshProUGUI Mana; //The manamodifier text
    public TextMeshProUGUI Damage; //The damagemodifier text
    public TextMeshProUGUI skillPoints; //The skillpoint text

    public PlayerStats playerStats; //The playerstats
    
	// Update is called once per frame
	void Update ()
    {
        if (PlayerPrefs.GetInt("InPauseMenu") == 0) //Checks if the player is not in the pause menu
        {
            if (player == null) //Checks if the player is null
            {
                player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the player tag
            }
            if (playerStats == null && player != null) //Checks if the playerstats are null and if the player exists
            {
                playerStats = player.GetComponent<PlayerStats>(); //Sets the playerstats to the playerstats component on the player
            }
            if (Input.GetKeyDown(KeyCode.P) && PlayerPrefs.GetInt("InInventory") == 0 && PlayerPrefs.GetInt("InDialogue") == 0) //Checks if P is pressed and if the player is not in the inventory and if the player is not in a dialogue or fight
            {
                skillsMenu.SetActive(!skillsMenu.activeSelf); //Sets the skillsmenu active state to its own inverse
            }
            if (skillsMenu.activeSelf == true) //Checks if the skillmenu is active
            {
                PlayerPrefs.SetInt("InSkillsMenu", 1); //Sets that the skillsmenu is active/open
            }
            else
            {
                PlayerPrefs.SetInt("InSkillsMenu", 0); //Sets that the skillsmenu is inactive/closed
            }
        }
	}

    void LateUpdate()
    {
        if (player != null && playerStats != null) //Checks if the player exists and if the playerstats exists
        {
            Health.text = "Health: " + playerStats.healthModifier; //Sets the healthmodifier text to the player's healthmodifier
            Mana.text = "Mana: " + playerStats.manaModifier; //Sets the manamodifier text to the player's manamodifier
            Damage.text = "Damage: " + playerStats.damageModifier; //Sets the damagemodifier text to the player's damagemodifier
            skillPoints.text = "Points Available: " + playerStats.skillPoints; //Sets the skillpoint text to the player's amount of skillpoints
        }
    }
}
