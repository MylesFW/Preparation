using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimTime : MonoBehaviour
{
    // Events
    
    public Action OnSimulationTick;
    public Action OnSimulationHour;
    public Action OnSimulationDay;

    // Simulation min, hr, days
    
    [HideInInspector] public int currentDay;
    [HideInInspector] public int currentTime;
    [HideInInspector] public int elapsedHours;
    [HideInInspector] public float elapsedFrame;
    [HideInInspector] private int elapsedMinuets, elapsedDays;
    public float minuteScale;
    
    // Clock
    
    [HideInInspector] public int militaryTime;
    [HideInInspector] private int clockMinute, clockHour;
    [HideInInspector] private string myMilitaryTime, minString, hourString, clockTime;
    [HideInInspector] private string am_pm;

   
    // Methods
    private void ElapseSimTime()
    {
        // seconds
        
        elapsedFrame += Time.deltaTime;
        militaryTime = ((elapsedHours * 100) + elapsedMinuets);

        // minutes
        
        if (elapsedFrame >= minuteScale)
        {
            OnSimulationTick?.Invoke();
            
            elapsedMinuets++;
            clockMinute++;
            militaryTime++;
            
            elapsedFrame = 0f;
        }
        
        // hours
        
        if (elapsedMinuets > 59)
        {
            OnSimulationHour?.Invoke();
            elapsedHours++;
            elapsedMinuets = 0;
            militaryTime  = elapsedHours * 100;
        }
        
        // days
        
        if (elapsedHours > 23)
        {
            OnSimulationDay?.Invoke();
            elapsedDays++;
            elapsedHours = 0;
            militaryTime = 0;
        }
        
        currentTime = militaryTime;
        currentDay = elapsedDays;
    }
    private void Twelvehourclock() 
    {
        // AM--PM
        
        if (elapsedHours == 12)
        {
            am_pm = "PM";            
        } 
        else if (elapsedHours < 12)
        {
            am_pm = "AM";
        }

        // 12 Hour Clock -- Minutes
        
        if (clockMinute > 59)
        {
            clockHour++;
            clockMinute = 0;
        }

        // 12 Hour Clock -- Hours
        
        if (clockHour > 12)
        {
            clockHour = 1;
        }

        // convert Int minutes to String Minutes
        
        if (clockMinute < 10)
        {
            minString = "0" + clockMinute.ToString();
        }
        else if (clockMinute >= 10)
        {
            minString = clockMinute.ToString();
        }

        // convert int hours to string hours. 
        
        hourString = clockHour.ToString();
        clockTime = hourString + ":" + minString + " " + am_pm;

        myMilitaryTime = militaryTime.ToString();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Simtime inits

        currentTime = 0;

        elapsedFrame = 0;
        elapsedMinuets = 0;
        elapsedHours = 6;
        elapsedDays = 0;

        // value of 300 mimics TLD's 15min per hour        
        //Clock inits

        militaryTime = 600;
        clockHour = 6;
        clockMinute = 0;

        am_pm = "AM";
    }

    // Update is called once per frame
    void Update()
    {
        ElapseSimTime();
        Twelvehourclock();
    }

    private void OnGUI()
    {    
        GUI.Label(new Rect(910, 100, 100, 20), clockTime);
        GUI.Label(new Rect(910, 120, 100, 20), myMilitaryTime);
    }
}