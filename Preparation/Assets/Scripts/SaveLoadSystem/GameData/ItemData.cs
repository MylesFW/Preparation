using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string name;
    public float amount;
    public float condition;
    public ItemData(string name, float amount, float condition)
    {
        this.name = name;
        this.amount = amount;
        this.condition = condition;
    }
}
