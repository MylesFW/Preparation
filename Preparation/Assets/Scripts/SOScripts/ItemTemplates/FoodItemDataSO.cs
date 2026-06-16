using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Templates", menuName = "ItemTemplates/Food Item", order = 1)]
public class FoodItemDataSO : ItemDataSO
{
    // Template for Food Items, contains all data fields relevant to food items,
    // used to construct food items in game.
    // Brennan RF(1): 2/21/26
    // Brennan RF(2): 3/4/26 -- offload base traits to parent SO

    [SerializeField]
    [Header("Food Item Attributes")]
    [Tooltip("Used for sorting, non-beverages can still hydrate")]
    private bool isBeverage;

    [SerializeField]
    [Tooltip("Amount of hydration per 1kg of item")]
    private float hydrateDensity;

    [SerializeField]
    [Tooltip("Amount of calorie per 1kg of item")]
    private float caloricDensity;

    public bool IsBeverage => isBeverage;
    public float HydrateDensity => hydrateDensity;
    public float CaloricDensity => caloricDensity;
}

