using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")] //Allows creation of attacks in asset menu
public class Attack : ScriptableObject
{
    //(Sorry for using playerprefs, I didn't know better when starting and I'm too lazy to redo it now)

    new public string name = "New Attack"; //Name of attack
    //public string attackType = "Attack Type";
    //public Sprite icon = null;
    //public bool isDefault = false;

    //public Animation animation;

    public GameObject user; //Entity using attack

    public int damageRangeMin; //Minimum damage dealt
    public int damageRangeMax; //Maximum damage dealt
    private int damageDealt = 0; //How much damage is dealt after all bonuses
    public int manaCost; //How much mana it costs to use the attack

   
    /// <summary>
    /// This method calculates the base damage the attack will deal and sets playerpref values
    /// </summary>
    /// <param name="User">Entity using attack</param>
    public virtual void Use (GameObject User)
    {
        user = User; //Sets user entity to the inserted entity
        damageDealt = Random.Range(damageRangeMin, damageRangeMax + 1); //Sets random value between maximum and minimum damage
        //Use the attack

        //Sets the respective playerpref values to the values entered and calculated.
        PlayerPrefs.SetInt("DamageDealt", damageDealt);
        PlayerPrefs.SetInt("ManaCost", manaCost);
        PlayerPrefs.SetInt("UsingAttack", 1);
    }
}
