using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public string inventoryID;
    public List<ItemData> items;
    public bool HasBeenLooted;
    public bool locked;
    public InventoryData(string inventoryID, bool looted, bool _locked)
    {
        this.items = new List<ItemData>();
        this.inventoryID = inventoryID;
        this.HasBeenLooted = looted;
        this.locked = _locked;
    } 
}
