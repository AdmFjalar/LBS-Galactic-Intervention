using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest")]
public class Quest : ScriptableObject
{
    new public string name = "New Quest";
    public enum Goal { reachNumber, collectItem, killPerson };
    public Goal goal;

    public string targetItem;
    public int targetNumber;
    public string targetName;

    public int reward = 0;
    public Item _reward;

    public GameObject Player;

	// Use this for initialization
	void Start ()
    {
	    if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(goal)
        {
            case Goal.reachNumber:

                break;
            case Goal.collectItem:

                break;
            case Goal.killPerson:

                break;
        }
	}

    public void Reward()
    {
        //Give the player reward
        
    }
}
