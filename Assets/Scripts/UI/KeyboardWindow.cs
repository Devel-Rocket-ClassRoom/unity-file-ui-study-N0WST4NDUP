using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    private readonly StringBuilder sb = new();

    public TextMeshProUGUI inputField;
    public GameObject rootKeyboard;
    public Button cancleButton;
    public Button deleteButton;
    public Button acceptButton;

    public int maxCharacters = 7;

    private float timer = 0f;
    private float cursorDelay = 0.5f;
    private bool blink;

    private void Awake()
    {
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach (var key in keys)
        {
            var text = key.GetComponentInChildren<TextMeshProUGUI>();
            key.onClick.AddListener(() => OnKey(text.text));
        }

        cancleButton.onClick.AddListener(OnCancle);
        deleteButton.onClick.AddListener(OnDelete);
        acceptButton.onClick.AddListener(OnAccept);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > cursorDelay)
        {
            timer = 0f;
            blink = !blink;
            UpdateInputField();
        }
    }

    public override void Open()
    {
        sb.Clear();
        timer = 0f;
        blink = false;
        base.Open();
        UpdateInputField();
    }

    public void OnKey(string key)
    {
        if (sb.Length < maxCharacters)
        {
            sb.Append(key);
            UpdateInputField();
        }
    }

    private void UpdateInputField()
    {
        bool showCursor = !blink && sb.Length < maxCharacters;
        if (showCursor)
        {
            sb.Append("_");
        }
        inputField.SetText(sb);
        if (showCursor)
        {
            sb.Length--;
        }
    }

    public void OnCancle()
    {
        sb.Clear();
        UpdateInputField();
    }

    public void OnDelete()
    {
        if (sb.Length > 0)
        {
            sb.Length--;
            UpdateInputField();
        }
    }

    public void OnAccept()
    {
        windowManager.Open(0);
    }
}
