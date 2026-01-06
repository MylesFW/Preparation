using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLigthtToolItem : ToolItem
{

    public override void OnInteract()
    {
        
    }

    public override void OnConsume()
    {
       int _i = this.context.playerController.currentEquiped;
       this.context.playerController.equiped[_i] = this;
        Debug.Log(context.playerController.equiped[_i].name + " equiped");
    }


    public FlashLigthtToolItem(ObjectContext _context, float _weight = -1f, float _stack = 1f)
    {
        this.name = "Flash Light";
        this.description = "Somewhat old, but works just fine. Don't run out of batteries";

        this.stackable = false;
        this.indefiniteShelfLife = false;

        this.currentStackAmount = _stack;
        this.stackWeight = 8f;
        this.decayRate = 0.001f;
        this.condition = 100f;

        this.context = _context;

        if (_weight <= 0)
        {
            this.currentWeight = this.stackWeight;
        }
        else if (_weight > 0)
        {
            this.currentWeight = _weight;
        }

        if (stackable == false)
        {
            this.currentStackAmount = Mathf.Clamp(currentStackAmount, 1f, 1f);
            this.currentStackWeight = this.currentWeight * this.currentStackAmount;
        }
        else if (stackable == true)
        {
            this.currentStackWeight = this.stackWeight * this.currentStackAmount;
        }
    }
}
