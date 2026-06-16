using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string SceneName;
    public PlayerData playerData;
    public SimData simData;
    public WeatherData weatherData;
    public List<SceneData> sceneList;
    public int indexedScene;

    public GameData()
    {
        sceneList = new List<SceneData>();
    }
    public void TryAddScene(SceneData data)
    {
        string inputDataName = data.name;
        if (sceneList.Count == 0) { sceneList.Add(data); return; }
        for(int i = 0; i < sceneList.Count; i++) 
        {
            if (sceneList[i].name == inputDataName)
            {
                sceneList[i] = data;
                return;
            }
            else if (i == sceneList.Count - 1 && sceneList[i].name != inputDataName) 
            {
                sceneList.Add(data);
                return;
            }
        }
    }
    public int GetSceneListIndex(string sceneName)
    {
        int index = 0;

        for (int i = 0; i < sceneList.Count; i++)
        {
            if (sceneList[i].name == sceneName)
            {
                index = i;
                indexedScene = i;
                break;
            }
        }
        return index;
    }
}
