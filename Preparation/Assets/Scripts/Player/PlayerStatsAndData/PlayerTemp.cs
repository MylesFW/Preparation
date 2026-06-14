using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp : MonoBehaviour, ISavable
{

    public VoidEventChannelSO onSimulationTick;
    public WeatherController weather;
    public SimTime simTime;

    public float ambientTemp;
    public float currentPlayerTemp;
    public float severityMultiplier;
    public float feelsLike;
    public float maxPlayerTemp;
    public float windChill;

    public int playerTempPercentage;
    public int feelsLikeInt;

    public Action isCold;
    public Action isFreezing;

    private void HandlePlayerTemps()
    {
        feelsLike = weather.AmbientAirTemp;
        feelsLike += weather.windChill;

        if (feelsLike < 0 && currentPlayerTemp > 0)
        {
            currentPlayerTemp -= feelsLike * (severityMultiplier * -1.3f);
        }      
        else if (feelsLike > 0 && currentPlayerTemp < maxPlayerTemp)
        {
            currentPlayerTemp += feelsLike * (severityMultiplier * -2 - 1);
        }
        currentPlayerTemp = Mathf.Clamp(currentPlayerTemp, -200, maxPlayerTemp);
    }    
                

    private void Awake()
    {               
        maxPlayerTemp = 100;
        currentPlayerTemp = maxPlayerTemp;

        severityMultiplier = 0.0098f;

       onSimulationTick.onEventRaised += HandlePlayerTemps;
    }

    private void Start()
    {
        GameObject inst = ObjectRegistry.instance.Get("WeatherCycle");
        if (inst != null)
        {
            var wcycle = inst.GetComponent<WeatherController>();
            weather = wcycle;
        }
    }


    // Update is called once per frame
    void Update()
    {
        feelsLikeInt = Mathf.RoundToInt(feelsLike);

        playerTempPercentage = Mathf.RoundToInt(currentPlayerTemp);
        playerTempPercentage = Mathf.Clamp(playerTempPercentage, 0, Mathf.RoundToInt(maxPlayerTemp));

        if (playerTempPercentage == 0)
        {
            isFreezing?.Invoke();
        }
        else if (playerTempPercentage == 50)
        {
            isCold?.Invoke();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 860, 300, 20), "Feels Like: " + feelsLikeInt.ToString() + " C");
        GUI.Label(new Rect(0, 880, 300, 20), "Freezing: " + playerTempPercentage.ToString() + "%");
        
    }

    GameData ISavable.SaveInstance(GameData data)
    {
        data.playerData.playerTemp = currentPlayerTemp;
        return data;
    }

    void ISavable.LoadInstance(GameData data)
    {
        currentPlayerTemp = data.playerData.playerTemp;
    }

    void ISavable.NewGame()
    {
        currentPlayerTemp = maxPlayerTemp;
    }
}
