using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> _tables = new();

    public static StringTable StringTable => Get<StringTable>(DatableIds.String);

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
        _tables.Add(DatableIds.String, stringTable);
#else
        foreach (var id in DatableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            _tables.Add(id, stringTable);
        }
#endif
    }

    public static void ChangeLanguage(Languages lang)
    {
        var stringTable = StringTable;
        stringTable.Load(DatableIds.StringTableIds[(int)lang]);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!_tables.ContainsKey(id))
        {
            Init();
        }

        return _tables[id] as T;
    }
}