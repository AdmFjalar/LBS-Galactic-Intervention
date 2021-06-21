using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreen : MonoBehaviour
{
    /// <summary>
    /// Used when the battle screen has passed to disable it
    /// </summary>
    public void DisableScreen()
    {
        gameObject.GetComponent<Animator>().SetBool("EnteringBattle", false); //Tells the animator that the animation is finished
        gameObject.SetActive(false); //Turns off the gameobject
        PlayerPrefs.SetInt("BattleScreen", 0); //Sets it so the battle screen is inactive and the player can move
    }

    /// <summary>
    /// Starts the battle
    /// </summary>
    public void StartBattle()
    {
        PlayerPrefs.SetInt("Turn", 1); //Sets it to the players turn in combat
        PlayerPrefs.SetInt("InFight", 1); //Starts battle
    }
}
