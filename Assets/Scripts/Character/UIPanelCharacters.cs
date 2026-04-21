using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIPanelCharacters : MonoBehaviour
{
    public TMP_Dropdown Sorting;
    public TMP_Dropdown Filtering;

    public UICharacterSlotList UICharacterSlotList;

    private string[] SortId =
    {
        "CreationTimeAsscding",
        "CreationTimeDeccending",
        "NameAccending",
        "NameDeccending",
        "CostAccending",
        "CostDeccending",
        "ValueAccending",
        "ValudDeccending",
    };
    private string[] FilterId =
    {
        "None",
        "Weapon",
        "Equip",
        "Consumable",
        "NonConsumable"
    };

    private void OnEnable()
    {
        OnFetch();
    }

    private void OnDisable()
    {
        OnSnapshot();
    }

    public void OnChangeSorting(int index)
    {
        UICharacterSlotList.Sorting = (UICharacterSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        UICharacterSlotList.Filtering = (UICharacterSlotList.FilteringOptions)index;
    }

    public void OnSnapshot()
    {
        SaveLoadManager.Data.CharacterList = UICharacterSlotList.GetSaveCharacterDataList();
        SaveLoadManager.Data.Options.CharacterFilteringOptions = (UICharacterSlotList.FilteringOptions)Filtering.value;
        SaveLoadManager.Data.Options.CharacterSortingOptions = (UICharacterSlotList.SortingOptions)Sorting.value;
    }

    public void OnFetch()
    {
        Filtering.value = (int)SaveLoadManager.Data.Options.CharacterFilteringOptions;
        Sorting.value = (int)SaveLoadManager.Data.Options.CharacterSortingOptions;
        UICharacterSlotList.SetSaveCharacterDataList(SaveLoadManager.Data.CharacterList);
    }

    public void OnSave()
    {
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
    }

    public void OnCreateCharacter()
    {
        UICharacterSlotList.AddRandomCharacter();
    }

    public void OnRemoveCharacter()
    {
        UICharacterSlotList.RemoveCharacter();
    }
}
