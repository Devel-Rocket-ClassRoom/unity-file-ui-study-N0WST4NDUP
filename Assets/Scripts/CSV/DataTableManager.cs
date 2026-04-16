using System.Collections.Generic;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    public static StringTable StringTable => Get<StringTable>(DatableIds.String);

    public static ItemTable ItemTable => Get<ItemTable>(DatableIds.Item);

    public static CharacterTable CharacterTable => Get<CharacterTable>(DatableIds.Character);

#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DatableIds.StringTableIds[(int)lang]);
    }
#endif

    static DataTableManager()
    {
        Init();
    }


    private static void Init()
    {
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DatableIds.String);
        tables.Add(DatableIds.String, stringTable);

        
#else
        foreach (var id in DatableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#endif

        var itemTable = new ItemTable();
        itemTable.Load(DatableIds.Item);
        tables.Add(DatableIds.Item, itemTable);

        var characterTable = new CharacterTable();
        characterTable.Load(DatableIds.Character);
        tables.Add(DatableIds.Character, characterTable);
    }
    public static void ChangeLanguage(Languages lang)
    {
        string newId = DatableIds.StringTableIds[(int)lang];

        if (tables.ContainsKey(newId))
            return;

        string oldId = string.Empty;
        foreach (var id in DatableIds.StringTableIds)
        {
            if (tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }

        var stringTable = tables[oldId];

        stringTable.Load(newId);

        tables.Remove(oldId);

        tables.Add(newId, stringTable);

    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Init();
        }
        return tables[id] as T;
    }
}
