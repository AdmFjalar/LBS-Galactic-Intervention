using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

    public Animator animator; //The animator of the entity

    public AudioSource audioSource; //The audiosource on the entity

    public AudioClip damageSound; //The soundeffect when the entity takes damage
    public AudioClip deathSound; //The soundeffect when the entity dies

    public bool initiated = false; //Used when spawning to heal to max health
    public bool dying = false; //Shows if the character is dying, used by other scripts

    public int maxHealth { get; private set; } //Max health of the entity
    public int currentHealth { get; set; } //The current health of the entity

    public int maxMana { get; private set; } //Max mana of the entity
    public int currentMana { get; set; } //The current mana of the entity

    public int xp { get; set; } //The current xp of the entity
    public int neededXp { get; private set; } //The amount of xp to level up

    public int level { get; set; } //The level of the entity

    public int rand; //The random value used to calculate if it was a critical hit

    public int skillPoints; //The skillpoints of the entity

    public int healthModifier = 1; //The health modifier of the entity
    public int manaModifier = 1; //The mana modifier of the entity
    public int damageModifier = 1; //The damage modifier of the entity

    public int minRange = 1; //The minimum level the entity will be if it is not the player
    public int maxRange = 10; //The maximum level the entity will be if it is not the player

    public Stat damage; //The basedamage of the entity
    public Stat armor; //The basearmour of the entity

    public GameObject poof; //The effect used when the entity is dying
    public GameObject slash; //The effect used when the entity is damaged
    public GameObject damageTextObj; //The gameobject of the text where the amount of damage dealt to the entity will be displayed
    public Text damageText; //The text where the amount of damage dealt to the entity will be displayed

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>(); //Sets the animator to the animator component on the entity
        audioSource = gameObject.GetComponent<AudioSource>(); //Sets the audiosource to the audiosource component on the entity
        if (level <= 0 && gameObject.tag == "Player") //Checks if the level is less or equal to 1 and if the tag is player
        {
            level = 1; //Sets the level to 1
        }
        else if (level <= 0 && gameObject.tag != "Player") //Checks if the level is less or equal to 1 and if the tag is not player
        {
            level = Random.Range(minRange, maxRange + 1); //Sets the level to a random value between the min and max range
            healthModifier += level-1; //Adds the level of the entity to the healthmodifier
        }

        maxHealth = healthModifier * 20; //Sets the maxhealth to 20 times the health modifier

        maxMana = manaModifier * 20; //Sets the maxmana to 20 times the mana modifier
    }

    void LateUpdate()
    {
        if (initiated == false) //Checks if the entity is initiated
        {
            currentHealth = maxHealth; //Sets the health to max health
            currentMana = maxMana; //Sets the mana to max mana

            initiated = true; //Sets that the entity has been initiated
        }
    }

    void Update()
    {
        if (gameObject.tag == "Player") //Checks if the entity's tag is player
        {
            damageTextObj = GameObject.FindWithTag("PlayerTDC"); //Sets the damagetext object to the gameobject with the tag playertdc
            if (damageTextObj != null) //Checks if the damagetext object is not null
            {
                damageText = damageTextObj.GetComponent<Text>(); //Sets the damagetext to the text component on the damagetext object
            }
        }
        else if (gameObject.tag == "Enemy") //Checks if the entity's tag is enemy
        {
            damageTextObj = GameObject.FindWithTag("EnemyTDC"); //Sets the damagetext object to the gameobject with the tag enemytdc
            if (damageTextObj != null) //Checks if the damagetext object is not null
            {
                damageText = damageTextObj.GetComponent<Text>(); //Sets the damagetext to the text component on the damagetext object
            }
        }
        if (currentHealth < 0) //Checks if the current health is less than 0
        {
            currentHealth = 0; //Sets the health to 0
        }
        maxHealth = healthModifier * 20; //Sets the max health to 20 times the health modifier
        maxMana = manaModifier * 20; //Sets the max mana to 20 times the mana modifier
        neededXp = 50;

        if (xp >= neededXp) //Checks if the entity's xp is more than
        {
            xp -= neededXp; //Removes the needed xp from the entity's xp 
            level++; //Adds a level to the entity
            skillPoints++; //Adds a skillpoint to the entity
            GameObject.FindObjectOfType<NPCManager>().gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
    
    /// <summary>
    /// Takes away health from the entity
    /// </summary>
    /// <param name="damage">Amount of damage that will be dealt</param>
    public void TakeDamage (int damage)
    {
        Instantiate(slash, transform.position, Quaternion.identity); //Spawns the slash effect
        audioSource.clip = damageSound; //Sets the clip of the audiosource to the damage soundeffect
        audioSource.Play(); //Plays the soundeffect
        if (animator != null) //Checks if there is an animator on the entity
        {
            animator.SetBool("TakingDamage", true); //Starts the damage animation of the entity
        }
        damage -= armor.GetValue(); //Takes away the armor value of the entity from the damage dealt
        damage = Mathf.Clamp(damage, 1, int.MaxValue); //Locks the damage between 1 and the int max value

        rand = Random.Range(0, 10); //Sets rand to a value between 0 and 9

        if (rand == 0) //Checks if rand is equal to 0
        {
            damage *= 5; //Multiplies the damage by 5
        }

        StartCoroutine(PrintDamage(damage)); //Starts the co-routine to print out the damage dealt

        if (currentHealth - damage >= 0) //Checks if the entity's health will be greater or equal to 0 when removing the damage from the health
        {
            currentHealth -= damage; //Removes the damage from the entity's health
        }
        else if (currentHealth - damage < 0) //Checks if the damage will cause the entity to go under 0 health
        {
            currentHealth = 0; //Sets the health of the entity to 0
        }
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0) //Checks if the entity's health is less or equal to 0
        {
            StartDeathProcess(); //Starts the death process
            return;
        }
    }

    /// <summary>
    /// Stops the damage animation
    /// </summary>
    public void StopDamage ()
    {
        if (animator != null) //Checks if there is an animator attached
        {
            animator.SetBool("TakingDamage", false); //Stops the damage animation
        }
    }

    /// <summary>
    /// Starts the death process, including death soundeffect and the death animation
    /// </summary>
    public void StartDeathProcess()
    {
        audioSource.clip = deathSound; //Sets the clip of the audiosource to the death soundeffect
        audioSource.Play(); //Plays the death soundeffect
        if (gameObject.tag != "Player") //Checks if the tag is not player
        {
            DialogueTrigger dt = gameObject.GetComponent<DialogueTrigger>(); //Sets a reference to the dialogue trigger of the entity
            dt.Dying(); //Sets that the entity is dying
            dt.TriggerDeathDialogue(); //Starts the death dialogue
        }
        else if (gameObject.tag == "Player") //Checks if the entity tag is player
        {
            InitiateDeath(); //Calls the initiatedeath method
        }
    }

    /// <summary>
    /// Starts the death animation
    /// </summary>
    public virtual void InitiateDeath ()
    {
        dying = true; //Shows that the character is dying
        //Die in some way
        if (animator != null) //Checks if the entity has an animator attached
        {
            animator.SetBool("Dying", true); //Starts the death animation
        }
        else if (animator == null) //Checks if the entity does not have an animator
        {
            FinishDeath(); //Calls the finishdeath method
        }
    }

    /// <summary>
    /// The final step when dying, removing the gameobject and spawning the smoke effect
    /// </summary>
    public virtual void FinishDeath()
    {
            Debug.Log(transform.name + " died.");
        if (animator != null) //Checks if the entity has an animator attached
        {
            Instantiate(poof, transform.position, Quaternion.identity); //Spawns the smoke/poof effect
            //animator.SetBool("Dying", false); //Stops the death animation
        }
    }

    /// <summary>
    /// Prints out the damage dealt to the entity in the damagetext window
    /// </summary>
    /// <param name="damage">Amount of damage dealt</param>
    /// <returns>Time to wait before removing text</returns>
    IEnumerator PrintDamage(int damage)
    {
        if (rand != 0) //Checks if it was not a critical hit
        {
            damageText.text = ""; //Sets the text to null
            damageText.color = Color.black; //Changes the textcolor
            damageText.text = "-" + damage + "HP"; //Sets the text to show how much health was removed
            yield return new WaitForSeconds(0.75f); //Waits for 0.75 seconds
            damageText.text = ""; //Sets the text to null
            StopAllCoroutines(); //Stops all co-routines
        }
        else if (rand == 0) //Checks if it was a critical hit
        {
            damageText.text = ""; //Sets the text to null
            damageText.color = Color.red; //Changes the text color
            damageText.text = "CRITICAL HIT! -" + damage + "HP"; //Sets the text to show how much health was removed
            yield return new WaitForSeconds(0.75f); //Waits for 0.75 seconds
            damageText.text = ""; //Sets the text to null
            StopAllCoroutines(); //Stops all co-routines
        }
    }
}
