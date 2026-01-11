using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootTable")]
public class ItemTable : ScriptableObject
{
    public string tableName;

    public List<FoodItemTemplate> foodItems = new List<FoodItemTemplate>();
    public List<ToolItemTemplate> toolItems = new List<ToolItemTemplate>();
    public List<ClothesItemTemplate> clothesItems = new List<ClothesItemTemplate>();
    public List<FirstAidItemTemplate> firstAidItems = new List<FirstAidItemTemplate>();
    public List<MaterialItemTemplate> materialItems = new List<MaterialItemTemplate>(); 
}
