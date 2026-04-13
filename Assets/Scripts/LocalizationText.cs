using System.Collections.Generic;
using UnityEngine;

public class LocalizationText : MonoBehaviour
{
    private StringTableText[] texts;

    private void Start()
    {
        texts = FindObjectsByType<StringTableText>(FindObjectsSortMode.None);
        Localizing(Languages.Korean);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Localizing(Languages.Korean);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Localizing(Languages.English);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Localizing(Languages.Japanese);
        }
    }

    private void Localizing(Languages lang)
    {
        foreach (var text in texts)
        {
            text.ChangeLanguage(lang);
        }
    }
}