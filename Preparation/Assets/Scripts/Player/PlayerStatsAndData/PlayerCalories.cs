using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCalories : MonoBehaviour
{
    public float currentCalories;
    public float maxCalories;
    public float burnRate;
    public float maxBurnRate;
    
    public int kcal;

    public Action isHungry;
    public Action isStarving;

    public SimTime simTime;

    private void HandleCalorieBurn()
    {
        if (currentCalories > 0)
        {
            currentCalories -= 1 * burnRate;
        }
    }

    private void Awake()
    {
        maxCalories = 2000;

        currentCalories = maxCalories;
        maxBurnRate = 2f;
        burnRate = 1.4f;


        simTime.OnSimulationTick += HandleCalorieBurn;
    }

    // Update is called once per frame
    void Update()
    {
        kcal = Mathf.RoundToInt(currentCalories);
        kcal = Mathf.Clamp(kcal, 0, Mathf.RoundToInt(maxCalories)); 
        
        if (kcal == 500)
        {
            isHungry?.Invoke();
        }
        else if (kcal == 0)
        {
            isStarving?.Invoke();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 940, 300, 20), "Food: " + kcal.ToString() + " Kcal");
    }
}
