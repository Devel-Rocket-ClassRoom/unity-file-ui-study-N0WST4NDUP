using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;
    public Button[] buttons;

    public int selected;

    private string pathFolder;
    private string path;


    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        buttons[0].onClick.AddListener(OnCancle);
        buttons[1].onClick.AddListener(OnApply);

        pathFolder = Path.Combine(Application.persistentDataPath, "SettingTest");
        path = Path.Combine(pathFolder, "Setting.txt");
    }

    public override void Open()
    {
        base.Open();
        Load();
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool acitve)
    {
        if (acitve)
        {
            selected = 0;
            Debug.Log("OnEasy");
        }
    }

    public void OnNormal(bool acitve)
    {
        if (acitve)
        {
            selected = 1;
            Debug.Log("OnNormal");
        }
    }

    public void OnHard(bool acitve)
    {
        if (acitve)
        {
            selected = 2;
            Debug.Log("OnHard");
        }
    }

    public void OnApply()
    {
        Save();
        windowManager.Open(0);
    }

    public void OnCancle()
    {
        windowManager.Open(0);
    }

    private void Save()
    {
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }

        Difficulty data = new Difficulty
        {
            difficultyId = selected
        };
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, json);
    }

    private bool Load()
    {
        if (!File.Exists(path))
        {
            return false;
        }
        string json = File.ReadAllText(path);
        Difficulty data = JsonConvert.DeserializeObject<Difficulty>(json);
        selected = data.difficultyId;

        return true;
    }
}

public class Difficulty
{
    public int difficultyId;
}
