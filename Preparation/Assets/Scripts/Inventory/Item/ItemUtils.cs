using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtils : MonoBehaviour
{
    public void ManufactureFoodItemInstances(List<FoodItemDataSO> fromList, List<Item> _inventory, PlayerContext _context)
    {
        for (int i = 0; i <= fromList.Count - 1; i++) 
        {
            FoodItemDataSO data = fromList[i];
            Item newItem = new FoodItem(data, _context);
            _inventory.Add(newItem);
        }
    }
    public void ManufactureToolItemInstances(List<ToolItemDataSO> fromList, List<Item> _inventory, PlayerContext _context)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            ToolItemDataSO data = fromList[i];
            Item newItem = new ToolItem(data, _context);
            _inventory.Add(newItem);
        }
    }
    public void ManufactureFirstAidItemInstances(List<FirstAidItemDataSO> fromList, List<Item> _inventory, PlayerContext _context)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            FirstAidItemDataSO data = fromList[i];
            Item newItem = new FirstAidItem(data, _context);
            _inventory.Add(newItem);
        }
    }
    public void ManufactureClothesItemInstances(List<ClothesItemDataSO> fromList, List<Item> _inventory, PlayerContext _context)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            ClothesItemDataSO data = fromList[i];
            Item newItem = new ClothesItem(data, _context);
            _inventory.Add(newItem);
        }
    }
    public void ManufactureMaterialItemInstances(List<MaterialItemDataSO> fromList, List<Item> _inventory, PlayerContext _context)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            MaterialItemDataSO data = fromList[i];
            Item newItem = new MaterialItem(data, _context);
            _inventory.Add(newItem);
        }
    }
}
