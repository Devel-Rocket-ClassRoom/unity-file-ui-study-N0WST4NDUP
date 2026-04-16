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

    public static bool Save(int slot = 0, SaveMode mode = SaveMode.Encrypted)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length) return false;

        if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);
        var path = Path.Combine(SaveDirectory, SaveFileNames[slot] + SaveFileExtensions[(int)Mode]);

        try
        {
            var json = JsonConvert.SerializeObject(Data, settings);
            switch (mode)
            {
                case SaveMode.Text:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllText(path, json);
                    break;
            }
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }

        return true;
    }

    public static bool Load(int slot = 0, SaveMode mode = SaveMode.Encrypted)
    {
        if (slot < 0 || slot >= SaveFileNames.Length) return false;

        if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);

        var path = Path.Combine(SaveDirectory, SaveFileNames[slot] + SaveFileExtensions[(int)Mode]);
        if (!File.Exists(path)) return false;

        try
        {
            SaveData data;
            switch (mode)
            {
                case SaveMode.Text:
                    var decrypted = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    data = JsonConvert.DeserializeObject<SaveData>(decrypted, settings);
                    break;
                case SaveMode.Encrypted:
                    var json = File.ReadAllText(path);
                    data = JsonConvert.DeserializeObject<SaveData>(json, settings);
                    break;
                default:
                    Debug.Log("사용하지 않는 모드");
                    return false;
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