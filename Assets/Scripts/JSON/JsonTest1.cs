using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

// [Serializable] 붙은 것만 직렬화 역직렬화 지원
[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    //[JsonConverter(typeof(Vector3Converter))]
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health} / {position}";
    }
}

public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    private void Update()
    {
        // Save
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerState obj = new PlayerState
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };

            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
                );

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(
                pathFolder,
                "player2.json"
                );

            string json = JsonConvert.SerializeObject(
                obj, jsonSetting
                );
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
                );

            string path = Path.Combine(
                pathFolder,
                "player2.json"
                );

            string json = File.ReadAllText(path);
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(
                json, jsonSetting
                );

            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.lives} / {obj.health} / {obj.position}");
        }
    }
}
