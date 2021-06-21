using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<DialogueTrigger>().dialogue.name != "") //Checks if the name in the dialogue is not empty
        {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(); //Triggers the dialogue
            Destroy(gameObject); //Removes this gameobject
        }
    }
}
