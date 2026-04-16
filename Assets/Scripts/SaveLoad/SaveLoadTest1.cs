using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 4321;
            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach (var itemId in SaveLoadManager.Data.Inventory)
                {
                    var item = DataTableManager.ItemTable.Get(itemId);
                    Debug.Log(item);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(SaveLoadManager.Data.Name);
            Debug.Log(SaveLoadManager.Data.Gold);
            foreach (var itemId in SaveLoadManager.Data.Inventory)
            {
                var item = DataTableManager.ItemTable.Get(itemId);
                Debug.Log(item);
            }
        }
    }

    public void OnClickAddItem()
    {
        var items = new string[]
        {
            "Item1",
            "Item2",
            "Item3",
            "Item4",
        };
        var item = DataTableManager.ItemTable.Get(items[Random.Range(0, items.Length)]);
        SaveLoadManager.Data.Inventory.Add(item.Id);
        Debug.Log($"아이템이 추가 되었습니다: {item}");
    }
}