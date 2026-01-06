using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewItem
{
    // Item Strings
    
    public string name;   
    public string description;

    // Current Item condition and decay rate

    public float condition;
    public float decayRate;

    // Current weight of a unique object (non stackable)
    // Looks like a duplicate value, but seperating is used for partial consumption of unique items;

    public float currentWeight;

    // Current weight of the sum of a stacked Item (stackWeight * CurrentStackAmount
    
    public float currentStackWeight;

    // Current stock amount, ie. inventory has 12 sticks, with an individual stick weight and a combined 
    // weight thats received by current stack ammount

    public float currentStackAmount;

    // The weight of 1 stack of a given Item. (This serves as the maximum weight for unique objects)
    // unique example: an inventory may contain full or partial amounts of canned beans but-
    // -the stackWeight will decide the the maximum amount of beans the can can hold.

    public float stackWeight;

    // self explanatory
    
    public bool stackable;
    
    public bool indefiniteShelfLife;

    public virtual void OnConsume() { }
    public ObjectContext context;

    private Sprite sprite;
    private Sprite worldSprite;
    public void UpdateStackWeight()
    {
        currentStackWeight = stackWeight * currentStackAmount;
    }
}
