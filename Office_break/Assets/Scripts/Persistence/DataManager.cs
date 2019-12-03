using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class DataManager
{
    /// <summary> Saves the data associated with the given level. </summary>
    /// <param name="levelData"> The data associated to the completed level. </param>
    public static void SaveData(LevelData levelData)
    {
        LevelData existingData = LoadData(levelData.playerAlias);

        if (existingData != null)
        {
            Debug.Log(existingData);
            Debug.Log(existingData.collectedBriefcases);
            levelData.collectedBriefcases += existingData.collectedBriefcases;
            levelData.collectedClips += existingData.collectedClips;
        }

        string path = $"{Application.persistentDataPath}/{levelData.playerAlias}.data";

        using (StreamWriter file = File.CreateText(path))
        {
            JsonSerializer serializer = new JsonSerializer();

            //serialize object directly into file stream
            serializer.Serialize(file, levelData);
        }
    }

    /// <summary> Loads the <see cref="LevelData"/> associated with the given player. </summary>
    /// <param name="playerAlias"> The alias of the player. </param>
    /// <returns> The <see cref="LevelData"/> associated with the given player. </returns>
    public static LevelData LoadData(string playerAlias)
    {
        string path = $"{Application.persistentDataPath}/{playerAlias}.data";

        if (!File.Exists(path))
        {
            Debug.LogError("Archivo de guardado no encontrado en " + path);

            return null;
        }

        using (StreamReader file = File.OpenText(path))
        {
            JsonSerializer jsonSerializer = new JsonSerializer();

            return (LevelData)jsonSerializer.Deserialize(file, typeof(LevelData));
        }
    }
}