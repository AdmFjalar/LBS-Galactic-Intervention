using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPickup : MonoBehaviour {

    public Attack attack; //The attack which will be picked up

	// Use this for initialization
	void Start () {
        PickUp(); //Calls the pickup method so the attack will be equipped instantly when the attack is spawned
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    /// <summary>
    /// Picks up attack and adds it to the attack menu
    /// </summary>
    void PickUp()
    {
        bool wasPickedUp = AttackMenu.attackInstance.Add(attack); //Checks if there was enough space to add attack

        if (wasPickedUp) //Checks if there was enough space and the attack was successfully added to the attackmenu
        {
            Debug.Log("Succesfully added " + attack.name);
            Destroy(gameObject); //Removes the attack from the map so it will not be added again
        }
    }
}
