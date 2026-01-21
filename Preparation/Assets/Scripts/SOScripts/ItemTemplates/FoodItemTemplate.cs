using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Templates", menuName = "ItemTemplates/Food Item", order = 1)]
public class FoodItemTemplate : ScriptableObject
{

    // Base Items

    [Header("Sprite Images")]
    [Tooltip("The sprite Potrait")]
    public Sprite sprite;
    
    [Tooltip("Image for dropped in world Items")]
    public Sprite worldSprite;
    
    [Tooltip("Optional overlay (paired with portrait sprite)")]
    public Sprite overlaySprite;

    [Header("String Data ")]
    [Tooltip("Name of Item to be displayed in game")]
    public string itemName;

    [Tooltip("Description of Item to be displayed in game")]
    public string description;

    [Header("Probability")]
    [Range(0, 200)]
    [Tooltip("Higher number = more common; It is not a percantage value")]
    public int dropRate;
    
    [Header("Base Attributes")]
    [Tooltip("The rate the Item decays per simulation tick")]
    public float decayRate;

    [Tooltip("The weight of one stack of this item. Serves as Max Weight for non-stackable items")]
    public float stackWeight;

    [Tooltip("Non-Stackable: consume partial. Stackable: Cannot decay, cannot consume partial stack")]
    public bool stackable;

    [Tooltip("Decay rate override, no affect on stackable items as they cannot decay")]
    public bool indefiniteShelfLife;

    // food specific
    [Header("Food Item Attributes")]
    [Tooltip("Used for sorting, non-beverages can still hydrate")]
    public bool isBeverage;

    [Tooltip("Amount of hydration per 1kg of item")]
    public float hydrateDensity;

    [Tooltip("Amount of calorie per 1kg of item")]
    public float caloricDensity;

    public List<Buff> foodBuffs = new List<Buff>();
}

