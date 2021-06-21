using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackgroundMovement : MonoBehaviour
{
    public GameObject _camera; //The camera gameobject
    void Update()
    {
        transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0); //Moves the gameobject to the camera with and offset on the z axis
    }
}
