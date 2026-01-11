using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootRandomizer : MonoBehaviour
{
    // Total Items possible
    [Header("Number of Items to loot")]
    [Tooltip("Starting amount of possible loot")]
    public int setLootAmount;
    [Tooltip("Variance of total possible loot")]
    public int amountRange;
    
    // Average value of item, range of value for rarity distribution
    [Header("Item Value Chance")]
    [Tooltip("Average item rarity")]
    public int setPoint;
    [Tooltip("Larger range = more variety of common/rare loot")]
    public int valueRange;

    // ItemTable to Pull from
    [Header("Item Table to randomize")]
    public ItemTable itemTable;

    // Randomizer's personal ItemTemplate lists
    private List<FoodItemTemplate> foodTemplates;
    private List<ToolItemTemplate> toolTemplates;
    private List<FirstAidItemTemplate> firstAidTemplates;
    private List<ClothesItemTemplate> ClothesTemplates;
    private List<MaterialItemTemplate> materialTemplates;

    // Resulting Items to loot
    private FoodItemTemplate[] foodArray;
    private ToolItemTemplate[] toolArray;
    private FirstAidItemTemplate[] materialArray;
    private ClothesItemTemplate[] clothesArray;
    private MaterialItemTemplate[] firtAidArray;

    private void Awake()
    {
        // Cache local template lists
        List<FoodItemTemplate> foodTemplates = new List<FoodItemTemplate>();
        List<ToolItemTemplate> toolTemplates = new List<ToolItemTemplate>();
        List<FirstAidItemTemplate> firstAidTemplates = new List<FirstAidItemTemplate>();
        List<ClothesItemTemplate> ClothesTemplates = new List<ClothesItemTemplate>();
        List<MaterialItemTemplate> materialTemplates = new List<MaterialItemTemplate>();
    }

    private void Start()
    {
        // TODO 
        // get the item templates stored in the loot table lists and perform- 
        // Probability checks to decide on what Items to pull
        // Additionally, add randomization of total Items to loot from the container
    }   
}
