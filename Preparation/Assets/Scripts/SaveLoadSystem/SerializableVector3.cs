using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(float _x = 0, float _y = 0, float _z = 0)
    {
        this.x = _x;
        this.y = _y;
        this.z = _z;
    }
    public Vector3 ToVec()
    {
        var vec = new Vector3(x, y, z);
        return vec;
    }
}
