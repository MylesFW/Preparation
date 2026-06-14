using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "ItemData/ItemDatabase")]
public class ItemDatabaseSO : ScriptableObject
{
    public List<FoodItemDataSO> foodItemSet;
    public List<ToolItemDataSO> toolItemSet;
    public List<FirstAidItemDataSO> firstAidItemSet;
    public List<ClothesItemDataSO> clothesItemSet;
    public List<MaterialItemDataSO> materialItemSet;
}
