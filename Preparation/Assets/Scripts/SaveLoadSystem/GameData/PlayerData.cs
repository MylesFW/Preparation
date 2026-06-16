using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public float health;
    public SerializableVector3 pos;

    // Survival stats
    public float thirst;
    public float calories;
    public float fatigue;
    public float playerTemp;
    
    // Inventory
    public List<ItemData> items;

    public PlayerData()
    {
        pos = new SerializableVector3();
        items = new List<ItemData>();
    }
}
