using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //SaveLoadManager.Data = new SaveDataV3();
            //SaveLoadManager.Data.Name = "TEST1234";
            //SaveLoadManager.Data.Gold = 4321;
            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach (var saveItemData in SaveLoadManager.Data.ItemList)
                {
                    Debug.Log(saveItemData.instanceId);
                    Debug.Log(saveItemData.ItemData.Name);
                    Debug.Log(saveItemData.CreationTime);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SaveItemData data = new SaveItemData();
            data.ItemData = DataTableManager.ItemTable.Get(DataTableManager.ItemTable.GetId()[Random.Range(0, DataTableManager.ItemTable.GetId().Count)]);
            SaveLoadManager.Data.ItemList.Add(data);
            Debug.Log("아이템 랜덤 추가");
        }

        // if (Input.GetKeyDown(KeyCode.Alpha4))
        // {
        //     foreach (var id in SaveLoadManager.Data.itemId)
        //     {
        //         ItemData item = DataTableManager.ItemTable.Get(id);
        //         Debug.Log($"타입: [{item.Type}] {item.StringName} : {item.StringDesc}");
        //     }
        // }
    }
}
