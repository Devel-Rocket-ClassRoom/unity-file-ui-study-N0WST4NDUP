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
        return new SaveDataV2()
        {
            Name = PlayerName
        };
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

    public override SaveData VersionUp()
    {
        return new SaveDataV3()
        {
            Name = this.Name,
            Gold = this.Gold
        };
    }
}

[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public List<string> Inventory = new();

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}