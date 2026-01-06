using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFatigue : MonoBehaviour
{
    public float currentFatigue;
    public float maxFatigue;
    public float drainRate;
    public float maxDrainRate;

    public int myfatigue;

    public Action isTired;
    public Action isSleepDeprived;

    public SimTime simTime;
    private void HandleFatigueDrain()
    {
        if (currentFatigue > 0)
        {
            currentFatigue -= 1 * drainRate;
        }
    }
    
    private void Awake()
    {
        maxFatigue = 100;
        currentFatigue = maxFatigue;
        maxDrainRate = 0.1f;
        drainRate = 0.1f;

        simTime.OnSimulationTick += HandleFatigueDrain;
    }

    // Update is called once per frame
    void Update()
    {
        myfatigue = Mathf.RoundToInt(currentFatigue);
        myfatigue = Mathf.Clamp(myfatigue, 0, Mathf.RoundToInt(maxFatigue));

        if (myfatigue == 50)
        {
            isTired?.Invoke();
        }
        else if (myfatigue == 0)
        {
            isSleepDeprived?.Invoke();
        }
    }

    private void OnGUI()
    {
      GUI.Label(new Rect(0, 920, 300, 20), "Fatigue: " + myfatigue.ToString() + "%");
    }
}
