using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Item
{
    // Base Item Class all item types inherit from
    // Brennan RF(1): 2/21/26

    // Current Item condition and decay rate
    // Base weight of a single Item, this is used to calculate the weight of a stack of items
    private string itemName;
    private string description;
    private float baseWeight;
    private float decayRate;
    public bool isIndefinite;
    private bool isStackable;
    private ItemDataSO data;
    private PlayerContext context; // Light weight injection

    // Getters and Setters
    public ItemDataSO ItemData
    {
        get => data;
        set => data = value;
    }
    public ItemDataSO Data => data;
    public string ItemName
    {
        get => itemName;
        set => itemName = value;
    }
    public string Description
    {
        get => description;
        set => description = value;
    }
    public float BaseWeight
    {
        get => baseWeight;
        set => baseWeight = Mathf.Clamp(value, 0, 100000);
    }
    public float DecayRate
    {
        get => decayRate;
        set => decayRate = Mathf.Clamp(value, 0, 100);
    }
    public bool IsStackable
    {
        get => isStackable;
        set => isStackable = value;
    }
    public bool IsIndefinite
    {
        get => IsIndefinite;
        set => IsIndefinite = value;
    }

    public PlayerContext Context
    {
        get => context;
        set => context = value;
    }
    public abstract Item Copy();
}
