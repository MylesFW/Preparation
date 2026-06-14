using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using UnityEngine;
using Random = UnityEngine.Random;
public class WeatherController : Singleton<WeatherController>, ISavable
{
    public SimTime simTime;
    public FiniteStateMachine weatherFSM;
    public ObjectContext weatherContext;
    public VoidEventChannelSO OnSimulationHour;

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

    private float ambientAirTemp;
    private float currentAmbientAirTemp;
    private float currentWindChill;

    private int weatherIndex;
    private int weatherDuration;

    private State clear;
    private State fog;
    private State windLight;
    private State windMedium; 
    private State windHigh;
    private State blizzard;

    private List<State> stateListProbability = new List<State>();
    private List<State> stateList = new List<State>();

    public float AmbientAirTemp { get{ return ambientAirTemp;} }

    protected override void Awake()
    {
        base.Awake();
        weatherFSM = GetComponent<FiniteStateMachine>();
        weatherContext = new ObjectContext
        {
            weatherController = GetComponent<WeatherController>()
        };       
        
        clear       = new WeatherClearState(weatherFSM, weatherContext);
        fog         = new WeatherFogState(weatherFSM, weatherContext);
        windLight   = new WeatherWindLightState(weatherFSM, weatherContext);
        windMedium  = new WeatherWindMediumState(weatherFSM, weatherContext);
        windHigh    = new WeatherWindHighState(weatherFSM, weatherContext);
        blizzard    = new WeatherBlizzardState(weatherFSM, weatherContext);

        stateList.Add(clear);
        stateList.Add(fog);
        stateList.Add(windLight);
        stateList.Add(windMedium);
        stateList.Add(windHigh);
        stateList.Add(blizzard);
        
        RollWeather();

        OnSimulationHour.onEventRaised += WeatherDurationSimulator;
    }

    private void Update()
    {
        HandleAirTemps();      
        HandleWindChill();
    }

    private State GetStateByName(string name)
    {
        foreach (State weather in stateList)
        {
            if (weather.name == name)
            {
                return weather;
            }
        }
        return new WeatherClearState(weatherFSM, weatherContext);
    }



    // Loads available weather state types. Apply recursive adding per type to weight-
    // the randomizer.
    private void ManufactureWeatherList()
    {
        PopulateWeatherList(clearChance, clear);
        PopulateWeatherList(fogChance, fog);
        PopulateWeatherList(windLightChance, windLight);
        PopulateWeatherList(windMediumChance, windMedium);
        PopulateWeatherList(windHighChance, windHigh);
        PopulateWeatherList(blizzardChance, blizzard);
    }

    // Increment current air temp to target air temp
    private void HandleAirTemps()
    {
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

    // updates lifetime and changing of weather state
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
        ManufactureWeatherList();
        
        // Roll random range, Current weather will be shifted left or right by _dice amount
        // This prevents huge weather jumps, maximum weather severity jump adjusted by weatherVolatility
        int dice = Random.Range(-1 * weatherVolatility, weatherVolatility);

        // Define end list region
        // Prevent Start and end regions heavy chance weighting
        int _endlistRegion = stateListProbability.Count - 1;
        _endlistRegion -= blizzardChance;

        if (weatherIndex <= clearChance && dice < 0)
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
        weatherDuration = Random.Range(1, 1);
        weatherFSM.EnqueueState(stateListProbability[weatherIndex]);
        stateListProbability.Clear();
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

    GameData ISavable.SaveInstance(GameData data)
    {
        data.weatherData.currentWeather = weatherFSM.CurrentState.name;
        data.weatherData.weatherDuration = weatherDuration; 
        return data;
    }

    void ISavable.LoadInstance(GameData data)
    {
        
        weatherFSM.EnqueueState(GetStateByName(data.weatherData.currentWeather));
        weatherDuration = data.weatherData.weatherDuration;
    }

    void ISavable.NewGame()
    {
        RollWeather();
    }
}
