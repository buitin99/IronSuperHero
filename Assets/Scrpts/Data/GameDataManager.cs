using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager<T> where T : class, new ()
{
    public static T LoadData ()
    {
        string path = Application.persistentDataPath +  $"/Saves/{typeof(T).Name}.sav";
        // Does the file exist?
        if (File.Exists(path))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(path);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            T data = JsonUtility.FromJson<T>(fileContents);
            return data;
        }
        else
        {
            T newData = new T();
            return newData;
        }
    }

    public static void SaveData (T data)
    {
        if(!Directory.Exists(Application.persistentDataPath + "/Saves")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }
        string path = Application.persistentDataPath + $"/Saves/{typeof(T).Name}.sav";
        Debug.Log(path);
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(data);

        // Write JSON to file.
        File.WriteAllText(path, jsonString);
    }
}

public class JsonVector3
{
    public float x;
    public float y;
    public float z;
    
    public JsonVector3(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public JsonVector3(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public static Vector3 ToVector3(JsonVector3 jsonVector3) {
        return new Vector3(jsonVector3.x, jsonVector3.y, jsonVector3.z);
    }
}