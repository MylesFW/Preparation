using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Singleton<ItemController>
{
    public Dictionary<string, ItemDataSO> itemDatabase;
    public ItemDatabaseSO itemSet;
    protected override void Awake()
    {
        base.Awake();
        itemDatabase = new Dictionary<string, ItemDataSO>();
        itemDatabase.Clear();
        InitItemDatabase();
    }

    public void Start()
    {
        Debug.Log("Start!");
    }
    private void InitItemDatabase()
    {
        foreach (FoodItemDataSO item in itemSet.foodItemSet)
        {
            itemDatabase.Add(item.ItemName, item);
        }
        foreach (ToolItemDataSO item in itemSet.toolItemSet)
        {
            itemDatabase.Add(item.ItemName, item);
        }
        foreach (FirstAidItemDataSO item in itemSet.firstAidItemSet)
        {
            itemDatabase.Add(item.ItemName, item);
        }
        foreach (ClothesItemDataSO item in itemSet.clothesItemSet)
        {
            itemDatabase.Add(item.ItemName, item);
        }
        foreach (MaterialItemDataSO item in itemSet.materialItemSet)
        {
            itemDatabase.Add(item.ItemName, item);
        }
        Debug.Log("Finished!!");
    }
}
