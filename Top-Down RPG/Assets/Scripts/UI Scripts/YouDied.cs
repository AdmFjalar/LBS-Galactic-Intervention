using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDied : MonoBehaviour
{
    /// <summary>
    /// Used to exit the respawn scene and enter the demo scene
    /// </summary>
    public void ExitScene()
    {
        SceneManager.LoadScene("Demo"); //Loads the demo scene
    }
}
