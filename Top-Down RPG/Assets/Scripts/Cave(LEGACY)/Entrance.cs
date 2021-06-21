using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour {

    public GameObject Player;
    public GameObject Menu;
    public GameObject NPCManager;
    public GameObject InteractWithE;
    public GameObject entrance;

    // Use this for initialization
    void Start()
    {
        if (Menu == null)
        {
            Menu = GameObject.FindWithTag("LoadLevelMenu");
        }
        if (Menu != null)
        {
            Menu.SetActive(false);
        }
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (entrance == null)
        {
            entrance = GameObject.FindWithTag("CaveEntrance");
        }
        if (NPCManager == null)
        {
            NPCManager = GameObject.FindWithTag("NPCManager");
        }
        if (InteractWithE == null)
        {
            InteractWithE = GameObject.FindWithTag("InteractWithE");
        }
        if (Menu == null)
        {
            Menu = GameObject.FindWithTag("LoadLevelMenu");
        }
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        if (entrance != null && entrance.transform.position.x - Player.transform.position.x >= -1 && entrance.transform.position.x - Player.transform.position.x <= 1 && entrance.transform.position.y - Player.transform.position.y >= -1 && entrance.transform.position.y - Player.transform.position.y <= 1 && Input.GetKeyDown("e"))
        {
            NPCManager.SetActive(false);
            InteractWithE.SetActive(false);
            if (Menu != null)
            {
                Menu.SetActive(true);
            }
        }
        if (entrance != null && (entrance.transform.position.x - Player.transform.position.x < -1.25f || entrance.transform.position.x - Player.transform.position.x > 1.25f || entrance.transform.position.y - Player.transform.position.y < -1.25f || entrance.transform.position.y - Player.transform.position.y > 1.25f))
        {
            if (Menu != null)
            {
                Menu.SetActive(false);
            }
            NPCManager.SetActive(true);
        }
    }
}
