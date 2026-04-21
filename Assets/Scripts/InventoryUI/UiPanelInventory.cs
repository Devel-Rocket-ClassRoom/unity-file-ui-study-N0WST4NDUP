using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList uiInvenSlotList;

    private string[] sortId =
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
    private string[] filterId =
    {
        "None",
        "Weapon",
        "Equip",
        "Consumable",
        "NonConsumable"
    };

    //private void Awake()
    //{
    //    Variables.OnLanguageChanged += UpdateLanguage;
    //}

    private void OnEnable()
    {
        //UpdateLanguage();
        filtering.value = (int)SaveLoadManager.Data.filterIndex;
        sorting.value = (int)SaveLoadManager.Data.sortIndex;
        OnLoad();
    }

    //private void OnDisable()
    //{
    //    Variables.OnLanguageChanged -= UpdateLanguage;
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        Variables.Language = Languages.Korean;
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        Variables.Language = Languages.English;
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        Variables.Language = Languages.Japanese;
    //    }
    //}

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.filterIndex = (UiInvenSlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Data.sortIndex = (UiInvenSlotList.SortingOptions)sorting.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();

        //OnChangeFiltering(filtering.value);
        //OnChangeSorting(sorting.value);
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }

    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }

    //public void UpdateLanguage()
    //{
    //    List<string> sortingOptions = new List<string>();
    //    List<string> filteringOptions = new List<string>();

    //    for (int i = 0; i < sortId.Length; i++)
    //    {
    //        sortingOptions.Add(DataTableManager.StringTable.Get(sortId[i]));
    //        Debug.Log(DataTableManager.StringTable.Get(sortId[i]));
    //    }
    //    sorting.ClearOptions();
    //    sorting.AddOptions(sortingOptions);
    //    sorting.RefreshShownValue();

    //    for (int i = 0; i < filterId.Length; i++)
    //    {
    //        filteringOptions.Add(DataTableManager.StringTable.Get(filterId[i]));
    //        Debug.Log(DataTableManager.StringTable.Get(filterId[i]));
    //    }

    //    filtering.ClearOptions();
    //    filtering.AddOptions(filteringOptions);
    //    filtering.RefreshShownValue();
    //}
}
