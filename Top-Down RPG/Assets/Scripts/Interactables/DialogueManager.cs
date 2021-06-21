using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public Slider slider; //Slider to show how much of the current sentence that has been printed
    public float sentenceProgress; //A number between 0 and 1 representing how much of the current sentence has been printed

    private string stringToPrint = ""; //The string which will be printed

    public GameObject levelLoader;

    public TextMeshProUGUI nameText; //The text showing the character name in battle
    public TextMeshProUGUI nameText2; //The text showing the character name in dialogue
    public TextMeshProUGUI nextButton; //The text on the "next" button
    public string nameString; //Name of speaking character
    public string title; //Title of speaking character
    public bool enemy; //Bool representing if the character/item is an enemy
    public bool dying; //Bool representing if the character/item is dying
    public bool firstEncounter;
    public bool trader; //Bool representing if the character/item is a trader
    public TextMeshProUGUI dialogueText; //The textbubble where the sentences will be printed
    public string sentence; //The current sentence

    public AudioClip battleMusic; //The music played during battles
    public Animator battleScreen; //The animator of the battle screen
    public TextMeshProUGUI battleNameText; //The name text for entering battles
    public TextMeshProUGUI battleTitleText; //The title text for entering battles
    public Image battleIcon; //The image for entering battles

    public float pLength; //Number of printed characters
    public float sLength; //Total number of characters in sentence

    private int i; //Iteration variable used in multiple places
    private int arrayLength; //Number of portraits for dialogue

    public float printDelay; //How much time is inbetween printing each character

    private Sprite[] icons; //The portraits for the dialogue

    public AudioClip dialogueSound; //The sound used when printing out characters
    public AudioSource dialogueSoundSource; //The audiosource for the dialogue sounds
    public GameObject StatsMenu; //The menu listing all the stats of the enemy

    public Dialogue _dialogue;

    private List<char> soundingChars = new List<char> { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z', 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; //The characters which makes a sound when printed. Not every character makes a sound since it sounds a bit too metal
    //private int inPauseMenu

    public Image icon; //The current portrait used

    public Animator animator; //The animator for the dialogue window

	private Queue<string> sentences; //All the sentences for the characters that are left

	void Start () {
		sentences = new Queue<string> (); //Instantiates the sentence queue
	}

    private void Update()
    {
            slider.value = sentenceProgress; //Sets the slider value to the sentenceprogress to show how much of the current sentence that is finished
            char[] printedChars = stringToPrint.ToCharArray(); //A char array with all the printed characters

            pLength = printedChars.Length; //Sets the pLength to the amount of characters in printed characters

            sLength = sentence.Length; //Sets the slength to the total sentence length

            sentenceProgress = pLength / sLength; //Sets the sentence progress to plength divided by slength to get a value between 0 and 1
            if (sentenceProgress != 1f) //Checks if the sentence progress is not equal to 1
            {
                nextButton.text = "SKIP"; //Set the text on the "next" button the "SKIP"
            }
            else if (sentenceProgress == 1f) //Checks if the sentence progress is equal to 1
            {
                nextButton.text = "NEXT >>"; //Sets the text on the "next" button the "NEXT >>"
            }
            if (PlayerPrefs.GetInt("InFight") == 1 && StatsMenu != null) //Checks if player is in fight
            {
                StatsMenu.SetActive(true); //Sets the stats menu to active, showing the enemy stats
            }
            else if (PlayerPrefs.GetInt("InFight") != 1 && StatsMenu != null) //Checks if the player is not in a fight
            {
                StatsMenu.SetActive(false); //Sets the stats menu to inactive
            }
            if (PlayerPrefs.GetInt("InDialogue") == 1 && Input.GetKeyDown(KeyCode.Space)) //Checks if the player is in a dialogue and if space is pressed
            {
                NextOrDeath(); //Calls the next or death method to see which sentence it should print next
            }
    }

    /// <summary>
    /// Starts the dialogue with the given dialogue
    /// </summary>
    /// <param name="dialogue">Dialogue to start</param>
    public void StartDialogue (Dialogue dialogue)
    {
        PlayerPrefs.SetInt("InDialogue", 1); //Sets that the player is in a dialogue
        i = -1; //Sets the iteration variable to -1
        Debug.Log("Starting conversation with " + dialogue.name);

        icons = dialogue.icons; //Sets the icons to the portraits of the character

        stringToPrint = ""; //Empties the stringToPrint
        dialogueText.text = ""; //Empties the dialogue text
        arrayLength = icons.Length; //Sets arraylength to the amount of portraits in the dialogue

        nameText.text = dialogue.name; //Sets the nametext to the name of the character
        nameText2.text = dialogue.name; //Sets the nametext2 to the name of the character
        nameString = dialogue.name; //Sets the nameString to the name of the character
        title = dialogue.title; //Sets the title to the title of the character

        battleMusic = dialogue.battleMusic; //Sets the battle music to the music from the character

        dying = dialogue.dying; //Sets the dying bool to the dying bool of the character
        trader = dialogue.trader; //Sets the trader bool to the trader bool of the character
        firstEncounter = dialogue.firstEncounter;
        enemy = dialogue.enemy; //Sets the enemy bool to the enemy bool of the character
        sentences.Clear(); //Empties all previous sentences
        sentence = ""; //Empties sentence

        _dialogue = dialogue;

        animator.SetBool("IsOpen", true); //Sets the IsOpen bool to true which in turn opens the dialogue window

        foreach (string sentence in dialogue.sentences) //Goes through every sentence in the character
        {
            sentences.Enqueue(sentence); //Adds the sentence to the sentence queue
        }

        DisplayNextSentence(); //Displays the next sentence
    }

    /// <summary>
    /// Starts the death dialogue. This is called when the character is dying
    /// </summary>
    /// <param name="dialogue">The death dialogue to start</param>
    public void DeathDialogue (Dialogue dialogue)
    {
        PlayerPrefs.SetInt("InDialogue", 1); //Sets that the player is in a dialogue
        i = -1; //Sets the iteration variable to -1

        icons = dialogue.deathIcons; //Sets the icons to the portraits of the character

        stringToPrint = ""; //Empties the stringToPrint
        dialogueText.text = "";//Empties the dialogue text
        arrayLength = icons.Length; //Sets arraylength to the amount of portraits in the dialogue

        nameText.text = dialogue.name; //Sets the nametext to the name of the character
        nameText2.text = dialogue.name; //Sets the nametext2 to the name of the character
        nameString = dialogue.name; //Sets the nameString to the name of the character

        dying = dialogue.dying; //Sets the dying bool to the dying bool of the character
        enemy = dialogue.enemy; //Sets the enemy bool to the enemy bool of the character
        sentences.Clear(); //Empties all previous sentences
        sentence = ""; //Empties sentence
        animator.SetBool("IsOpen", true); //Sets the IsOpen bool to true which in turn opens the dialogue window

        foreach (string sentence in dialogue.deathSentences) //Goes through every sentence in the character
        {
            sentences.Enqueue(sentence); //Adds the sentence to the sentence queue
        }

        DisplayNextDeathSen(); //Displays the next sentence
    }

    /// <summary>
    /// Determines whether it is currently going through a normal or a death dialogue and calls for the respective method
    /// </summary>
    public void DisplayNextDeathSen ()
    {
        if (i + 1 < arrayLength && stringToPrint == sentence) //Checks if there are more portraits than the current iteration variable plus 1 and if the stringToPrint is equal to the sentence
        {
            i++; //Adds one to the iteration variable

            icon.sprite = icons[i]; //Sets the current portrait to the portrait of the iteration variable
        }

        if (sentences.Count == 0 && stringToPrint == sentence) //Checks if there are 0 sentences left and if stringToPrint is equal to sentence
        {
            EndDeath(); //Calls the EndDeath method to start the correct animation and initiate the death process for the character
            return;
        }

        if (stringToPrint == sentence) //Checks if the stringToPrint is equal to the sentence
        {
            stringToPrint = ""; //Empties stringToPrint
            sentence = sentences.Dequeue(); //Sets sentence to the next sentence in the sentence queue
            StopAllCoroutines(); //Stops all co-routines
            StartCoroutine(TypeSentence(sentence)); //Starts the printing co-routine and feeds in the sentence
        }
        else if (dialogueText.text != sentence) //Checks if the dialogueText is not equal to the sentence
        {
            StopAllCoroutines(); //Stops all co-routines
            stringToPrint = sentence; //Sets stringToPrint equal to sentence
            dialogueText.text = stringToPrint; //Sets dialogueText to stringToPrint
            return;
        }
    }

    /// <summary>
    /// Goes to the next sentence in the dialogue
    /// </summary>
    public void DisplayNextSentence ()
    {
        if (i + 1 < arrayLength && stringToPrint == sentence) //Checks if there are more portraits than the current iteration variable plus 1 and if the stringToPrint is equal to the sentence
        {
            i++; //Adds one to the iteration variable

            icon.sprite = icons[i]; //Sets the current portrait to the portrait of the iteration variable
        }

        if (sentences.Count == 0 && enemy == false && stringToPrint == sentence && trader == false) //Checks if there are 0 sentences left and if stringToPrint is equal to sentence and the character is not a trader nor an enemy
        {
            EndDialogue(); //Ends Dialogue
            return;
        }
        else if (sentences.Count == 0 && enemy == true && stringToPrint == sentence) //Checks if the stringToPrint is equal to the sentence and there are no more sentences to print and if the character is an enemy
        {
            Debug.Log("Starting fight with " + nameString);

            battleIcon.sprite = icon.sprite; //Changes the battle icon to the NPC's icon
            battleNameText.text = nameString; //Changes the battle name text to the NPC's name
            battleTitleText.text = title; //Changes the battle title text to the NPC's title
            PlayerPrefs.SetInt("BattleScreen", 1); //Sets it so the battle screen is active and the player can't move
            GameManager.instance.audioSource.clip = battleMusic; //Sets the audioclip of the gamemanager to the battlemusic
            GameManager.instance.audioSource.Play(); //Starts playing the battlemusic
            battleScreen.gameObject.SetActive(true); //Turns on the battle screen
            battleScreen.SetBool("EnteringBattle", true); //Tells the animator that the player is entering battle

            EndDialogue(); //Ends dialogue
            return;
        }
        else if (sentences.Count == 0 && trader == true && stringToPrint == sentence && firstEncounter == false) //Checks if there are no more sentences to print and if the character is a trader and if stringToPrint is equal to sentence
        {
            Debug.Log("Entering shop of " + nameString);
            PlayerPrefs.SetInt("InShop", 1); //Sets the player to enter the shop
            EndDialogue(); //Ends dialogue
            return;
        }
        else if (sentences.Count == 0 && trader == true && stringToPrint == sentence && firstEncounter == true)
        {
            EndDialogue(); //Ends Dialogue
            return;
        }

        if (dialogueText.text == sentence) //Checks if the dialogue text is equal to the sentence
        {
            stringToPrint = ""; //Empties stringToPrint
            sentence = sentences.Dequeue(); //Sets sentence to the next sentence in the sentence queue
            StopAllCoroutines(); //Stops all co-routines
            StartCoroutine(TypeSentence(sentence)); //Starts the printing co-routine and feeds in the sentence
        }
        else if (dialogueText.text != sentence) //Checks if the dialogue text is not equal to the sentence
        {
            StopAllCoroutines(); //Stops all co-routines
            stringToPrint = sentence; //Sets stringToPrint equal to sentence
            dialogueText.text = stringToPrint; //Sets dialogue text equal to string to print
            return;
        }
    }

    /// <summary>
    /// Prints out given sentence character for character
    /// </summary>
    /// <param name="sentence">Sentence to print</param>
    /// <returns></returns>
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = ""; //Empties the dialogue text
        foreach (char letter in sentence.ToCharArray()) //Goes through every character in the sentence
            {
            if (soundingChars.Contains(letter)) //Checks if the letter should make a sound
            {
                dialogueSoundSource.Stop(); //Stops the audiosource if its already playing something
                dialogueSoundSource.clip = dialogueSound; //Sets the audio clip to the dialogue sound
                dialogueSoundSource.Play(); //Plays the dialogue sound
            }
                stringToPrint += letter; //Adds the letter to stringToPrint

                dialogueText.text = stringToPrint; //Sets dialogue text equal to stringToPrint

            if (letter != '.' && letter != '?' && letter != '!' && letter != ',') //Checks if the letter is one of the following; . ? !
            {
                yield return new WaitForSeconds(printDelay); //Waits for printDelay seconds until continuing printing
            }
            else if (letter == ',') //Checks if the letter is ,
            {
                yield return new WaitForSeconds(0.5f); //Waits for 0.5 seconds until continuing printing
            }
            else if (letter == '.' || letter == '?' || letter == '!') //Checks if the letter is not one of the following; . ? !
            {
                yield return new WaitForSeconds(1); //Waits for 1 second until continuing printing
            }
            }
    }

    /// <summary>
    /// Ends dialogue and closes the dialogue window
    /// </summary>
    public void EndDialogue()
    {
        _dialogue.firstEncounter = false;
        PlayerPrefs.SetInt("InDialogue", 0); //Sets that the player is no longer in dialogue
        animator.SetBool("IsOpen", false); //Closes the dialogue window

        if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            if (levelLoader != null)
            {
                levelLoader.SetActive(true);
                levelLoader.GetComponent<LoadScene>().LoadDemo();
            }
        }
    }

    /// <summary>
    /// Starts the death process and death animation for the enemy
    /// </summary>
    public void EndDeath()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy"); //Sets the enemy gameobject to the gameobject with the enemy tag
        CharacterStats enemyStats = enemy.GetComponent<CharacterStats>(); //Sets the enemy stats the stats on the enemy gameobject

        enemyStats.InitiateDeath(); //Calls the initiatedeath method in the enemy stats to start the animation

        PlayerPrefs.SetInt("InDialogue", 0); //Sets that the player is no longer in dialogue
        animator.SetBool("IsOpen", false);
    }

    /// <summary>
    /// Decides whether it should display the next death sentence or normal sentence
    /// </summary>
    public void NextOrDeath ()
    {
        if (dying == false) //Checks if dying is false
        {
            DisplayNextSentence(); //Displays next sentence
        }

        else if (dying == true) //Checks if dying is true
        {
            DisplayNextDeathSen(); //Displays the next death sentence
        }
    }
}
