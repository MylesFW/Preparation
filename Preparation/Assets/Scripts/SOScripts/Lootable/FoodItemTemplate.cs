using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Templates", menuName = "Food Item")]
public class FoodItemTemplate : ScriptableObject
{

    // Base Items

    public Sprite sprite;

    public Sprite worldSprite;

    public string itemName;
    
    public string description;

    public float probability;

    public float decayRate;

    public float stackWeight;

    public bool stackable;

    public bool indefiniteShelfLife;

    // food specific
    public bool isBeverage;

    public float hydrateDensity;
    
    public float caloricDensity;
}

