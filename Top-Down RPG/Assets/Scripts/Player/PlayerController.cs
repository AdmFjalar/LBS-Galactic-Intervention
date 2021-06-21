using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public enum MovementPriority { north, south, east, west }; //Represents which direction the player should face and possibly move in
    public MovementPriority movePriority = MovementPriority.north; //The instance of the player movement priority is set to north as default
    public MovementPriority direction = MovementPriority.north; //The instance of the player direction is set to north as default

    public bool[] dirCollisions = new bool[4]; //Bool array where each element represents if there is a collision in the respective direction where the indexes of the directions is as the following: 0=north   1=south   2=east   3=west

    public bool moving = false; //Represents if the player is currently moving

    public Scene scene; //The current scene in which the player is
	public GameObject target; //The target which the player will move towards
	public GameObject Target; //The prefab of the target object
	public int speed; //The movement speed of the player
    public Animator animator;

	void Start () 
	{
        animator = gameObject.GetComponent<Animator>();
        PlayerPrefs.SetInt("InFight", 0); //Sets that the player is not in a fight when entering the game
        PlayerPrefs.SetInt("BattleScreen", 0); //Sets the battlescreen as inactive
        target = Instantiate(Target, transform.position, Quaternion.identity); //Instantiates the target which the player will move towards
        scene = SceneManager.GetActiveScene(); //Sets the scene as the current scene
        PlayerPrefs.SetInt("LoadingScene", 1); //Sets that the player is currently loading the scene to start all effects for when entering a scene
		PlayerPrefs.SetInt("InDialogue", 0); //Sets that the player is not in a dialogue when entering the game
	}
	
	void Update () 
	{
        if (PlayerPrefs.GetInt("InPauseMenu") == 0) //Checks if the player is not in the pause menu
        {
            if (PlayerPrefs.GetInt("InFight") == 1)
            {
                animator.SetBool("InBattle", true);
            }
            else
            {
                animator.SetBool("InBattle", false);
            }

            switch (direction)
            {
                case MovementPriority.north:
                    animator.SetInteger("Direction", 0);
                    break;
                case MovementPriority.south:
                    animator.SetInteger("Direction", 1);
                    break;
                case MovementPriority.east:
                    animator.SetInteger("Direction", 2);
                    break;
                case MovementPriority.west:
                    animator.SetInteger("Direction", 3);
                    break;
            }
            if (target == null) //Checks if the target is null
            {
                target = GameObject.FindWithTag("Target"); //Sets the target to the gameobject with the target tag
            }
            if (PlayerPrefs.GetInt("InShop") == 0 && PlayerPrefs.GetInt("InFight") == 0 && PlayerPrefs.GetInt("InDialogue") == 0 && PlayerPrefs.GetInt("BattleScreen") == 0 && target != null) //Checks if the player is not in a shop and not in a fight and not in a dialogue and the target is not null
            {
                switch (Input.inputString) //Checks which button was pressed most recently
                {
                    case "w":
                        movePriority = MovementPriority.north; //Sets that the player is facing north
                        break;
                    case "s":
                        movePriority = MovementPriority.south; //Sets that the player is facing south
                        break;
                    case "d":
                        movePriority = MovementPriority.east; //Sets that the player is facing east
                        break;
                    case "a":
                        movePriority = MovementPriority.west; //Sets that the player is facing west
                        break;
                }

                if (transform.position == target.transform.position) //Checks if the player has reached the target
                {
                    moving = false; //Sets that the player is not moving
                    if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                    {
                        animator.SetBool("Moving", false);
                    }
                    PlayerPrefs.SetInt("ReachedTarget", 0); //Sets that the player has reached the target
                }
                else if (transform.position != target.transform.position) //Checks if the player has not reached the target
                {
                    moving = true; //Sets that the player is moving
                    animator.SetBool("Moving", true);
                }

                if (Input.GetKey("w") && dirCollisions[0] == false && target.transform.position == transform.position) //Checks if W is pressed and there is not a collision in that direction and the player has reached the target
                {
                    if (movePriority == MovementPriority.north || Input.GetKey("s") == false && Input.GetKey("a") == false && Input.GetKey("d") == false) //Checks if the movement priority is north or if S is not pressed and A is not pressed and D is not pressed
                    {
                        direction = MovementPriority.north;
                        target.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0); //Moves the target towards the north
                    }
                }

                if (Input.GetKey("s") && dirCollisions[1] == false && target.transform.position == transform.position) //Checks if S is pressed and there is not a collision in that direction and the player has reached the target
                {
                    if (movePriority == MovementPriority.south || Input.GetKey("w") == false && Input.GetKey("a") == false && Input.GetKey("d") == false) //Checks if the movement priority is south or if W is not pressed and A is not pressed and D is not pressed
                    {
                        direction = MovementPriority.south;
                        target.transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0); //Moves the target towards the south
                    }
                }

                if (Input.GetKey("d") && dirCollisions[2] == false && target.transform.position == transform.position) //Checks if D is pressed and there is not a collision in that direction and the player has reached the target
                {
                    if (movePriority == MovementPriority.east || Input.GetKey("w") == false && Input.GetKey("a") == false && Input.GetKey("s") == false) //Checks if the movement priority is east or if W is not pressed and A is not pressed and S is not pressed
                    {
                        direction = MovementPriority.east;
                        target.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0); //Moves the target towards the east
                    }
                }

                if (Input.GetKey("a") && dirCollisions[3] == false && target.transform.position == transform.position) //Checks if A is pressed and there is not a collision in that direction and the player has reached the target
                {
                    if (movePriority == MovementPriority.west || Input.GetKey("w") == false && Input.GetKey("d") == false && Input.GetKey("s") == false) //Checks if the movement priority is west or if W is not pressed and D is not pressed and S is not pressed
                    {
                        direction = MovementPriority.west;
                        target.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0); //Moves the target towards the west
                    }
                }
            }
            if (target != null && PlayerPrefs.GetInt("InFight") == 0) //Checks if the target is not null and if the player is not in a fight
            {
                float step = speed * Time.deltaTime; //Sets step (speed at which the player will be moved) to speed times deltaTime
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step); //Moves the player towards the target
            }
        }
	}

    /// <summary>
    /// Exits attack animation and reenters idle animation
    /// </summary>
    public void ExitAnimation()
    {
        animator.SetInteger("UsingAttack", 0); //Sets the enemy to idle again, exiting the attack animation
        animator.SetBool("TakingDamage", false);
        if (animator.GetBool("Dying"))
        {

        }
    }
}
