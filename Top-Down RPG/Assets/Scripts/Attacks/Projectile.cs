using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float distance; //The distance between the projectile spawner and the target
    private bool hasCrashed;

    public float speed = 5f; //The speed of the projectile

    public GameObject target; //Target gameobject
    public GameObject spawner; //Gameobject that spawned the projectile

    public EnemyController enemyController; //The script that controls the enemy

    public CharacterStats charStats; //The stats of the spawner gameobject

    public AudioClip crashSound; //Sound that plays when the projectile hits the target

    public Animator animator;

    void Start()
    {
        spawner = GameObject.FindWithTag("Enemy"); //Sets spawner to the active enemy
        target = GameObject.FindGameObjectWithTag("Player"); //Sets the target to the player
        enemyController = spawner.GetComponent<EnemyController>(); //Sets the enemycontroller to the script on the spawner gameobject
        charStats = spawner.GetComponent<CharacterStats>(); //Sets the character stats to the spawner stats
        distance = spawner.transform.position.x - target.transform.position.x; //Sets the distance variable to the distance between the spawner and target
    }

    void Update()
    {
        if (spawner == null) //Checks if there is not a spawner
        {
            spawner = GameObject.FindWithTag("Enemy"); //Sets the spawner to the active enemy
        }

        if (target == null) //Checks if there is not a target
        {
            target = GameObject.FindWithTag("Player"); //Sets the target to the player
        }

        if (enemyController == null) //Checks if there is no enemy controller
        {
            enemyController = spawner.GetComponent<EnemyController>(); //Sets the enemy controller to the spawner controller
        }

        if (distance == 0) //Checks if the distance is 0
        {
            distance = spawner.transform.position.x - target.transform.position.x; //Sets the distance to the distance between the spawner and target
        }

        if (transform.position.x - target.transform.position.x < 2 && enemyController != null && !hasCrashed) //Checks if the projectile has reached the target and that the enemy controller is not null
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetBool("Crashing", true);
            hasCrashed = true;
        }

        if (transform.position == target.transform.position)
        {
            charStats.audioSource.clip = crashSound; //Sets the sound of the audioplayer to the crash sound
            charStats.audioSource.Play(); //Plays the crash sound
        }

        float step = speed * Time.deltaTime; //Sets the speed of the projectile
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step); //Moves the projectile towards the target
    }

    /// <summary>
    /// Uses the attack
    /// </summary>
    public void Attack()
    {
        enemyController.UseAttack(); //Calls the useattack method from the enemy controller so that the effects of the attack can be used on the target
        Destroy(gameObject); //Removes the projectile
    }
    /*
    void FixedUpdate()
    {
        if (transform.position.x > target.transform.position.x)
        {
            transform.position += spawner.transform.position + new Vector3(speed * -1f, speed * Mathf.Sin(2f * Mathf.PI / (distance * ((spawner.transform.position.x + transform.position.x)))), 0f);
        }
    }*/
}
