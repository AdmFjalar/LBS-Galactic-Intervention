using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMenu : MonoBehaviour {

    public static AttackMenu attackInstance; //The instance of the attackmenu

    public Attack basicAttack; //Default attack

    public delegate void OnAttackChanged(); //Used to update attack menu UI when attacks are added or removed
    public OnAttackChanged onAttackChangedCallback;

    public int space = 4; //Space for attacks in attack menu

    public List<Attack> attacks = new List<Attack>(); //Equiped attacks

    /// <summary>
    /// Adds attack to the equipped attacks
    /// </summary>
    /// <param name="attack">Attack to add</param>
    /// <returns></returns>
    public bool Add (Attack attack)
    {
        //if (!attack.isDefault)
        //{
            if (attacks.Count >= space) //Checks if the attack menu is full
            {
                Debug.Log("Not enough room for more attacks!");
                return false; //Tells the program that it failed to add the attack so it will not be removed
            }

            attacks.Add(attack); //Adds attack to the attack list

            if (onAttackChangedCallback != null) //Checks so that the UI change callback exists
            {
                onAttackChangedCallback.Invoke(); //Invokes the UI change callback so that the UI can be updated
            }
        //}
        
        return true; //Returns that the attack could be added so it will be removed from the map
    }

    /// <summary>
    /// Removes attack from the equipped attacks
    /// </summary>
    /// <param name="attack">Attack to remove</param>
    public void Remove (Attack attack)
    {
        attacks.Remove(attack); //Removes the attack

        if (onAttackChangedCallback != null) //Checks so that the UI change callback exists
        {
            onAttackChangedCallback.Invoke(); //Invokes the UI change callback so that the UI can be updated
        }
    }

    public void Awake()
    {
        attackInstance = this; //Sets the instance of the attack menu to this so that it may be called upon by other scripts
    }
    // Use this for initialization
    void Start ()
    {
        if (attacks.Count <= 0) //Checks if there are no equipped attacks
        {
            attacks.Add(basicAttack); //Adds the default attack to the equipped attacks
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        if (attacks.Count <= 0) //Checks if there are no equipped attacks
        {
            attacks.Add(basicAttack); //Adds the default attack to the equipped attacks
        }
    }
}
