using TMPro;
using UnityEngine;

public class UIPanelInventory : MonoBehaviour
{
    public TMP_Dropdown filtering;
    public TMP_Dropdown sorting;
    public UiInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {
        OnLoad();
        OnChangeFiltering(filtering.value);
        OnChangeSorting(sorting.value);
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.sortType = (int)uiInvenSlotList.Sorting;
        SaveLoadManager.Data.filterType = (int)uiInvenSlotList.Filtering;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
        filtering.value = SaveLoadManager.Data.filterType;
        sorting.value = SaveLoadManager.Data.sortType;
    }

    public void OnAddItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
}