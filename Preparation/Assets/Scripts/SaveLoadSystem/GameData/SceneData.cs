using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    public string name;
    public List<InventoryData> invObjects;
    public SceneData()
    {
        invObjects = new List<InventoryData>(); 
    }
    public SceneData(string scenename)
    {
        this.name = scenename;
        invObjects = new List<InventoryData>();
    }
    public void TryAddInvData(InventoryData invdata)
    {
        if (invObjects == null)
        {
            invObjects = new List<InventoryData>();
        }
        string inputInvName = invdata.inventoryID;
        if (invObjects.Count == 0) { invObjects.Add(invdata); return;}
        for (int i = 0; i < invObjects.Count; i++)
        {
            if (invObjects[i].inventoryID == inputInvName)
            {
                invObjects[i] = invdata; // overwrite existing inventory data for object with matching id
                return;
            }
            else if (i == invObjects.Count - 1 && invObjects[i].inventoryID != inputInvName)
            {
                invObjects.Add(invdata); // add invdata if none exists
                return;
            }
        }
    }
}
