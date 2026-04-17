using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class JsonTest2 : MonoBehaviour
{
    private void Update()
    {
        // Save
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SomeClass obj = new SomeClass
            {
                pos = new Vector3(0f, 1f, 2f),
                rot = new Quaternion(10f, 10f, 10f, 1f),
                scale = new Vector3(1f, 2f, 3f),
                color = new Color(0f, 0f, 0f, 100f)
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
                "someclass.json"
                );

            string json = JsonConvert.SerializeObject(
                obj
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
                "someclass.json"
                );

            string json = File.ReadAllText(path);
            SomeClass obj = JsonConvert.DeserializeObject<SomeClass>(
                json
                );

            Debug.Log(json);
            Debug.Log($"{obj.pos} / {obj.rot} / {obj.scale} / {obj.color}");
        }
    }
}
