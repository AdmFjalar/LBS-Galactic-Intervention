using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMenuUI : MonoBehaviour {

    public GameObject inv; //The player's inventory
    public GameObject FightMenu; //The fightmenu
    public Transform attacksParent; //The parent gameobject of the attack slots

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt("InFight") == 1 && PlayerPrefs.GetInt("Turn") == 1) //Checks if the player is in a fight and if it is the player's turn
        {
            FightMenu.SetActive(true); //Sets the fightmenu as active
        }
        else if ((PlayerPrefs.GetInt("InFight") == 0) || (PlayerPrefs.GetInt("InFight") == 1 && PlayerPrefs.GetInt("Turn") != 1)) //Checks if the player is not in a fight or if it is but it is not the player's turn
        {
            FightMenu.SetActive(false); //Sets the fightmenu as inactive
        }
	}

    /// <summary>
    /// Player tries to escape combat
    /// </summary>
    public void PassTurn()
    {
        int rand = Random.Range(1, 5); //A random value between 1 and 4
        Debug.Log(rand);
        if (rand == 1) //Checks if the random value is 1
        {
            PlayerPrefs.SetInt("ReachedTarget", 1); //Sets that the target has been reached
            PlayerPrefs.SetInt("InFight", 0); //Sets that the player no longer is in a fight
        }
        else
        {
            PlayerPrefs.SetInt("Turn", 2); //Sets it to the enemy's turn
        }
    }

    /// <summary>
    /// Toggles the inventory window's status
    /// </summary>
    public void ToggleInventory()
    {
        inv.SetActive(!inv.activeSelf); //Sets the inventory active status as it's own inverse
    }
}
