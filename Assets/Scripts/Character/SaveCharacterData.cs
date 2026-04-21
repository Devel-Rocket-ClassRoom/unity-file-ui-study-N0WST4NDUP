using System;
using Newtonsoft.Json;

[Serializable]
public class SaveCharacterData
{
    public Guid instanceId { get; set; }

    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }
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
}
