using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] Windows;

    public int CurrentWindowId;
    public int DefaultWindowId;

    private void Awake()
    {
        foreach (var window in Windows)
        {
            window.gameObject.SetActive(false);
            window.Init(this);
        }
        CurrentWindowId = DefaultWindowId;
        Windows[CurrentWindowId].Open();
    }

    public GenericWindow Open(int id)
    {
        Windows[CurrentWindowId].Close();
        CurrentWindowId = id;
        Windows[CurrentWindowId].Open();

        return Windows[CurrentWindowId];
    }
}