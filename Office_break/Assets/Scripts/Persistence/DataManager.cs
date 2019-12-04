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
        LevelData currentLevelData = levelData;

        if (existingData != null)
        {
            int totalClips = levelData.collectedClips + existingData.collectedClips;
            int totalBriefCases = levelData.collectedBriefcases + existingData.collectedBriefcases;

            currentLevelData = new LevelData(levelData.playerAlias, totalClips, totalBriefCases)
            {
                boughtWeapons = levelData.boughtWeapons
            };
        }

        string path = $"{Application.persistentDataPath}/{currentLevelData.playerAlias}.data";

        using (StreamWriter file = File.CreateText(path))
        {
            JsonSerializer serializer = new JsonSerializer();

            //serialize object directly into file stream
            serializer.Serialize(file, currentLevelData);
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