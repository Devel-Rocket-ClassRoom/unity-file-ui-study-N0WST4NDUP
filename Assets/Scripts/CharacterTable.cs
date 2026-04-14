using System.Collections.Generic;
using UnityEngine;

public class CharacterTable : DataTable
{
    public static readonly string Unknown = "키 없음";

    public class CharacterData
    {
        public string Id;
        public string Name;
        public string Desc;
        public int Level;
        public float MaxHealth;
        public float Atk;
        public float Def;
        public string Icon;
    }

    private readonly Dictionary<string, CharacterData> _table = new();

    public override void Load(string fileName)
    {
        _table.Clear();

        var path = string.Format(FormatPath, fileName);
        var textAsset = Resources.Load<TextAsset>(path);
        var dataList = LoadCSV<CharacterData>(textAsset.text);
        foreach (var data in dataList)
        {
            if (!_table.ContainsKey(data.Id))
            {
                _table.Add(data.Id, data);
            }
            else
            {
                Debug.LogError($"키 중복: {data.Id}");
            }
        }
    }
}