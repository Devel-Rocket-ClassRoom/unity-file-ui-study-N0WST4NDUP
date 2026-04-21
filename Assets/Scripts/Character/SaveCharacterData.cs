using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SaveCharacterData
{
    public Guid instanceId { get; set; }

    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }
    public SaveItemData WeaponData { get; set; }
    public SaveItemData ArmorData { get; set; }
    public DateTime CreationTime { get; set; }

    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newCharacter = new();
        newCharacter.CharacterData = DataTableManager.CharacterTable.GetRandom();
        return newCharacter;
    }

    public SaveCharacterData()
    {
        instanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{instanceId}\n{CreationTime}\n{CharacterData.Id}";
    }

    public SaveItemData EquipItem(SaveItemData itemData)
    {
        SaveItemData prevItem = null;
        if (itemData.ItemData.Type == ItemTypes.Weapon)
        {
            prevItem = WeaponData;
            WeaponData = itemData;
            Debug.Log($"무기 장착 완료: {itemData.ItemData.StringName}");
        }
        else if (itemData.ItemData.Type == ItemTypes.Equip)
        {
            prevItem = ArmorData;
            ArmorData = itemData;
            Debug.Log($"방어구 장착 완료: {itemData.ItemData.StringName}");
        }

        return prevItem;
    }
}
