using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerController playControl; //The playercontroller script

    private void Start()
    {
        playControl = GameObject.Find("Player").GetComponent<PlayerController>(); //Sets the playercontroller to the playercontroller attached to the player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetCollision(true, gameObject.tag.ToString()); //Calls the setcollision and feeds in the direction and sets that there is a collision
    }

    private void OnTriggerStay2D(Collider2D collision)
	{
        SetCollision(true, gameObject.tag.ToString()); //Calls the setcollision and feeds in the direction and sets that there is a collision
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SetCollision(false, gameObject.tag.ToString());//Calls the setcollision and feeds in the direction and sets that there is not a collision
    }

    /// <summary>
    /// Tells the playercontroller if there is currently a collision for every direction
    /// </summary>
    /// <param name="b">Whether there is a collision in the chosen direction or not</param>
    /// <param name="s">The direction of the collision</param>
    public void SetCollision(bool b, string s)
    {
        switch (s) //Checks the direction
        {
            case "North":
                playControl.dirCollisions[0] = b; //Sets if there is a collision to the north of the player
                break;
            case "South":
                playControl.dirCollisions[1] = b; //Sets if there is a collision to the south of the player
                break;
            case "East":    
                playControl.dirCollisions[2] = b; //Sets if there is a collision to the east of the player
                break;
            case "West":
                playControl.dirCollisions[3] = b; //Sets if there is a collision to the west of the player
                break;
        }
    }
}
