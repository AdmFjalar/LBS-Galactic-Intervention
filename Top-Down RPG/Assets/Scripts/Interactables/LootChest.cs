using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour {

    public GameObject Player; //Player gameobject
    public CharacterStats playerStats; //Characterstats of player

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Player == null) //Checks if player is null
        {
            Player = GameObject.FindWithTag("Player"); //Sets player to the gameobject with the tag player
            playerStats = Player.GetComponent<CharacterStats>(); //Sets the playerstats to the characterstats script on the player
        }

        if (Input.GetKeyDown("e") && PlayerPrefs.GetInt("InDialogue") == 0 && Mathf.Sqrt(Mathf.Pow(Player.transform.position.x-transform.position.x,2)+Mathf.Pow(Player.transform.position.y-transform.position.y,2))<=1) //Checks if E is pressed and if the player is not in dialogue and the distance between the chest and player is less or equal to 1
        {
            int addXP = Random.Range(1, 50); //Sets an amount of XP to add to player between 1 and 50
            int addGold = Random.Range(1, 50); //Sets an amount of gold to add to player between 1 and 50

            playerStats.xp += addXP; //Adds the chosen amount of XP to the player
            GameManager.instance.playerStats.gold += addGold; //Adds the chosen amount of gold to the player

            gameObject.tag = "Depleted"; //Marks the chest as depleted
            Destroy(this); //Removes this script to avoid using the script multiple times
        }

    }
}
