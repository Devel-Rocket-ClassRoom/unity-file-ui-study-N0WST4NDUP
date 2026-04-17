using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]
public class JsonTest2_Answer : MonoBehaviour
{
    public string fileName = "test.json";

    public GameObject cube;

    // 생성된 오브젝트들을 담을 리스트
    private List<GameObject> objects;
    // 생성된 오브젝트의 모양에 대응하는 int값을 딕셔너리로 저장
    private Dictionary<GameObject, int> shapeDic;

    //private List<int> types;

    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);

    public JsonSerializerSettings jsonSettings;

    private void Awake()
    {
        objects = new List<GameObject>();
        shapeDic = new Dictionary<GameObject, int>();
        //types = new List<int>();
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaterionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    private void Start()
    {
        objects.Add(cube);
        shapeDic.Add(cube, 3);
    }

    public void Save()
    {
        List<SomeClass> classes = new List<SomeClass>();

        // 오브젝트 리스트의 정보들을 한번에 리스트로 저장
        for (int i = 0; i < objects.Count; i++)
        {
            SomeClass obj = new SomeClass();
            obj.pos = objects[i].transform.position;
            obj.rot = objects[i].transform.rotation;
            obj.scale = objects[i].transform.localScale;
            obj.color = objects[i].GetComponent<Renderer>().material.color;
            obj.typeNum = shapeDic[objects[i]];
            classes.Add(obj);
        }

        // SomeClass 배열 Json 저장
        var json = JsonConvert.SerializeObject(classes, jsonSettings);
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        var json = File.ReadAllText(FileFullPath);

        // SomeClass 배열 로드
        List<SomeClass> obj = JsonConvert.DeserializeObject<List<SomeClass>>(json, jsonSettings);

        cube.transform.position = obj[0].pos;
        cube.transform.rotation = obj[0].rot;
        cube.transform.localScale = obj[0].scale;
        cube.GetComponent<Renderer>().material.color = obj[0].color;

        obj.RemoveAt(0);

        // 로드한 데이터를 토대로 하나씩 생성
        foreach (var gameObj in obj)
        {
            GameObject game = GameObject.CreatePrimitive((PrimitiveType)gameObj.typeNum);
            game.transform.position = gameObj.pos;
            game.transform.rotation = gameObj.rot;
            game.transform.localScale = gameObj.scale;
            game.GetComponent<Renderer>().material.color = gameObj.color;

            // 현재 생성된 애들이 다시 저장될 수 있으니 배열과 딕셔너리에 다시 저장
            objects.Add(game);
            shapeDic.Add(game, gameObj.typeNum);
        }
    }

    public void Create()
    {
        int count = UnityEngine.Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            int typeNum = UnityEngine.Random.Range(0, 6);

            // PrimitiveType(큐브, 스피어 등등)에서 랜덤한 오브젝트를 골라서 생성
            GameObject obj = GameObject.CreatePrimitive((PrimitiveType)typeNum);
            obj.transform.position = new Vector3(UnityEngine.Random.Range(0f, 5f), UnityEngine.Random.Range(0f, 5f), UnityEngine.Random.Range(0f, 5f));
            obj.transform.rotation = new Quaternion(UnityEngine.Random.Range(0f, 30f), UnityEngine.Random.Range(0f, 30f), UnityEngine.Random.Range(0f, 30f), UnityEngine.Random.Range(0f, 1f));
            obj.transform.localScale = new Vector3(UnityEngine.Random.Range(0.5f, 3f), UnityEngine.Random.Range(0.5f, 3f), UnityEngine.Random.Range(0.5f, 3f));
            obj.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();

            // 오브젝트 배열에 저장
            objects.Add(obj);
            
            // 딕셔너리에 모양 저장 
            shapeDic.Add(obj, typeNum);
            //types.Add(typeNum);
        }
    }

    public void Clear()
    {
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
    }




    //public void SaveCube()
    //{
    //    SomeClass obj = new SomeClass
    //    {
    //        pos = cube.transform.position,
    //        rot = cube.transform.rotation,
    //        scale = cube.transform.localScale,
    //        color = cube.GetComponent<Renderer>().material.color
    //    };
    //    var json = JsonConvert.SerializeObject(obj, jsonSettings);
    //    File.WriteAllText(FileFullPath, json);
    //}

    //public void LoadCube()
    //{
    //    var json = File.ReadAllText(FileFullPath);
    //    var obj = JsonConvert.DeserializeObject<SomeClass>(json, jsonSettings);
    //    cube.transform.position = obj.pos;
    //    cube.transform.rotation = obj.rot;
    //    cube.transform.localScale = obj.scale;
    //    cube.GetComponent<Renderer>().material.color = obj.color;
    //}
}
