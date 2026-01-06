using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using Random = UnityEngine.Random;
public class WeatherController : MonoBehaviour
{
    public SimTime simTime;
    public FiniteStateMachine weatherFSM;
    public ObjectContext weatherContext;

    public float ambientAirTemp;
    public float windChill;
    public float windSpeed;
    public float targetAmbientAirTemp;
    public float targetWindChill;

    public int weatherVolatility;
    public int clearChance;
    public int fogChance;
    public int windLightChance;
    public int windMediumChance;
    public int windHighChance;
    public int blizzardChance;

    private float currentAmbientAirTemp;
    private float currentWindChill;

    private int weatherIndex;
    private int weatherDuration;
    
    private int snowAmount;
   
    private int weatherLength;
    private int elapsedHours;
    private bool snowing;

    private State clear;
    private State fog;
    private State windLight;
    private State windMedium; 
    private State windHigh;
    private State blizzard;

    List<State> stateListProbability = new List<State>();

    private void HandleAirTemps()
    {
        // Increment current air temp to target air temp
        if (currentAmbientAirTemp == targetAmbientAirTemp)
        {
            return;
        }
        else
        {
            currentAmbientAirTemp = Mathf.MoveTowards(
                currentAmbientAirTemp, 
                targetAmbientAirTemp, 
                Time.deltaTime * 2);     
        }
        
        ambientAirTemp = currentAmbientAirTemp;
    }
    
    private void WeatherDurationSimulator()
    {
        weatherDuration--;
        
        if (weatherDuration == 0)
        {
            RollWeather();
        }
    }

    private void RollWeather()
    {
        // Roll random range, Current weather will be shifted left or right by _dice amount
        // This prevents huge weather jumps, maximum weather severity jump adjusted by weatherVolatility

        int dice = Random.Range(-1 * weatherVolatility, weatherVolatility);

        // Define end list region
        // Prevent Start and end regions heavy chance weighting
        
        int _endlistRegion = stateListProbability.Count - 1;       
        _endlistRegion -= blizzardChance;

        if (weatherIndex <= clearChance  && dice < 0)
        {
            dice *= -1;
        }
        else if (weatherIndex >= _endlistRegion && dice > stateListProbability.Count - 1)
        {
            dice *= -1;
        }

        weatherIndex += dice;
        weatherIndex = Mathf.Clamp(weatherIndex, 0, stateListProbability.Count - 1);
        
        // Roll Weather Duration

        weatherDuration = Random.Range(6, 8);

        // Enqueue Rolled Weather
        
        weatherFSM.EnqueueState(stateListProbability[weatherIndex]);
    }

    private void PopulateWeatherList(int _probability, State _state)
    {
        for (int i = 0; i < _probability; i++)
        {
            stateListProbability.Add(_state);
        }
    }
    
    private void HandleWindChill()
    {
        if (currentWindChill == targetWindChill)
        {
            return;
        }
        else
        {
            currentWindChill = Mathf.MoveTowards(
                currentWindChill,
                targetWindChill,
                Time.deltaTime);
        }
        
        windChill = currentWindChill;
    }
    
    private void Awake()
    {
        // Object Context for state enqueing
        
        weatherContext = new ObjectContext
        {
            weatherController = GetComponent<WeatherController>()
        };
        
        weatherFSM = GetComponent<FiniteStateMachine>();
        elapsedHours = 0;

        // Init State storage
        // Weather State Array
        
        clear       = new WeatherClearState(weatherFSM, weatherContext);
        fog         = new WeatherFogState(weatherFSM, weatherContext);
        windLight   = new WeatherWindLightState(weatherFSM, weatherContext);
        windMedium  = new WeatherWindMediumState(weatherFSM, weatherContext);
        windHigh    = new WeatherWindHighState(weatherFSM, weatherContext);
        blizzard    = new WeatherBlizzardState(weatherFSM, weatherContext);

        PopulateWeatherList(clearChance, clear);
        PopulateWeatherList(fogChance, fog);
        PopulateWeatherList(windLightChance, windLight);
        PopulateWeatherList(windMediumChance, windMedium);
        PopulateWeatherList(windHighChance, windHigh);
        PopulateWeatherList(blizzardChance, blizzard);

        // Subscribe WeatherDuration to SimTime's Elapsed Hour Event
        // Set Current Weather to Clear

        weatherIndex = 0; 

        RollWeather();
        simTime.OnSimulationHour += WeatherDurationSimulator;
    }
    
    private void Update()
    {
        HandleAirTemps();      
        HandleWindChill();
    }
}
