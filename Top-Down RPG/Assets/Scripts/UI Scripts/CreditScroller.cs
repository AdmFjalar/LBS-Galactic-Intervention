using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroller : MonoBehaviour
{
    public int speed;
    public LoadScene loadScene;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadScene.LoadMainMenu(); //Fades to the main menu
        }

        if (transform.position.y <= 1535) //Checks if the entire window has passed
        {
            gameObject.transform.position += new Vector3(0, 0.001f * speed, 0); //Moves the object upwards with the given speed
        }
        else if (transform.position.y > 1535)
        {
            loadScene.LoadMainMenu(); //Fades to the main menu
        }
    }
}
