using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;

public class FoodItem : Item
{
    // Food Item Class, subtype of Item. Contains fields and methods specific to food items
    // Data stored in TemplateSO, passed in on construction
    // Brennan RF(1): 2/21/26

    // Fields
    public readonly FoodItemDataSO data;

    // Constructor
    public FoodItem(FoodItemDataSO _Data, PlayerContext _context)
    {
        data = _Data;
        Context = _context;       
        ItemName = data.ItemName;
        Description = data.Description;
        BaseWeight = data.BaseWeight;
        IsStackable = data.IsStackable;
        DecayRate = data.DecayRate;
    }
    public override Item Copy()
    {
        FoodItem copy = new FoodItem(data, Context);
        copy.BaseWeight = BaseWeight;
        return copy;
    }
}
