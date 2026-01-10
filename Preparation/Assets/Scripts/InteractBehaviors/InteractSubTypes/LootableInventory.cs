using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class LootableInventory : BaseInteractable
{
    private int index;
        
       

    public override void ExecuteInteraction(ObjectContext _self)
    {
        Inventory inventory = _self.inventory;
    }
}
