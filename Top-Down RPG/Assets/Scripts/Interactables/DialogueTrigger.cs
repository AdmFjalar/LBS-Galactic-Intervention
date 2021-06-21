using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue[] dialogues = new Dialogue[1]; //The set of dialogues the NPC can choose from
	public Dialogue dialogue; //The NPC's main dialogue
    public GameObject Player; //The player gameobject

    /// <summary>
    /// The Dialogue Trigger constructor used to set the dialogue
    /// </summary>
    /// <param name="d">The chosen dialogue which will be the main dialogue</param>
    public DialogueTrigger(Dialogue d)
    {
        dialogue = d; //Sets the dialogue to the entered dialogue
    }

    /// <summary>
    /// Starts the dialogue with the NPC
    /// </summary>
    public void TriggerDialogue()
    {
        if (dialogue.firstEncounter)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else if (!dialogue.firstEncounter)
        {
            if (dialogues.Length > 1)
            {
                int i = Random.Range(1, dialogues.Length); //Selects a random element from the NPC's dialogues
                Debug.Log(dialogues[i].name);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogues[i]); //Sets the Dialogue Manager to start the dialogue with the chosen dialogue
            }
            else
            {
                int i = Random.Range(0, dialogues.Length); //Selects a random element from the NPC's dialogues
                Debug.Log(dialogues[i].name);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogues[i]); //Sets the Dialogue Manager to start the dialogue with the chosen dialogue
            }
        }
    }

    /// <summary>
    /// Starts the dearth dialogue with the NPC
    /// </summary>
    public void TriggerDeathDialogue()
    {
        FindObjectOfType<DialogueManager>().DeathDialogue(dialogue); //Sets the Dialogue Manager to start the death dialogue
    }

    public void Start()
    {
        dialogues[0] = dialogue; //Sets the first element of the dialogues to the main dialogue
        foreach (Dialogue d in dialogues) //Goes through all the dialogues in the NPC's possible dialogues
        {
            if (d.icons.Length <= 0) //Checks if the dialogue does not have any portraits
            {
                d.icons = dialogues[0].icons; //Sets the portraits of the dialogue to the main dialogue's portraits
            }
            d.name = dialogues[0].name; //Sets the name to the name of the main dialogue
            d.enemy = dialogues[0].enemy; //Enters if the NPC is an enemy to the bool of the main dialogue
            d.dying = dialogues[0].dying; //Enters if the NPC is dying to the bool of the main dialogue
            d.trader = dialogues[0].trader; //Enters if the NPC is a trader to the bool of the main dialogue
            d.deathSentences = dialogues[0].deathSentences; //Sets the death sentences to the death sentences of the main dialogue
            d.deathIcons = dialogues[0].deathIcons; //Sets the death portraits to the death portraits of the main dialogue
        }
        Player = GameObject.Find("Player"); //Sets the player reference to the player gameobject
        PlayerPrefs.SetInt("InDialogue", 0); //Sets that the player is not in a dialogue
    }

    public void Update()
    {
        if (Input.GetKeyDown("e") && PlayerPrefs.GetInt("InDialogue") == 0 && PlayerPrefs.GetInt("InFight") == 0 && PlayerPrefs.GetInt("BattleScreen") == 0 && Mathf.Sqrt(Mathf.Pow(Player.transform.position.x-transform.position.x, 2)+Mathf.Pow(Player.transform.position.y-transform.position.y, 2)) <= 1) //Checks if the requirements are met to start a dialogue
        {
            PlayerPrefs.SetInt("InDialogue", 1); //Sets that the dialogue has been started
            TriggerDialogue(); //Triggers the dialogue
        } 
    }

    /// <summary>
    /// Sets that the NPC is dying
    /// </summary>
    public void Dying()
    {
        dialogue.dying = true; //Sets the dying bool to true
    }
}
