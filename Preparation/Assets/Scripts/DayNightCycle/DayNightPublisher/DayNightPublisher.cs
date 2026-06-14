using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightPublisher : MonoBehaviour
{
    [HideInInspector] public Action onNewDay;
    public bool enableLog;
    private SimTime simTime;
    private int currentDay;
    private int currentTime;


    private void Awake()
    {
        simTime = GetComponent<SimTime>();
        currentDay = simTime.currentDay;
        currentTime = simTime.militaryTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = simTime.militaryTime;
        if (currentTime == 600 && simTime.elapsedFrame == 0)
        {
            if (enableLog)
            {
                Debug.Log("You Survived another Night");
            }
           
            onNewDay?.Invoke();        
        }
    }
}
