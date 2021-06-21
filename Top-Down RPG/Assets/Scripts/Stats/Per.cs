using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Per : MonoBehaviour
{
    public CharacterStats myStats; //The characterstats of the entity
    public AudioSource audioSource; //The audio source on the entity
    public AudioClip deathAnimationSound; //The sound playing when the character is dying

    // Start is called before the first frame update
    void Start()
    {
        myStats = gameObject.GetComponent<CharacterStats>(); //Sets the characterstats to the characterstats component on the entity 
        audioSource = gameObject.GetComponent<AudioSource>(); //Sets the audiosource to the audiosource component on the entity
    }

    // Update is called once per frame
    void Update()
    {
        if (myStats.dying) //Checks if the character is dying
        {
            audioSource.clip = deathAnimationSound; //Sets the audioclip of the character to the deathAnimationSound
            audioSource.Play(); //Plays the soundeffect

            Destroy(this); //Removes the script
        }
    }
}
