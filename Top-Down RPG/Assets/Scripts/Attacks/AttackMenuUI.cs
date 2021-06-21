using UnityEngine;

public class AttackMenuUI : MonoBehaviour {

    public Transform attacksParent; //Parent gameobject to all attack slots
    //public GameObject attackUI;

    AttackMenu attackMenu; //Reference to the attack menu

    AttackMenuSlot[] slots; //An array of all slots

    bool updatedUI = false; //Checks if the UI has been updated

	// Use this for initialization
	void Start () {
        attackMenu = AttackMenu.attackInstance; //Sets attack menu reference to the attack menu instance
        attackMenu.onAttackChangedCallback += UpdateUI; //UpdateUI will be called every time onAttackChangedCallback is invoked

        slots = attacksParent.GetComponentsInChildren<AttackMenuSlot>(); //Slots are set to the children gameobjects of the parent
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerPrefs.GetInt("InFight") == 1 && updatedUI == false) //Checks if player is in fight and if the UI has not been updated
        {
            UpdateUI(); //Updates UI
            updatedUI = true; //Sets that the UI has been updated
        }
        if (PlayerPrefs.GetInt("InFight") == 0 && updatedUI == true) //Checks if the player is not in a fight and if the UI has been updated
        {
            updatedUI = false; //Sets that the UI has not been updated
        }
    }


    /// <summary>
    /// Updates the Fight Menu UI
    /// </summary>
    void UpdateUI()
    {
        Debug.Log("Updating fightmenu UI");
        for (int i = 0; i < slots.Length; i++) //Goes through every slot
        {
            if (i < attackMenu.attacks.Count) //Checks if the current itteration value is lower than the amount of equipped attacks
            {
                slots[i].AddAttack(attackMenu.attacks[i]); //Adds the equipped attack from the attack menu to the current itteration value
            }
            else
            {
                slots[i].ClearSlot(); //Empties slot
            }
        }
    }
}
