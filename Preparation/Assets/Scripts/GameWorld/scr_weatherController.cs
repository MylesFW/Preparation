using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scr_weatherController : MonoBehaviour
{
    //=============================================================================
    //-------------------------- Weather Controller -------------------------------
    //=============================================================================
    //Handles all things related to weather and day/night Cycle    
    #region Class Items

    //Ambient air temp 
    double feels_like         = 0;    //Air temp - windchill = feels like
    double air_temp           = 0;    //Rounded air temp
    double current_air_temp   = 0;    //Unrounded air temp 
    double new_air_temp       = 0;    //Rate of temperature change
    float air_temp_min        = 0;    //The minimum air temp of the current day
    float air_temp_max        = 0;    //The maximum air temp of the current day

    //Snow 
    ParticleSystem snowPS;
    float current_snow_amount = 0;    //Current emission amount of the snow particel system
    float new_snow_amount     = 0;    //New emission amount
    float snow_amount_min     = 0;    //Min Snow amount for the current weather event
    float snow_amount_max     = 50;   //Max Snow amount for the current weather event
    float snow_fall           = 0;
    float peak_snow           = 0;


    float snow_subtract = 0;
    float wind_subtract = 0;
    
    
    
    //wind
    float current_wind_speed  = 0;
    float new_wind_speed      = 0;
    float wind_speed          = 0;    //current wind speed     
    float wind_min            = 0;    //Minimum wind speed (used by state rng)
    float wind_max            = 0;    //Maximum wind speed (used by state rng)
    float wind_direction      = 0;    //wind direction (1 = right, -1 = left)
    float peak_wind           = 0;

    //day and night cycle
    int time_of_day           = 0;    //Current time of day (millitary time)
    int time_interval         = 0;    //in game time debuf
    int time_interval_timer   = 0;    //TOD Timer

    //------------------------ Class Functions -------------------------------------
    //Weather randomizer function
    void RandomizeWeather()         //Weather state randomizer, called before state switch 
    {
        //randomizer local variables
        var _rng                  = 0;  //Value that will hold the result of our random number generator
        var _rangemax             = 10; //Max value of rng roll, higher value = higher probability fidelty 
        var _rangemin             = 0;  //Min value of rng roll, keep 0
        var _clear_probability    = 2;  //State probability, higher value = higher probability of weather
        var _fog_probability      = 3;  //Do not leave gaps in the ranges
        var _windy_probability    = 7;  //_Blizzard_probability must equal _rngmax
        var _blizzard_probability = 10;

        _rng = Random.Range(_rangemin, _rangemax);

        //Assign ranges to each weather state, the wider the range, the higher the chance it's picked by _rng
        //*The min value starts at the last state's probability. The max value is the current state's value being checked.
        //(the above is not crucial to functionality, but uses less variables to define ranges)   

        if (_rng >= 0 && _rng <= _clear_probability)
        {
            current_weather = Weather.clear;
        }
        else if (_rng > _clear_probability && _rng <= _fog_probability)
        {
            current_weather = Weather.fog;
        }
        else if (_rng > _fog_probability && _rng <= _windy_probability)
        {
            current_weather = Weather.windy;
        }
        else if (_rng > _windy_probability && _rng <= _blizzard_probability)
        {
            current_weather = Weather.blizzard;
        }
    }

    //-------------------------- Class Enums ---------------------------------------
    //Weather enum for state machine
    public enum Weather { clear, fog, windy, blizzard, } //Enum for the different weather states, possible to add more in the future
    Weather current_weather = Weather.clear;             //Current_weather, a variable of the Weather type, holding the current weather
    int weather_duration = 0;                            //Weather duration timer
    int weather_peak_point = 0;
    #endregion 
    //=============================================================================
    //------------------------------- Start ---------------------------------------
    //=============================================================================
    #region Start
    void Start()
    {

        //Variable inits, do not change these values 
        time_interval    = 300;       //3000 is the interval used in TLD; 5 minutes per one in game hour  
        current_air_temp = -20;      //assign starting value                    
        time_of_day      = 650;      //starting time of day
        current_weather  = Weather.clear;
        air_temp_max = Random.Range(-10, -1);
        air_temp_min = Random.Range(-23, -10);
        wind_direction = (Random.Range(-1, 1));

    }
    #endregion
    //=============================================================================
    //------------------------------- Update --------------------------------------
    //=============================================================================
    #region Step (update)
    void Update()
    {

        //======================= Day and Night Cycle ===============================

        //Timer advance 
        if (time_interval_timer < time_interval)
        {
            time_interval_timer += 1;
        }
        //Timer trigger
        else if (time_interval_timer == time_interval)
        {
            time_of_day += 1;
            time_interval_timer = 0;
        }
        //Resets time of day at 12am
        if (time_of_day > 2399) { time_of_day = 0; }

        //===================== Weather and Temperature ==============================

        //Start of day, Reroll for high temps for the next 12 hours
        if (time_of_day == 700)
        {
            air_temp_max = Random.Range(-10, -1);
            new_air_temp = ((current_air_temp - air_temp_max) / 1200) * -1;
            new_snow_amount = ((current_snow_amount - snow_amount_max) / 1200) * -1;
            new_wind_speed = ((current_wind_speed - wind_max) / 1200) * -1;
            
        }
        //Start of Evening, Reroll for low temps for the next 12 hours
        else if (time_of_day == 1901)
        {
            air_temp_min = Random.Range(-23, -10);
            new_air_temp = ((air_temp_min - current_air_temp) / 1200) * -1;
            new_snow_amount = ((current_snow_amount - snow_amount_min) / 1200) * -1;
            new_wind_speed = ((current_wind_speed - wind_min) / 1200) * -1;            
        }
        
        snowPS = GetComponent<ParticleSystem>();
        var emission = snowPS.emission;
        var velocity = snowPS.velocityOverLifetime;
        var trails   = snowPS.trails;
        var noise    = snowPS.noise; 
        emission.rateOverTime = current_snow_amount * 2;
        




        //State switch
        if (weather_duration == 0)
        {
            RandomizeWeather();
            switch (current_weather)
            {
                //------ CLEAR -----------------
                case Weather.clear:
                    wind_min = 0;
                    wind_max = 0;
                    weather_duration = Random.Range(1, 12);
                    weather_duration = (weather_duration * time_interval * 100);
                    snow_amount_min = Random.Range(0, 1);
                    snow_amount_max = Random.Range(2, 5);
                    if (snowPS.particleCount == 0)
                    {
                        wind_direction = Random.Range(-1, 1);
                    }                    
                    weather_peak_point = weather_duration / 4;
                    break;
                //------- FOG ------------------
                case Weather.fog:
                    air_temp_max -= 2;
                    air_temp_min -= 5;
                    wind_min = 0;
                    wind_max = 0;
                    weather_duration = Random.Range(5, 12);
                    weather_duration = (weather_duration * time_interval * 100);
                    snow_amount_min = 0;
                    snow_amount_max = 0;
                    current_snow_amount = 0;
                    weather_peak_point = weather_duration / 4;
                    break;
                //------- WINDY ----------------
                case Weather.windy:
                    air_temp_max -= 3;
                    air_temp_min -= 3;
                    wind_min = 5;
                    wind_max = 9;
                    weather_duration = Random.Range(4, 12);
                    weather_duration = (weather_duration * time_interval * 100);
                    snow_amount_min = Random.Range(5, 50);
                    snow_amount_max = Random.Range(51, 75);
                    weather_peak_point = weather_duration / 4;
                    break;
                //------ BLIZZARD --------------
                case Weather.blizzard:
                    air_temp_max -= 10;
                    air_temp_min -= 10;
                    wind_min = 10;
                    wind_max = 20;
                    weather_duration = Random.Range(6, 12);
                    weather_duration = (weather_duration * time_interval * 100);
                    snow_amount_min = Random.Range(75, 149);
                    snow_amount_max = Random.Range(150, 200);
                    weather_peak_point = weather_duration/4;
                    break;
              
            }
        }
       else if (weather_duration > 0)
        {
            weather_duration--;
        }
        
        
      //Cutted


        //Day Temp Cycle - (7:00 AM to 7:00 PM) 
        if (time_of_day > 700 && time_of_day < 1900)
        {
            if ((time_interval_timer == time_interval) && (current_air_temp < air_temp_max))
            {
                current_air_temp += new_air_temp;
            }
        }
        //Night Temp Cycle - (7:00 PM to 7:00 Am)
        else if (time_of_day > 1900 || time_of_day < 700)
        {
            if ((time_interval_timer == time_interval) && (current_air_temp > air_temp_min))
            {
                current_air_temp -= new_air_temp;
            }
        }
        //Day Snow Cycle - (7:00 AM to 7:00 PM) 
        if (time_of_day > 700 && time_of_day < 1900)
        {
            if ((time_interval_timer == time_interval) && (current_snow_amount < snow_amount_max))
            {
                current_snow_amount += new_snow_amount;
            }
        }
        //Night Snow Cycle - (7:00 PM to 7:00 Am)
        else if (time_of_day > 1900 || time_of_day < 700)
        {
            if ((time_interval_timer == time_interval) && (current_snow_amount > snow_amount_min))
            {
                current_snow_amount -= new_snow_amount;
            }
        }
        //Day Snow Cycle - (7:00 AM to 7:00 PM) 
        if (time_of_day > 700 && time_of_day < 1900)
        {
            if ((time_interval_timer == time_interval) && (current_wind_speed < wind_max))
            {
                current_wind_speed += new_wind_speed;
            }
        }
        //Night Snow Cycle - (7:00 PM to 7:00 Am)
        else if (time_of_day > 1900 || time_of_day < 700)
        {
            if ((time_interval_timer == time_interval) && (current_wind_speed > wind_min))
            {
                current_wind_speed -= new_wind_speed;
            }
        }

        //-----------------------------------------------------CP
        if (weather_duration == weather_peak_point)
        {
            peak_wind = current_wind_speed;
            peak_snow = current_snow_amount;
            wind_min = 0;
            snow_amount_min = 0;
            snow_subtract = snow_amount_max / weather_peak_point;
            wind_subtract = wind_max / weather_peak_point;
        }

        else if (weather_duration < weather_peak_point)
        {
            current_snow_amount -= snow_subtract;
            current_wind_speed -= wind_subtract;
        }










        //snow amount max override
        if (current_snow_amount > snow_amount_max)
        {
            current_snow_amount--;
        }
        //snow amount min override
        else if (current_snow_amount < snow_amount_min)
        {
            current_snow_amount++;
        }
        //wind max override
        if (current_wind_speed > wind_max)
        {
            current_wind_speed--;
        }
        //wind min override
        else if (current_wind_speed < wind_min)
        {
            current_wind_speed++;
        }







        //Round to 0.00 digits because it looks cleaner
        air_temp = Math.Round(current_air_temp, 2);
        //snow_fall = Mathf.FloorToInt(current_snow_amount);
        //wind_speed = Mathf.FloorToInt(current_wind_speed);
        //Air temp and windchill
        feels_like = Math.Round(current_air_temp - current_wind_speed, 1);

        //=========================== Snow particle ==========================================
        

        //Prevents from rolling a zero
        if (wind_direction == 0)
        {
            wind_direction = 1;
        }
        //apply wind speed and velocity to particles   
        if (current_wind_speed != 0)
        {
            velocity.xMultiplier = wind_direction * current_wind_speed * 2;
            velocity.yMultiplier = -1 - Mathf.Abs(current_wind_speed / 2);
        }
        //trails
        if (current_wind_speed > 9)
        {
            trails.lifetimeMultiplier = (current_wind_speed / 200);
            noise.strengthXMultiplier = (current_wind_speed / 500);
        }
        else
        {
            trails.lifetimeMultiplier = 0;
        }
    }   
    #endregion
    //=============================================================================
    //-------------------------------- GUI ----------------------------------------
    //=============================================================================
    #region GUI
    private void OnGUI()
    {
        int _yoffset = 30;
        
        
            GUI.Label(new Rect(500, 460 + _yoffset, 500, 1000), "Time of Day:" + time_of_day.ToString());
            GUI.Label(new Rect(500, 480 + _yoffset, 500, 1000), "Weather:" + current_weather.ToString());
           // GUI.Label(new Rect(500, 540 + _yoffset, 500, 1000), "Day Max:" + air_temp_max.ToString() + " C");
            //GUI.Label(new Rect(500, 520 + _yoffset, 500, 1000), "Night Min: " + air_temp_min.ToString() + " C");
            GUI.Label(new Rect(500, 500 + _yoffset, 500, 1000), "Air Temp:" + air_temp.ToString() + " C");
           // GUI.Label(new Rect(500, 560 + _yoffset, 500, 1000), "snow Amount:" + current_snow_amount.ToString());
            //GUI.Label(new Rect(500, 580 + _yoffset, 500, 1000), "snow Min:" + snow_amount_min.ToString());
            //GUI.Label(new Rect(500, 600 + _yoffset, 500, 1000), "snow Max:" + snow_amount_max.ToString());
            //GUI.Label(new Rect(500, 620 + _yoffset, 500, 1000), "Feels Like:" + feels_like.ToString() + " C");
            //GUI.Label(new Rect(500, 640 + _yoffset, 500, 1000), "wind Speed:" + current_wind_speed.ToString());
            //GUI.Label(new Rect(500, 660 + _yoffset, 500, 1000), "wind Min:" + wind_min.ToString());
            //GUI.Label(new Rect(500, 680 + _yoffset, 500, 1000), "wind Max: " + wind_max.ToString());
            //GUI.Label(new Rect(500, 700 + _yoffset, 500, 1000), "PeakPoit: " + weather_peak_point.ToString());
           // GUI.Label(new Rect(500, 720 + _yoffset, 500, 1000), "Duration: " + weather_duration.ToString());
        #endregion
    }

}