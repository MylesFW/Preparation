using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataLoader
{
    // Creates and initializes game data at start of the program run
    public GameData InitGameData()
    {       
        GameData data = new GameData();
        data.playerData = new PlayerData();
        data.simData = new SimData();
        data.weatherData = new WeatherData();
              
        data.sceneList = new List<SceneData>();     
        int sceneAmount = SceneLibrary.instance.sceneNames.Count;
        for (int i = 0; i < sceneAmount; i++)
        {
            string name = SceneLibrary.instance.sceneNames[i];
            SceneData sceneData = new SceneData();
            sceneData.name = name;
            sceneData.invObjects = new List<InventoryData>();
            data.sceneList.Add(sceneData);           
        }
        return data;
    }   
}
