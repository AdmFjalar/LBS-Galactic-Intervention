using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //Sets the class to serializable so that it can be edited directly from the dialogue containers
public class Dialogue {

	public string name; //The name of the character/item
    public string title; //The title of the character/item
    public bool enemy; //Bool representing if the character/item is an enemy
    public bool dying; //Bool representing if the character/item is dying
    public bool firstEncounter;
    public bool trader; //Bool representing if the character/item is a trader

    //  public Sprite icon = null;

    [TextArea(1, 3)] //How big the textbubble is for writing the dialogue
	public string[] sentences; //The sentences which will be uttered by the character during dialogue
    public Sprite[] icons; //The portraits for the dialogue
    public AudioClip battleMusic; //Music played when fighting against NPC if they're an enemy
    public string[] deathSentences; //The sentences which will be uttered by the character when the character is dying
    public Sprite[] deathIcons; //The portraits for the death dialogue
}
