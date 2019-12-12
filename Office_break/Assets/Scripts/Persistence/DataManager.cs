using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class DataManager
{
    /// <summary> Saves the data associated with the given level. </summary>
    /// <param name="userData"> The data associated to the given user. </param>
    public static void SaveData(UserData userData)
    {
        string path = $"{Application.persistentDataPath}/{userData.UserAlias}.data";

        using (StreamWriter file = File.CreateText(path))
        {
            JsonSerializer serializer = new JsonSerializer();

            //serialize object directly into file stream
            serializer.Serialize(file, userData);
        }
    }

    /// <summary> Loads the <see cref="UserData"/> associated with the given player. </summary>
    /// <param name="playerAlias"> The alias of the player. </param>
    /// <returns> The <see cref="UserData"/> associated with the given player. </returns>
    public static UserData LoadData(string playerAlias)
    {
        string path = $"{Application.persistentDataPath}/{playerAlias}.data";

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Archivo de guardado no encontrado en {path}.");

            return null;
        }

        using (StreamReader file = File.OpenText(path))
        {
            JsonSerializer jsonSerializer = new JsonSerializer();

            return (UserData)jsonSerializer.Deserialize(file, typeof(UserData));
        }
    }
}