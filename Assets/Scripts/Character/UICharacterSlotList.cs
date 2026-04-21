using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class UICharacterSlotList : MonoBehaviour
{
    // CompareTo
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        AttackAscending,
        AttackDescending,
        DefenceAscending,
        DefenceDescending
    }

    // Predicate
    public enum FilteringOptions
    {
        None,
        Warrior,
        Thief,
        Tanker
    }

    public readonly System.Comparison<SaveCharacterData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.CharacterData.Attack.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.Attack.CompareTo(lhs.CharacterData.StringName),
        (lhs, rhs) => lhs.CharacterData.Defense.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.Defense.CompareTo(lhs.CharacterData.StringName),
    };

    public readonly System.Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.StringName.Equals(CharacterData.Classes[0]),
        (x) => x.CharacterData.StringName.Equals(CharacterData.Classes[1]),
        (x) => x.CharacterData.StringName.Equals(CharacterData.Classes[2]),
    };

    public UICharacterSlot prefab;
    public ScrollRect scrollRect;

    private List<UICharacterSlot> uiSlotList = new();
    private List<SaveCharacterData> saveCharacterDataList = new();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveCharacterData> onSelectSlot;

    private void OnSelectSlot(SaveCharacterData saveItemData)
    {
        Debug.Log(saveItemData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < 10; i++)
            {
                AddRandomItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }

    public void SetSaveItemDataList(List<SaveCharacterData> source)
    {
        saveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacterData> GetSaveItemDataList()
    {
        return saveCharacterDataList;
    }

    private void UpdateSlots()
    {
        // 아이템 데이터들을 필터링 및 정렬 
        var list = saveCharacterDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            // 아이템 데이터들보다 현재 생성된 프리팹이 적으면 생성
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot?.Invoke(newSlot.SaveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        // 현재 uiSlot들을 보여질 만큼만 갱신
        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetCharacter(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots?.Invoke();
    }

    public void AddRandomItem()
    {
        saveCharacterDataList.Add(SaveCharacterData.GetRandomCharacter());

        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].SaveCharacterData);
        UpdateSlots();
    }
}
