using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoFoodItem : FoodItem
{
    float _calories;   
    float _hydration;

    public override void OnConsume()
    {
        float missingCalories = context.playerCalories.maxCalories - context.playerCalories.currentCalories;
        
        if (missingCalories >= calories)
        {
            context.playerCalories.currentCalories += Mathf.RoundToInt(calories);
            context.playerThirst.thirstLevel += Mathf.RoundToInt(hydrate);
            calories = 0;
        }
        else if (missingCalories < calories)
        {           
            context.playerCalories.currentCalories += Mathf.RoundToInt(missingCalories);
            calories -= missingCalories;
            currentStackWeight = calories / caloricDensity;
            currentWeight = currentStackAmount;
        }
    }

    public PotatoFoodItem(ObjectContext _context, float _weight = -1f, float _stack = 1f)
    {        
        this.name = "Potato";
        this.description = "A hardy root vegetable";
        
        this.isBeverage = false;
        this.stackable = false;
        this.indefiniteShelfLife = false;
        this.caloricDensity = 250;
        this.calories = currentWeight * caloricDensity;

        this.currentStackAmount = _stack;
        this.stackWeight = 0.5f;
        this.decayRate = 0.1f;
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
        
        _calories = this.caloricDensity * this.currentWeight;
        _hydration = this.hydrateDensity * this.currentWeight;


        this.calories = Mathf.RoundToInt(_calories);
        this.hydrate = Mathf.RoundToInt(_hydration);
    }
}

