using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poof : MonoBehaviour {


    /// <summary>
    /// Deletes the gameobject (Used for the "poof" effect when something/someone dies
    /// </summary>
    public void Destroy()
    {
        Destroy(gameObject); //Deletes the gameobject
    }
}
