using System.IO;
using Newtonsoft.Json;
using UnityEngine;

using SaveDataVC = SaveDataV3;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text = 0,       // JSON 텍스트 (.json)
        Encrypted = 1,  // AES 암호화 (.dat)
    }

    public static SaveMode Mode { get; set; } = SaveMode.Encrypted;

    public static int SaveDataVersion { get; } = 3;
    public static SaveDataVC Data { get; set; } = new();

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";
    private static readonly string[] SaveFileNames = {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };
    private static readonly string[] SaveFileExtensions = {
        ".json",
        ".dat"
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
            var path = Path.Combine(SaveDirectory, SaveFileNames[slot] + SaveFileExtensions[(int)Mode]);
            var json = JsonConvert.SerializeObject(Data, settings);
            if (Mode == SaveMode.Encrypted)
            {
                var encrypted = CryptoUtil.Encrypt(json);
                File.WriteAllBytes(path, encrypted);
            }
            else
            {
                File.WriteAllText(path, json);
            }
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

        var path = Path.Combine(SaveDirectory, SaveFileNames[slot] + SaveFileExtensions[(int)Mode]);
        if (!File.Exists(path)) return false;

        try
        {
            SaveData data;

            if (Mode == SaveMode.Encrypted)
            {
                var bytes = File.ReadAllBytes(path);
                var decrypted = CryptoUtil.Decrypt(bytes);
                data = JsonConvert.DeserializeObject<SaveData>(decrypted, settings);
            }
            else
            {
                var json = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<SaveData>(json, settings);
            }

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