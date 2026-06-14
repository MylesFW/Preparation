using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItem : Item
{
    // Tool Item Class, subtype of Item. Contains fields and methods specific to tool items
    // Data stored in TemplateSO, passed in on construction
    // Brennan RF(1): 2/21/26
    
    // Fields
    public bool equipable;
    public readonly ToolItemDataSO data;

    // Constructor
    public ToolItem(ToolItemDataSO _Data, PlayerContext _context)
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
        ToolItem copy = new ToolItem(data, Context);
        copy.BaseWeight = BaseWeight;
        return copy;
    }
}
