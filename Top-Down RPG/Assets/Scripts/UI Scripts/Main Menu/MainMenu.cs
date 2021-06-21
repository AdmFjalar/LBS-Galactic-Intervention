using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator animator;

    /// <summary>
    /// Tells the animator that the menu has been loaded
    /// </summary>
    public void Loaded()
    {
        animator.SetBool("Loaded", true);    
    }

    public void Start()
    {
        PlayerPrefs.SetInt("ChangingLevel", 1); //Sets that the level should be changed
        PlayerPrefs.SetInt("LoadingLevel", 1); //Sets that a level should be loaded
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit(); //Closes the game
    }

    /// <summary>
    /// Removes all playerpref values
    /// </summary>
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); //Deletes all the playerpref values
    }
}
