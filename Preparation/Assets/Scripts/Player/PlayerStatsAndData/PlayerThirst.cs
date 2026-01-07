using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirst : MonoBehaviour
{
    public SimTime simTime;
    public Action isThirsty;
    public Action isDehydrated;

    [HideInInspector] public float thirst;
    [HideInInspector] public float maxThirst;
    [HideInInspector] public float dehydrateRate;
    
    public float maxDehydrateRate;

    [HideInInspector] public int thirstLevel;
    
    private void HandleThirstDrain()
    {
        if (thirst > 0)
        {
            thirst -= 1 * dehydrateRate;
        }
    }

    private void Awake()
    {
        maxThirst = 100;
        thirst = maxThirst;
        maxDehydrateRate = 1f;
        dehydrateRate = 0.28f;
        simTime.OnSimulationTick += HandleThirstDrain;
    }

    // Update is called once per frame
    void Update()
    {
       thirstLevel = Mathf.RoundToInt(thirst);
       thirstLevel = Mathf.Clamp(thirstLevel, 0, Mathf.RoundToInt(maxThirst));
       
        if (thirstLevel == 50)
        {
            isThirsty?.Invoke();
        }
        else if (thirstLevel == 0)
        {
            isDehydrated?.Invoke();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 900, 300, 20), "Thirst: " + thirstLevel.ToString() + "%");
    }
}
