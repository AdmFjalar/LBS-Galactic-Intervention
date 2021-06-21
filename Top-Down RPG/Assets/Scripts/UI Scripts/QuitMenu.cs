using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour {

    public GameObject PauseMenu; //The pausemenu gameobject

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("InPauseMenu", 0); //Sets that the player is not in the pausemenu
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerPrefs.GetInt("InShop") == 0) //Checks if escape is pressed and if the player is not in the shop
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf); //Sets the pausemenu active status to its own inverse
        }
        if (PauseMenu.activeSelf == true) //Checks if the pausemenu is active
        {
            PlayerPrefs.SetInt("InPauseMenu", 1); //Sets that the pausemenu is active
        }
        else if (PauseMenu.activeSelf == false) //Checks if the pausemenu is inactive
        {
            PlayerPrefs.SetInt("InPauseMenu", 0); //Sets that the pausemenu is inactive
        }
    }
}
