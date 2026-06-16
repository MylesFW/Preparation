using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItem : Item
{
    public readonly MaterialItemDataSO data;
    public ObjectContext playerContext;

    // Constructor
    public MaterialItem(MaterialItemDataSO _Data, PlayerContext _context)
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
        MaterialItem copy = new MaterialItem(data, Context);
        copy.BaseWeight = BaseWeight;
        return copy;
    }
}
