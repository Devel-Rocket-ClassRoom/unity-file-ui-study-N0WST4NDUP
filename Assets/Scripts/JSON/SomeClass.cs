using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class SomeClass
{
    public int typeNum;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString()
    {
        return $"{pos} / {rot} / {scale} / {color}";
    }
}