using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtils : MonoBehaviour
{
    private Inventory destinationInventory;
    public void ManufactureFoodItemInstances(List<FoodItemTemplate> fromList, Inventory _inventory, PlayerContext objectContext)
    {
        for (int i = 0; i <= fromList.Count - 1; i++) 
        { 
            FoodItemTemplate _template = fromList[i];
            Item newItem = new FoodItem(_template, objectContext, _template.stackWeight, 100);
            _inventory.AddItem(newItem);
        }
    }
    public void ManufactureToolItemInstances(List<ToolItemTemplate> fromList, Inventory _inventory, ObjectContext objectContext)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            ToolItemTemplate _template = fromList[i];
            Item newItem = new ToolItem(_template, objectContext, _template.stackWeight, 100);
            _inventory.AddItem(newItem);
        }
    }
    public void ManufactureFirstAidItemInstances(List<FirstAidItemTemplate> fromList, Inventory _inventory, ObjectContext objectContext)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            FirstAidItemTemplate _template = fromList[i];
            Item newItem = new FirstAidItem(_template, objectContext, _template.stackWeight, 100);
            _inventory.AddItem(newItem);
        }
    }
    public void ManufactureClothesItemInstances(List<ClothesItemTemplate> fromList, Inventory _inventory, ObjectContext objectContext)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            ClothesItemTemplate _template = fromList[i];
            Item newItem = new ClothesItem(_template, objectContext, _template.stackWeight, 100);
            _inventory.AddItem(newItem);
        }
    }
    public void ManufactureMaterialItemInstances(List<MaterialItemTemplate> fromList, Inventory _inventory, ObjectContext objectContext)
    {
        for (int i = 0; i <= fromList.Count - 1; i++)
        {
            MaterialItemTemplate _template = fromList[i];
            Item newItem = new MaterialItem(_template, objectContext, _template.stackWeight, 100);
            _inventory.AddItem(newItem);
        }
    }
}
