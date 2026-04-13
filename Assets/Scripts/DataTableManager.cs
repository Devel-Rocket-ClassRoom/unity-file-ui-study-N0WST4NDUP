using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> _tables = new();

    public static StringTable StringTable => Get<StringTable>(DatableIds.String);

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
        var stringTable = new StringTable();
        stringTable.Load(DatableIds.String);
        _tables.Add(DatableIds.String, stringTable);
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