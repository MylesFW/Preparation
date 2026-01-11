using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class LootableInventory : BaseInteractable
{

    public ObjectContext playerContext;
    public PlayerController playerController;
    public Inventory inventory;

    private List<FoodItemTemplate> foodItems = new List<FoodItemTemplate>();
    private List<ToolItemTemplate> toolItems = new List<ToolItemTemplate>();
    private List<ClothesItemTemplate> clothesItems = new List<ClothesItemTemplate>();
    private List<FirstAidItemTemplate> firstAidItems = new List<FirstAidItemTemplate>();
    private List<MaterialItemTemplate> materialItems = new List<MaterialItemTemplate>();

    private List<Item> lootableItems = new List<Item>();

    private bool wasLooted;

    public void GenerateItemsFromTable()
    {
        playerContext = playerController.playerContext;

        for (int i = 0; i < foodItems.Count; i++) 
        {            
           lootableItems.Add(new FoodItem(foodItems[i], playerController.playerContext, foodItems[i].stackWeight, 100f));
        }
    }
    
    public void TransferInvetory()
    {
        for (int i = 0; i < lootableItems.Count; i++)
        {
            inventory.AddItem(lootableItems[i]);
            if (i == lootableItems.Count - 1)
            {
                Destroy(this);
            }
        }
    }

    public override void ExecuteInteraction(ObjectContext _self)
    {
        if (wasLooted == true)
        {
            return;
        }
        GenerateItemsFromTable();
        TransferInvetory();

        foodItems.Clear();
        toolItems.Clear();
        clothesItems.Clear();
        firstAidItems.Clear();
        materialItems.Clear();

        wasLooted = true;
    }   

    private void Awake()
    {
        wasLooted = false;
    }
    private void Start()
    {

        //foodItems = new List<FoodItemTemplate>(lootTable.foodItems);
        //toolItems = new List<ToolItemTemplate>(lootTable.toolItems);
        //clothesItems = new List<ClothesItemTemplate>(lootTable.clothesItems);
        //firstAidItems = new List<FirstAidItemTemplate>(lootTable.firstAidItems);
        //materialItems = new List<MaterialItemTemplate>(lootTable.materialItems);
    }
}
