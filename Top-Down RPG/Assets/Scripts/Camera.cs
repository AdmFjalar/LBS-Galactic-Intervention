using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject Player; //The player gameobject

	// Update is called once per frame
	void LateUpdate ()
    {
		if (Player == null) //Checks if the player is null
        {
            Player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the player tag
        }
        if (Player != null && PlayerPrefs.GetInt("InFight") == 0 && PlayerPrefs.GetInt("ReachedTarget") == 0) //Checks if the player exists and if the player is not in a fight and if the player has not reached the target
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -1); //Moves the camera to the player's position
        }
	}
}
