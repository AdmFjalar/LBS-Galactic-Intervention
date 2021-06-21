using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public Animator animator; //The animator of the scene loader
    public Scene scene; //The current scene
    string sceneName; //The scene to load
    public GameManager gm; //The gamemanager

    public void Start()
    {
        PlayerPrefs.SetInt("InitiateLevelLoad", 0); //Initiates the level loader
        scene = SceneManager.GetActiveScene(); //Sets the scene to the active scene
    }

    /// <summary>
    /// Fades out the level
    /// </summary>
    /// <param name="sceneName">Scene to load</param>
    public void FadeToLevel(string sceneName)
    {
        //if (SceneManager.GetActiveScene().name == "Demo") //Checks if the current scene is demo
        //{
        //    gm.Save(); //Saves the progress with the gamemanager
        //}
        gameObject.SetActive(true); //Sets scene loader as active
        PlayerPrefs.SetInt("ChangingLevel", 1); //Sets that the scene is being changed
        PlayerPrefs.SetInt("LoadingLevel", 0); //Sets that a scene is being loaded
        animator.SetTrigger("FadeOut"); //Starts the fade animation

        this.sceneName = sceneName; //Sets the scenename to the entered scenename
    }

    /// <summary>
    /// Starts loading the scene
    /// </summary>
    public void OnFadeComplete()
    {
        SceneManager.LoadSceneAsync(sceneName); //Loads the entered scene
    }

    /// <summary>
    /// Deactivates the scene loader
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false); //Sets the gameobject inactive
    }

    /// <summary>
    /// Loads the demo scene
    /// </summary>
    public void LoadDemo()
    {
        FadeToLevel("Demo"); //Starts loading the demo scene
    }

    public void LoadIntro()
    {
        FadeToLevel("IntroScene"); //Starts loading the intro scene
    }

    public void LoadRespawn()
    {
        FadeToLevel("Respawn");
    }

    /// <summary>
    /// Loads the main menu
    /// </summary>
    public void LoadMainMenu()
    {   
        FadeToLevel("MainMenu"); //Starts loading the main menu
    }
}
