using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public int ID;
    public float timer;
    public GameObject prefabPlayer;
    public GameObject Player;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("TileID", PlayerPrefs.GetInt("TileID") + 1);
        ID = PlayerPrefs.GetInt("TileID");
    }
	
	// Update is called once per frame
	void Update () {
        Player = GameObject.FindWithTag("Player");
		if (ID == 1 && timer < 0.2f)
        {
            timer += Time.deltaTime;
        }
	}
    public void LateUpdate()
    {
        if (timer >= 0.2f && Player == null)
        {
            Instantiate(prefabPlayer, transform.position, Quaternion.identity);
        }
    }
}
