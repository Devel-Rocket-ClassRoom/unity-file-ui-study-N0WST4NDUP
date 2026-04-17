using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        return saveData;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;

    public SaveDataV2()
    {
        Version = 2;
    }

    //public override SaveData VersionUp()
    //{
    //    var data = new SaveDataV3();
    //    data.Name = Name;
    //    data.Gold = Gold;
    //    return data;
    //}
    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();
        // SaveDataV2 -> SaveDataV3 필드 변환
        saveData.Name = Name;
        saveData.Gold = Gold;
        return saveData;
    }
}

//[System.Serializable]
//public class SaveDataV3 : SaveDataV2
//{
//    public List<string> itemList = new List<string>();

//    public SaveDataV3()
//    {
//        Version = 3;
//    }


//}

[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public List<string> itemId = new List<string>();

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}