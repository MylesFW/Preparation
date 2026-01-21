using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootRandomizer : MonoBehaviour
{
    // ItemTable to Pull from
    [Header("Item Table to randomize")]
    public ItemTable itemTable;

    [Header("Item Amount Min/Max")]
    [Range(0, 25)]
    public int minItemAmount;
    [Range(0, 25)]
    public int maxItemAmount;
    
    [Header("Empty Container Chance (%)")]
    [Range(0, 100)]
    public int emptyChance;

    [Header("Subtype Chance Rate")]
    [Range(0, 100)]
    public int foodRate;
    [Range(0, 100)]
    public int toolRate;
    [Range(0, 100)]
    public int firstAidRate;
    [Range(0, 100)]
    public int clothesRate;
    [Range(0, 100)]
    public int materialRate;

    private int empty_Dice;

    private int combinedTypeRate;
    private int combinedDice;
    
    private List<int> subTypeRateList;
    private List<int> availableSubtypes = new List<int>();

    private int lootAmount;
 
    private LootableInventory lootableInventory;

    private int[] itemTypeRate;
    private int indexedSubType;

    // Meth
    private void CalculateCombinedTypeRate()
    {
        availableSubtypes.Add(foodRate);
        availableSubtypes.Add(toolRate);
        availableSubtypes.Add(firstAidRate);
        availableSubtypes.Add(clothesRate);
        availableSubtypes.Add(materialRate);
        
        // Remove Empties 
        if (foodRate == 0)
        {
            availableSubtypes.Remove(foodRate);
        }
        
        if (toolRate == 0)
        {
            availableSubtypes.Remove(toolRate);
        }
        
        if (firstAidRate == 0)
        {
            availableSubtypes.Remove(firstAidRate);
        }   
        
        if (clothesRate == 0)
        {
            availableSubtypes.Remove(clothesRate);
        }
        
        if (materialRate == 0)
        {
            availableSubtypes.Remove(materialRate);
        }      

        for(int i = 0; i <= availableSubtypes.Count - 1; i++)
        {
            combinedTypeRate += availableSubtypes[i];
        }
    
        if (availableSubtypes.Count == 0)
        {
            combinedTypeRate = -1;
        }
    }    
    private void RollFoodItems()
    {
        int combinedSubTypeValue = 0;
        for (int i = 0; i <= itemTable.foodItems.Count - 1; i++)
        {
            combinedSubTypeValue += itemTable.foodItems[i].dropRate;
        }
        
        int _dice = Random.Range(0, combinedSubTypeValue);
        for (int i = 0; _dice > 0; i++) 
        {
            _dice -= itemTable.foodItems[i].dropRate;
            if (_dice <= 0)
            {
                // Send Template to Item Factory (inside player)
                lootableInventory.foodItems.Add(itemTable.foodItems[i]);
            }
        }      
    }
    private void RollToolItems()
    {
        int combinedSubTypeValue = 0;
        for (int i = 0; i < itemTable.toolItems.Count; i++)
        {
            combinedSubTypeValue += itemTable.toolItems[i].dropRate;
        }

        int _dice = Random.Range(0, combinedSubTypeValue);
        for (int i = 0; _dice > 0; i++)
        {
            _dice -= itemTable.toolItems[i].dropRate;
            if (_dice <= 0)
            {
                // Send Template to Item Factory (inside player)
                lootableInventory.toolItems.Add(itemTable.toolItems[i]);
            }
        }
    }
    private void RollFirstAidItems()
    {
        int combinedSubTypeValue = 0;
        for (int i = 0; i < itemTable.firstAidItems.Count; i++)
        {
            combinedSubTypeValue += itemTable.firstAidItems[i].dropRate;
        }

        int _dice = Random.Range(0, combinedSubTypeValue);
        for (int i = 0; _dice > 0; i++)
        {
            _dice -= itemTable.firstAidItems[i].dropRate;
            if (_dice <= 0)
            {
                // Send Template to Item Factory (inside player)
                lootableInventory.firstAidItems.Add(itemTable.firstAidItems[i]);
            }
        }
    }
    private void RollClothesItems()
    {
        int combinedSubTypeValue = 0;
        for (int i = 0; i < itemTable.clothesItems.Count; i++)
        {
            combinedSubTypeValue += itemTable.clothesItems[i].dropRate;
        }

        int _dice = Random.Range(0, combinedSubTypeValue);
        for (int i = 0; _dice > 0; i++)
        {
            _dice -= itemTable.clothesItems[i].dropRate;
            if (_dice <= 0)
            {
                // Send Template to Item Factory (inside player)
                lootableInventory.clothesItems.Add(itemTable.clothesItems[i]);
            }
        }
    }
    private void RollMaterialItems()
    {
        int combinedSubTypeValue = 0;
        for (int i = 0; i < itemTable.materialItems.Count; i++)
        {
            combinedSubTypeValue += itemTable.materialItems[i].dropRate;
        }

        int _dice = Random.Range(0, combinedSubTypeValue);
        for (int i = 0; _dice > 0; i++)
        {
            _dice -= itemTable.materialItems[i].dropRate;
            if (_dice <= 0)
            {
                // Send Template to Item Factory (inside player)
                lootableInventory.materialItems.Add(itemTable.materialItems[i]);
            }
        }
    }
    private void RollItemsFromTable()
    {
        // TODO - Stop the item table selection from picking empty tables.

        combinedDice = Random.Range(0, combinedTypeRate);

        // Roll ItemSubTypes
        for (int i = 0; i <= itemTypeRate.Length - 1; i++)
        {
            combinedDice -= itemTypeRate[i];
            if (combinedDice <= 0)
            {
                indexedSubType = i;
                break;
            }
        }

        if (indexedSubType == 0)
        {
            RollFoodItems();
        }
        else if (indexedSubType == 1)
        {
            RollToolItems();
        }
        else if (indexedSubType == 2)
        {
            RollFirstAidItems();
        }
        else if (indexedSubType == 3)
        {
            RollClothesItems();
        }
        else if (indexedSubType == 4)
        {
            RollMaterialItems();
        }
    }
    
    private void Awake()
    {
        // init random vars
        lootAmount = Random.Range(minItemAmount, maxItemAmount);
        empty_Dice = Random.Range(0, 100);

        CalculateCombinedTypeRate();

        lootableInventory = GetComponent<LootableInventory>();

        itemTypeRate = new int[5];
        
        itemTypeRate[0] = foodRate;
        itemTypeRate[1] = toolRate;
        itemTypeRate[2] = firstAidRate;
        itemTypeRate[3] = clothesRate;
        itemTypeRate[4] = materialRate;
                    
    }

    private void Start()
    {
        // Empty Check
        if (empty_Dice <= emptyChance)
        {
            return;
        }
        else
        {
            // Roll Items
            for (int i = 0; i < lootAmount; i++)
            {
                RollItemsFromTable();
            }
        }
    }
}
