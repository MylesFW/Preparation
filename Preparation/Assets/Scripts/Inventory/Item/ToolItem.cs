using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItem : Item
{
    public bool equipable;
    public ToolItemTemplate template;
    public ObjectContext playerContext;
    
    public virtual void OnInteract()
    {
    
    }

    public ToolItem(ToolItemTemplate _template, ObjectContext _context, float _weight, float _condition, int _currentStackAmount = 1)
    {
        // pass vars
        template = _template;

        playerContext = _context;

        sprite = _template.sprite;
        worldSprite = _template.worldSprite;
        overlay = _template.overlaySprite;

        name = _template.itemName;

        description = _template.description;

        stackable = _template.stackable;

        indefiniteShelfLife = _template.indefiniteShelfLife;

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
    }
}
