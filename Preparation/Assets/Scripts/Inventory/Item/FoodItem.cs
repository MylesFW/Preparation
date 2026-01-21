using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;

public class FoodItem : Item
{
    public FoodItemTemplate template;
    public PlayerContext playerContext;

    public bool isBeverage;
    
    public float calories;
    public float hydrate;
    
    public float hydrateDensity;
    public float caloricDensity;
    public override void OnConsume()
    {
        float missingCalories = playerContext.playerCalories.maxCalories - playerContext.playerCalories.currentCalories;

        if (missingCalories >= calories)
        {
            playerContext.playerCalories.currentCalories += Mathf.RoundToInt(calories);
            playerContext.playerThirst.thirstLevel += Mathf.RoundToInt(hydrate);
            calories = 0;
        }
        else if (missingCalories < calories)
        {
            playerContext.playerCalories.currentCalories += Mathf.RoundToInt(missingCalories);
            calories -= missingCalories;
            currentStackWeight = calories / caloricDensity;
            currentWeight = currentStackAmount;
        }
    }
    public FoodItem(FoodItemTemplate _template, PlayerContext _context, float _weight, float _condition, int _currentStackAmount = 1)
    {
        // pass vars
        template = _template;

        playerContext = _context;

        name = _template.itemName;

        description = _template.description;

        isBeverage = _template.isBeverage;
        
        stackable = _template.stackable;
        
        indefiniteShelfLife = _template.indefiniteShelfLife;
        
        caloricDensity = _template.caloricDensity;
        
        calories = _weight * _template.caloricDensity;

        hydrate = _weight * _template.hydrateDensity;
        
        currentStackAmount = _currentStackAmount;
        
        stackWeight = _template.stackWeight;
        
        decayRate = _template.decayRate;
        
        condition = _condition;

        if (_weight <= 0)
        {
            currentWeight = stackWeight;
        }
        else if (_weight > 0)
        {
            currentWeight = _weight;
        }

        if (stackable == false)
        {
            currentStackAmount = Mathf.Clamp(currentStackAmount, 1f, 1f);
            currentStackWeight = currentWeight * currentStackAmount;
        }
        else if (stackable == true)
        {
            currentStackWeight = stackWeight * currentStackAmount;
        }

        calories = Mathf.RoundToInt(calories);
        hydrate = Mathf.RoundToInt(hydrate);
    }
       
}
