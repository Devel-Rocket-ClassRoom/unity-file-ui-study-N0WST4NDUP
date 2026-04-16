using System.IO;
using Newtonsoft.Json;
using UnityEngine;

using SaveDataVC = SaveDataV3;

public static class SaveLoadManager
{
    public static int SaveDataVersion { get; } = 3;
    public static SaveDataVC Data { get; set; } = new();

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";
    private static readonly string[] SaveFileNames = {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json"
    };
    private static JsonSerializerSettings settings = new()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length) return false;

        if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);

        try
        {
            var path = Path.Combine(SaveDirectory, SaveFileNames[slot]);
            var json = JsonConvert.SerializeObject(Data, settings);
            File.WriteAllText(path, json);
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }

        return true;
    }

    public static bool Load(int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileNames.Length) return false;

        if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);

        var path = Path.Combine(SaveDirectory, SaveFileNames[slot]);
        if (!File.Exists(path)) return false;

        try
        {
            var json = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<SaveData>(json, settings);
            while (data.Version < SaveDataVersion)
            {
                Debug.Log($"이전 버전: {data.Version}");
                data = data.VersionUp();
                Debug.Log($"현재 버전: {data.Version}");
                Debug.Log("------------------------------");
            }
            Data = data as SaveDataVC;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }

        return true;
    }
}