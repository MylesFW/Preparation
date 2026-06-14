using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryExample : MonoBehaviour, ISavable
{
    List<string> items = new List<string>();
    public string inventoryID;
    public bool hasbeenLooted;
    
    public void Start()
    {

    }
    public void AddItem()
    {
        hasbeenLooted = true;
        items.Add("Sword");
        items.Add("Shield");
        items.Add("Health Potion");
        
        Debug.Log($"{inventoryID} Revealed items: " + string.Join(", ", items));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            int dice = Random.Range(1, 7);
            if (dice > 3)
            {
                AddItem();
            }
        }
    }

    public GameData SaveInstance(GameData data)
    {
        // Init invdata object to be added to gamedata

        InventoryData invData = new InventoryData(inventoryID, hasbeenLooted, false);

        // Add items to invdata
        for (var i = 0; i < items.Count; i++)
        {
            //invData.items.Add(items[i]);
        }
        //Add invdata to gamedata
        data.sceneList[data.indexedScene].TryAddInvData(invData);
        return data;
    }

    public void LoadInstance(GameData data)
    {
        // Loop through Gamdata to find a matching inventoryID and load items
        for (var i = 0; i < data.sceneList[data.indexedScene].invObjects.Count; i++)
        {
            if (data.sceneList[data.indexedScene].invObjects[i].inventoryID == inventoryID)
            {
                //items = data.sceneList[data.indexedScene].invObjects[i].items;
                Debug.Log($"Loaded items to {inventoryID}: " + string.Join(", ", items));
                return;
            }
        }
    }
    void ISavable.NewGame()
    {

    }
}
