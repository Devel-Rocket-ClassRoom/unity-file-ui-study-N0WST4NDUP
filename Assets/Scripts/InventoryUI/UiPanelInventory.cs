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
        OnFetch();
    }

    private void OnDisable()
    {
        OnSnapshot();
    }

    //private void OnDisable()
    //{
    //    Variables.OnLanguageChanged -= UpdateLanguage;
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 10; i++)
            {
                uiInvenSlotList.AddRandomItem();
            }
        }
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSnapshot()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.Options.InventoryFilteringOptions = (UiInvenSlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Data.Options.InventorySortingOptions = (UiInvenSlotList.SortingOptions)sorting.value;
    }

    public void OnFetch()
    {
        filtering.value = (int)SaveLoadManager.Data.Options.InventoryFilteringOptions;
        sorting.value = (int)SaveLoadManager.Data.Options.InventorySortingOptions;
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
