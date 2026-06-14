using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesItem : Item
{
    // Clothes Item Class, subtype of Item. Contains fields and methods specific to clothes items
    // Data stored in TemplateSO, passed in on construction
    // Brennan RF(1): 2/21/26
    
    // Fields
    public readonly ClothesItemDataSO data;

    // Constructor
    public ClothesItem(ClothesItemDataSO _Data, PlayerContext _context)
    {
        data = _Data;
        Context = _context;
        ItemName = data.ItemName;
        Description = data.Description;
        BaseWeight = data.BaseWeight;
        IsStackable = data.IsStackable;
    }

    // Methods
    public override Item Copy()
    {
        ClothesItem copy = new ClothesItem(data, Context);
        copy.BaseWeight = BaseWeight;
        return copy;
    }
}
