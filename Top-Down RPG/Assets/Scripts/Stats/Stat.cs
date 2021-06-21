
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Makes the class serializable
public class Stat {

    [SerializeField]
    private int baseValue; //The basevalue of the stat

    private List<int> modifiers = new List<int>(); //Modifiers that gets added onto the basevalue

    /// <summary>
    /// Returns the basevalue and the modifiers added onto each other
    /// </summary>
    /// <returns>Basevalue plus modifiers</returns>
    public int GetValue ()
    {
        int finalValue = baseValue; //Sets the final value to the basevalue
        modifiers.ForEach(x => finalValue += x); //Goes through the modifiers and adds them onto the final value
        return finalValue;
    }

    /// <summary>
    /// Adds chosen value to the stat modifiers
    /// </summary>
    /// <param name="modifier">Modifier size</param>
    public void AddModifier (int modifier)
    {
        if (modifier != 0) //Checks if the player does not equal 0
        {
            modifiers.Add(modifier); //Adds the modifier to the modifiers
        }
    }

    /// <summary>
    /// Removes the modifier from the modifiers
    /// </summary>
    /// <param name="modifier">Modifier to remove</param>
    public void RemoveModifier (int modifier)
    {
        if (modifier != 0) //Checks if the modifier is not 0
        {
            modifiers.Remove(modifier); //Removes the modifier
        }
    }

}
