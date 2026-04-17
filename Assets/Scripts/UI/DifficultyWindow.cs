using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;

    public int selected;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
    }

    public override void Open()
    {
        base.Open();
        selected = OptionsSaveLoadManager.Load()?.difficulty ?? 0;
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if (active) selected = 0;
    }

    public void OnNormal(bool active)
    {
        if (active) selected = 1;
    }

    public void OnHard(bool active)
    {
        if (active) selected = 2;
    }

    public void OnCancle()
    {
        windowManager.Open(0);
    }

    public void OnApply()
    {
        OptionsSaveLoadManager.Save(new() { difficulty = selected });
        windowManager.Open(0);
    }
}