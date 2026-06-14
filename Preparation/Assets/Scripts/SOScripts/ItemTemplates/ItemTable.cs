using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootTable")]
public class ItemTable : ScriptableObject
{
    public string tableName;

    public List<FoodItemDataSO> foodItems = new List<FoodItemDataSO>();
    public List<ToolItemDataSO> toolItems = new List<ToolItemDataSO>();
    public List<ClothesItemDataSO> clothesItems = new List<ClothesItemDataSO>();
    public List<FirstAidItemDataSO> firstAidItems = new List<FirstAidItemDataSO>();
    public List<MaterialItemDataSO> materialItems = new List<MaterialItemDataSO>(); 
}
