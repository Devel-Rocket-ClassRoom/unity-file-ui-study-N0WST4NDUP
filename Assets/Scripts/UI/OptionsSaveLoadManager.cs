using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class OptionsSaveLoadManager
{
    [System.Serializable]
    public class Options
    {
        public int difficulty;
    }
    private static string dirPath = Application.persistentDataPath + "/SaveData";
    private static string fileName = "SaveOptions.json";
    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented
    };

    public static void Save(Options Data)
    {
        try
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string path = Path.Combine(dirPath, fileName);
            var json = JsonConvert.SerializeObject(Data, settings);

            File.WriteAllText(path, json);
            Debug.Log("Save 완료");
        }
        catch (System.Exception)
        {
            Debug.LogError("Save 예외");
        }
    }

    public static Options Load()
    {
        Options Data = null;

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        string path = Path.Combine(dirPath, fileName);

        try
        {
            if (!File.Exists(path))
            {
                return null;
            }
            Data = JsonConvert.DeserializeObject<Options>(
                File.ReadAllText(path), settings
            );
            Debug.Log($"Load 완료: {Data.difficulty}");
        }
        catch (System.Exception)
        {
            Debug.LogError("Load 예외");
        }

        return Data;
    }
}