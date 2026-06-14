using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGui : MonoBehaviour
{
    public void Awake()
    {
        SaveSystem.instance.saveSlot = 0;
    }
    public void OnGUI()
    {
        int x = Screen.width / 2 - 75; // Center the buttons horizontally
        int y = Screen.height / 2 - 250; // Center the buttons vertically
        
        if (GUI.Button(new Rect(x, y - 50, 150, 30), "New Game"))
        {
            // Initiate new game here.
            SceneService sceneService = new SceneService();
            SaveSystem.instance.initNewGame = true;
            sceneService.JumpToScene("SaveSelect");
        }
        ;
        if (GUI.Button(new Rect(x, y, 150, 30), "Load Game"))
        {
            SceneService sceneService = new SceneService();
            SaveSystem.instance.initNewGame = false;
            sceneService.JumpToScene("SaveSelect");
        }
        ;
    }
}
