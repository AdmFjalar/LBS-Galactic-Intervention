using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    CharacterCombat combat; //The enemy character combat
    CharacterStats stats; //The enemy stats
    CharacterStats targetStats; //The player stats/target stats

    public Animator animator; //Animator of enemy entity

    public int attackChoice; //Choice of attack

    public GameObject player; //Player gameobject
    public GameObject[] projectile; //Projectile to spawn

    public float timer; //Variable used to delay the use of attack to give a sense of the enemy thinking over their choices

    public bool finishedAttack = true; //Checks whether the attack has been used and is finished

    public Attack[] attacks; //The enemy's choices of attacks

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>(); //Animator of enemy
        combat = GetComponent<CharacterCombat>(); //The character combat script of the enemy
        stats = GetComponent<CharacterStats>(); //The character stats script of the enemy
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerPrefs.GetInt("InPauseMenu") == 0) //Checks whether the player is in the pause menu
        {
            if (PlayerPrefs.GetInt("InFight") == 0) //Checks whether the player is in the fight menu
            {
                gameObject.tag = "Interactable"; //Sets the tag of the gameobject to interactable
            }
            if (player == null) //Checks if the player is null
            {
                player = GameObject.FindWithTag("Player"); //Sets the player to the gameobject with the tag 
            }

            if (player != null && targetStats == null) //Checks if the player is not null and if the target stats is null
            {
                targetStats = player.GetComponent<CharacterStats>(); //Sets the target stats to the character stats script on the player
            }
            if (PlayerPrefs.GetInt("InFight") == 1 && gameObject.tag == "Enemy") //Checks if player is in fight and if the current tag is enemy
            {
                animator.SetBool("InBattle", true); //Sets the animator in battle to true
            }
            if (PlayerPrefs.GetInt("Turn") == 2 && PlayerPrefs.GetInt("InFight") == 1 && gameObject.tag == "Enemy" && finishedAttack == true && stats.currentHealth > 0) //Checks if it is turn 2 and if player is in fight and if the current tag is enemy and the attack is finnished and the enemy has more than 0 health
            {
                if (timer < 2) //Checks if timer is lower than 2
                {
                    timer += Time.deltaTime; //Adds deltatime to timer
                }

                if (timer >= 1 && targetStats != null) //Checks if timer is equal or larger than 1 and if targetstats is not null
                {
                    timer = 0; //Sets timer to 0

                    a: //A point to return to (Sorry for using goto :()

                    finishedAttack = false; //Sets finishedattack to false

                    attackChoice = Random.Range(0, attacks.Length); //Sets attackchoice to a random value between 0 and the amount of attacks

                    animator.SetInteger("UsingAttack", (attackChoice + 1)); //Starts the attack animation

                    if (stats.currentMana - attacks[attackChoice].manaCost >= 0) //Checks if the enemy affords a certain attack
                    {
                        animator.SetInteger("UsingAttack", (attackChoice + 1)); //Starts the attack animation
                    }
                    else
                    {
                        goto a; //Goes to the point a
                    }
                }
            }

            else if ((PlayerPrefs.GetInt("Turn") == 1 && PlayerPrefs.GetInt("InFight") == 0) || PlayerPrefs.GetInt("InFight") == 0 && animator != null) //Checks if it is turn 1 and player is not in battle and if the entity has an animator attached
            {
                animator.SetBool("InBattle", false); //Sets the enemy animations to the regular idle animations
            }
        }
	}

    /// <summary>
    /// Exits attack animation and reenters idle animation
    /// </summary>
    public void ExitAnimation()
    {
        animator.SetInteger("UsingAttack", 0); //Sets the enemy to idle again, exiting the attack animation
    }

    /// <summary>
    /// Starts the attack animation and uses attack on target
    /// </summary>
    public void UseAttack()
    {
        if (PlayerPrefs.GetInt("Turn") == 2) //Checks if it is turn 2
        {
            finishedAttack = true; //Sets that the attack is finished
            attacks[attackChoice].Use(gameObject); //Uses the chosen attack
            PlayerPrefs.SetInt("UsingAttack", 1); //Starts the attack animation
        }
    }

    /// <summary>
    /// Spawns chosen projectile
    /// </summary>
    public void SpawnProjectile()
    {
        Instantiate(projectile[attackChoice], new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity); //Spawns the projectile besides the spawner
    }
}
