using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class LocalizationDropDown : MonoBehaviour
{
#if UNITY_EDITOR
    public Languages editorLang;
#endif
    public string[] ids;
    public TMP_Dropdown dropdown;

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangeLanguage;

            OnChangeLanguage();
        }
#if UNITY_EDITOR
        else
        {
            OnChangeLanguage(editorLang);
        }
#endif
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged -= OnChangeLanguage;
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editorLang);
#endif
    }

    private void OnChangeLanguage()
    {
        Apply(DataTableManager.StringTable);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        Apply(DataTableManager.GetStringTable(lang));
    }

    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {
        var all = FindObjectsByType<LocalizationDropDown>(FindObjectsSortMode.None);
        foreach (var item in all)
        {
            item.editorLang = editorLang;
            item.OnChangeLanguage(editorLang);
        }
    }
#endif

    private void Apply(StringTable table)
    {
        if (dropdown == null || ids == null)
            return;

        int prevValue = dropdown.value;
        dropdown.ClearOptions();

        var options = new List<TMP_Dropdown.OptionData>(ids.Length);
        for (int i = 0; i < ids.Length; i++)
        {
            StringBuilder sb = new();
            var textArr = ids[i].Split(' ');
            foreach (var text in textArr)
            {
                sb.Append(table.Get(text) + ' ');
            }
            options.Add(new TMP_Dropdown.OptionData(sb.ToString()));
        }
        dropdown.AddOptions(options);

        if (ids.Length > 0)
            dropdown.value = Mathf.Clamp(prevValue, 0, ids.Length - 1);
        dropdown.RefreshShownValue();
    }
}
