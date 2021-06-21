using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider; //The volume slider
    public AudioMixer audioMixer; //The master audiomixer
    public float currentVolume; //The current volume in the audiomixer
    public float chosenVolume; //The chosen volume
    public bool loadingLevel; //Bool representing if the scene is being loaded

    // Use this for initialization
    private void Start()
    {
        loadingLevel = true; //Sets that the level is being loaded
        currentVolume = -80; //Sets the volume to -80 (0%)
        audioMixer.SetFloat("volume", currentVolume); //Changes the volume of the audiomixer to the current volume
        
        chosenVolume = PlayerPrefs.GetFloat("Volume"); //Sets the chosen volume to the playerpref volume
        PlayerPrefs.SetInt("ChangingLevel", 1); //Sets that the scene should be changed
        PlayerPrefs.SetInt("LoadingLevel", 1); //Sets that the scene is being loaded
        if (slider != null) //Checks if the slider exists
        {
            slider.value = PlayerPrefs.GetFloat("Volume"); //Sets the slider value to the volume playerpref value
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("ChangingScene") == 1) //Checks if the scene is being changed
        {
            loadingLevel = true; //Sets the loadinglevel bool to true
        }
        else if (PlayerPrefs.GetInt("ChangingScene") == 0) //Checks if the scene is not being changed
        {
            loadingLevel = false; //Sets the loadinglevel bool to false
        }
        chosenVolume = PlayerPrefs.GetFloat("Volume"); //Sets the chosen volume to the playerprefs volume
        audioMixer.SetFloat("volume", currentVolume); //Sets the audiomixer volume to the current volume

        if (PlayerPrefs.GetInt("ChangingLevel") == 1) //Checks if the level is being changed
        {
            if (PlayerPrefs.GetInt("LoadingLevel") == 0 && currentVolume > -80) //Checks if there is not a scene being loaded and if the current volume is greater than -80
            {
                currentVolume = currentVolume - 1f; //Sets the current volume to the current volume minus one
                audioMixer.SetFloat("volume", currentVolume); //Sets the audiomixer's volume to the current volume
            }
            else if (PlayerPrefs.GetInt("LoadingLevel") == 1 && currentVolume < chosenVolume) //Checks if a scene is being loaded and if the current volume is less than the chosen volume
            {
                currentVolume = currentVolume + 1f; //Adds 1 to the current volume
                audioMixer.SetFloat("volume", currentVolume); //Sets the audiomixer's volume to the current volume 
            }
        }
    }

    public void LateUpdate()
    {
        if (currentVolume >= chosenVolume) //Checks if the current volume is greater than the chosen volume
        {
            loadingLevel = false; //Sets that the level is not being loaded
            PlayerPrefs.SetInt("ChangingLevel", 0); //Sets that the level is not being changed
            PlayerPrefs.SetInt("LoadingLevel", 0); //Sets that the level is not being loaded
        }
    }

    /// <summary>
    /// Sets the volume of the audio mixer
    /// </summary>
    /// <param name="volume">Chosen volume</param>
    public void SetVolume(float volume)
    {
        if (loadingLevel == false && slider != null) //Checks if the level is not being loaded and if the slider exists
        {
            currentVolume = volume; //Sets the current volume to the entered volume
            audioMixer.SetFloat("volume", currentVolume); //Sets the audiomixer's volume to the current volume
            if (PlayerPrefs.GetInt("ChangingLevel") == 0) //Checks if the level is not being changed
            {
                PlayerPrefs.SetFloat("Volume", currentVolume); //Sets the playerprefs volume to the current volume
            }
        }
    }
}
