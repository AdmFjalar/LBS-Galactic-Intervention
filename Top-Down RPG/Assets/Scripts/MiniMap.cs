using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public GameObject miniMap; //The minimap gameobject

	// Use this for initialization
	void Start () {
		if (miniMap == null) //Checks if the minimap is null
        {
            miniMap = GameObject.FindWithTag("MiniMap"); //Sets the minimap to the gameobject with the minimap tag
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M) && PlayerPrefs.GetInt("InFight") == 0 && PlayerPrefs.GetInt("InDialogue") == 0 && miniMap != null && PlayerPrefs.GetInt("InPauseMenu") == 0) //Checks if M is pressed and if the player is not in a fight and not in a dialogue or pausemenu and if the minimap exists
        {
            miniMap.SetActive(!miniMap.activeSelf); //Inverses the minimap's activestate
        }
        if (PlayerPrefs.GetInt("InFight") == 1) //Checks if the player is in a fight
        {
            miniMap.SetActive(false); //Disables the minimap
        }
	}
}
