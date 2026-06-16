using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLibrary : Singleton<SceneLibrary>, ISavable
{
    public SceneService sceneService = new SceneService();
    public string newGameScene = "SampleScene";
    public List<string> sceneNames = new List<string>();
    public string currentScene;

    protected override void Awake()
    {
        base.Awake();
        sceneNames.Add("Scene_2");
        sceneNames.Add("SampleScene");
        sceneNames.Add("SaveSelect");
        sceneNames.Add("MainMenu");
        sceneNames.Add("DeathScene");
        sceneNames.Add("BrennanTestSceneTwo");
        sceneNames.Add("TestSceneTwo");
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(500, 5, 250, 30),("Current Scene: " + currentScene));
        if (GUI.Button(new Rect(500, 20, 150, 30), "Load Scene 1"))
        {
            sceneService.GoToScene(sceneNames[0]);
        }
        ;
        if (GUI.Button(new Rect(500, 70, 150, 30), "Load Scene 2"))
        {
            sceneService.GoToScene(sceneNames[1]);
        }
    }
    public void OnSceneLoaded(Scene name, LoadSceneMode mode)
    {
        if (name.name == "LoadScene")
        {
            sceneService.JumpToScene("MainMenu");
        }
        currentScene = SceneManager.GetActiveScene().name;
    }

    public GameData SaveInstance(GameData data)
    {
        data.SceneName = currentScene;
        return data;
    }

    public void LoadInstance(GameData data)
    {
        if (data == null) return;
        currentScene = data.SceneName;
    }
    void ISavable.NewGame()
    {
        currentScene = "SampleScene";
    }
}
