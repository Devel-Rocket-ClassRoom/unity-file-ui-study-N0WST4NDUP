using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button ContinueButton;
    public Button StartButton;
    public Button OptionButton;
    public bool CanContinue;

    private void Awake()
    {
        ContinueButton.onClick.AddListener(OnContinue);
        StartButton.onClick.AddListener(OnNewGame);
        OptionButton.onClick.AddListener(OnOption);
    }

    public override void Open()
    {
        ContinueButton.gameObject.SetActive(CanContinue);
        if (!CanContinue)
        {
            FirstSelected = StartButton.gameObject;
        }

        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnContinue()
    {
        Debug.Log("OnContinue");
    }

    public void OnNewGame()
    {
        Debug.Log("OnNewGame");
    }

    public void OnOption()
    {
        Debug.Log("OnOption");
    }
}