using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    /// <summary>
    /// Saves the current progress in the game
    /// </summary>
    /// <param name="gameManager">The gamemanager in the scene</param>
    public static void SaveProgress(GameManager gameManager)
    {
        Debug.Log("Saving Progress...");
        BinaryFormatter formatter = new BinaryFormatter(); //Sets a binary formatter
        string path = Application.persistentDataPath + "/playerdata.s3"; //Sets the path of the savefile
        FileStream stream = new FileStream(path, FileMode.Create); //Opens a stream to save all the data

        SaveData data = new SaveData(gameManager); //Creates an instance of the savedata class to contain all the data

        formatter.Serialize(stream, data); //Converts the savedata to a binary format
        stream.Close(); //Closes the stream
    }

    /// <summary>
    /// Loads the saved progress
    /// </summary>
    /// <returns>Saved progress</returns>
    public static SaveData LoadData ()
    {
        Debug.Log("Loading Data...");
        string path = Application.persistentDataPath + "/playerdata.s3"; //Sets the path of the savefile 
        if (File.Exists(path)) //Checks if there exists a file at the path
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Sets a binary formatter
            FileStream stream = new FileStream(path, FileMode.Open); //Opens a stream to load all the data

            SaveData data = formatter.Deserialize(stream) as SaveData; //Deserializes the data and loads it into an instance of the savedata class
            stream.Close(); //Closes the stream

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }
}
