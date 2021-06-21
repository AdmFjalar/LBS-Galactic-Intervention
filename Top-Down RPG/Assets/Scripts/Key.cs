using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Inventory/Key")]
public class Key : Item
{
    public override void Use()
    {
        base.Use();

        PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
        RemoveFromInventory();
    }
}
