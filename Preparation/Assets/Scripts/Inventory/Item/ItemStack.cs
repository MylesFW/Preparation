using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemStack
{
    // Class to represent a stack of items in the inventory,
    // with a reference to the item and the quantity.
    // Brennan 2/21/26

    // fields
    public readonly Item item;
    private float stackAmount;
    private float stackWeight;
    public float condition;

    // Constructor
    public ItemStack(Item _item, float _quantity = 1, float _condition = 100)
    {
        item = _item;
        stackAmount = Mathf.RoundToInt(_quantity);
        stackWeight = item.BaseWeight * stackAmount;
        condition = _condition;
    }

    // Properties
    public Item Item => item;
    public float Condition
    {
        get { return condition; }
        set { condition = value; }
    }
    public float StackWeight => stackWeight;
    public float StackAmount
    {
        get => stackAmount;
        set => stackAmount = Mathf.RoundToInt(stackAmount + value);
    }
    // Methods
    
    public void DecayItem()
    {
        if (item is FoodItem)
        {
            condition -= item.DecayRate;
        }
    }
    public float CalculateWeight()
    {
        // Calculate the total weight of the stack based on the base weight of the
        // item and the quantity
        stackWeight = item.BaseWeight * StackAmount;
        return stackWeight;
    }
    public void AddToStack(float amount)
    {
        stackAmount+= amount;
        CalculateWeight();
    }
    public bool RemoveFromStack(float amount)
    {
        stackAmount -= amount;
        CalculateWeight();
        if (StackAmount <= 0)
        {
            StackAmount = 0;
            return true;  // stack empty
        }
        else { return false; }
    }
    public ItemStack CopyStack()
    {
        var newStack = new ItemStack(this.item, this.StackAmount, this.condition);
        return newStack;
    }
}
