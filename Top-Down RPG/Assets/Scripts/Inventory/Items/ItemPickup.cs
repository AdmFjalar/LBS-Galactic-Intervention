using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public Item item; //The item which will be picked up

    public GameObject Player; //The player gameobject

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Player == null) //Checks if player is null
        {
            Player = GameObject.FindWithTag("Player"); //Sets player to the gameobject with the player tag
        }
		if (Mathf.Sqrt((transform.position.x - Player.transform.position.x) * (transform.position.x - Player.transform.position.x) + (transform.position.y - Player.transform.position.y) * (transform.position.y - Player.transform.position.y)) < 0.5f) //Checks if player is close enough to pick up item
        {
            PickUp(); //Calls pickup method
        }
	}

    /// <summary>
    /// Tries to add the item to the player inventory
    /// </summary>
    void PickUp ()
    {
        Debug.Log("Attempting to pick up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item); //Sets a bool representing if there was enough space to add the item

        if (wasPickedUp) //Checks if the item was picked up
        {
            Destroy(gameObject); //Removes the itempickup to prevent picking it up multiple times
        }
    }
}
