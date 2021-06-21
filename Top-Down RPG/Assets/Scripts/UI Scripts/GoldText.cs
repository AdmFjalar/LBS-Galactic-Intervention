using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldText : MonoBehaviour {

    public TextMeshProUGUI goldText; //The text where the gold will be displayed
    public int gold; //The player's current amount of gold
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.playerStats != null)
        {
            gold = GameManager.instance.playerStats.gold; //Sets the gold to the player's amount of gold
        }

        goldText.text = "Gold: " + gold; //Sets the gold text to display the amount of gold
	}
}
