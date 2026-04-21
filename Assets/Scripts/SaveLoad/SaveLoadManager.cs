using UnityEngine;
using SaveDataVC = SaveDataV5;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,       // (.json)
        Encrypted   // (.dat)
    }

    public static SaveMode Mode { get; set; } = SaveMode.Text;

    public static int SaveDataVersion { get; } = 5;
    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";
    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    static SaveLoadManager()
    {
        if (!Load())
        {
            Debug.Log("생성자 로드 실패");
        }
    }

    private static string GetSavefilePath(int slot)
    {
        return GetSavefilePath(slot, Mode);
    }

    private static string GetSavefilePath(int slot, SaveMode mode)
    {
        var ext = Mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,    // 추상 클래스의 실제 객체에 맞게 직렬화/역질렬화 하기 위함
    };

    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }

    public static bool Load(int slot = 0)
    {
        return Load(slot, Mode);
    }

    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetSavefilePath(slot, mode);

            switch (Mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    break;
            }

            return true;
        }
        catch (System.Exception)
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        string path = GetSavefilePath(slot, mode);

        if (!File.Exists(path))
        {
            return Save();
        }

        try
        {
            string json = string.Empty;
            switch (Mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    break;
            }
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);


            while (saveData.Version < SaveDataVersion)
            {
                Debug.Log(saveData.Version);
                saveData = saveData.VersionUp();
                Debug.Log(saveData.Version);
            }
            Data = saveData as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
}
