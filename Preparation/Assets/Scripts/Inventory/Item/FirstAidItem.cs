using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidItem : Item
{
    // First Aid Item Class, subtype of Item. Contains fields and methods specific to first aid items
    // Data stored in TemplateSO, passed in on construction
    // Brennan RF(1): 2/21/26
    
    // Fields
    public readonly FirstAidItemDataSO data;
    public ObjectContext playerContext;

    // Constructor
    public FirstAidItem(FirstAidItemDataSO _Data, PlayerContext _context)
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
        FirstAidItem copy = new FirstAidItem(data, Context);
        copy.BaseWeight = BaseWeight;
        return copy;
    }
}
