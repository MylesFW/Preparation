using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService
{    
    public SaveSystem saveSystem;
    public void GoToScene(string sceneName)
    {
        SaveSystem.instance.loadSceneData = true;
        SaveSystem.instance.SaveGameJSON();
        SceneManager.LoadScene(sceneName);
    }
    public void JumpToScene(string sceneName)
    {
        SaveSystem.instance.loadSceneData = false;
        SceneManager.LoadScene(sceneName);
    }
    public void JumpToSceneAndLoad(string sceneName)
    {
        SaveSystem.instance.loadSceneData = true;
        SceneManager.LoadScene(sceneName);
    }
}
