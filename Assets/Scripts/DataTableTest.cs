using System;
using UnityEngine;
using UnityEngine.UI;

public class DataTableTest : MonoBehaviour
{
    // private StringTable _stringTable = new();

    public void OnClickEvent()
    {
        Debug.Log(DataTableManager.StringTable.Get("YOU DIE"));
    }
}