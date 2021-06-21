using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatalog : MonoBehaviour
{
    public List<Item> items = new List<Item>(); //The items in the game
    public static ItemCatalog Instance; //The instance of the itemcatalog

    // Start is called before the first frame update
    void Start()
    {
        Instance = this; //Sets the instance to this script
    }
}
