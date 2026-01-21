using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class LootableInventory : BaseInteractable
{
    // Brennan 1/17/26
    // Handles the final accessable loot Data available to the player on loot interaction
    // Kiki says meow i miss you !!!
    
    // Declare pieces needed to generate items for player
    public PlayerContext playerContext;
    public PlayerController playerController;
    public Inventory inventory;

    [HideInInspector] public bool wasLooted;
    [HideInInspector] public bool isInteracting;
    [HideInInspector] public bool interactionComplete;

    // Final lists of lootable templates to generate
    public List<FoodItemTemplate> foodItems = new List<FoodItemTemplate>();
    public List<ToolItemTemplate> toolItems = new List<ToolItemTemplate>();
    public List<FirstAidItemTemplate> firstAidItems = new List<FirstAidItemTemplate>();
    public List<ClothesItemTemplate> clothesItems = new List<ClothesItemTemplate>();
    public List<MaterialItemTemplate> materialItems = new List<MaterialItemTemplate>();

    // Privs
    private ItemUtils itemUtils;

    private void Awake()
    {
        itemUtils = GetComponent<ItemUtils>();
    }
    
    private void Start()
    {
        wasLooted = false;
        isInteracting = false;
        playerContext = playerController.playerContext;
    }

    public void GenerateItemsFromTable()
    {
        // Mass produce Items from templates and context
        itemUtils.ManufactureFoodItemInstances(foodItems, inventory, playerContext);
        itemUtils.ManufactureToolItemInstances(toolItems, inventory, playerContext);
        itemUtils.ManufactureFirstAidItemInstances(firstAidItems, inventory, playerContext);
        itemUtils.ManufactureClothesItemInstances(clothesItems, inventory, playerContext);
        itemUtils.ManufactureMaterialItemInstances(materialItems, inventory, playerContext);
    }

    public override void ExecuteInteraction(ObjectContext _self)
    {
        
        // Perform Interaction     
        if (wasLooted == true)
        {
            return;
        }
        else if (wasLooted == false)
        {
            GenerateItemsFromTable();
            foodItems.Clear();
            toolItems.Clear();
            clothesItems.Clear();
            firstAidItems.Clear();
            materialItems.Clear();
        }
        interactionComplete = true;
    }
}
