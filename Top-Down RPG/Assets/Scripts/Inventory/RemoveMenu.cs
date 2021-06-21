using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMenu : MonoBehaviour { 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //Checks if enter is pressed
        {
            RemoveItem(); //Removes the item
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //Checks if escape is pressed
        {
            gameObject.SetActive(false); //Closes the remove menu
        }
	}

    /// <summary>
    /// Removes the item from the chosen slot
    /// </summary>
    public void RemoveItem()
    {
        PlayerPrefs.SetInt("RemovingItem", 1); //Sets that the slot should be emptied
        gameObject.SetActive(false); //Closes the remove menu
    }
}
