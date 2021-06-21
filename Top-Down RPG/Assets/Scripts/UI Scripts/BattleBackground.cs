using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackground : MonoBehaviour
{
    public GameObject battleBackground; //The background for battles
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("InFight") == 1) //Checks if the player is in combat
        {
            battleBackground.SetActive(true); //Activates the battle background
        }
        else
        {
            battleBackground.SetActive(false); //Deactivates the battle background
        }
    }
}
