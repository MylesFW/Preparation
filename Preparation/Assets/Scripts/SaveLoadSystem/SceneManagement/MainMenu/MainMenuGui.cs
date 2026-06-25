using Newtonsoft.Json.Bson;
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
    public void Start()
    {
        PlayerController.instance.transform.position = new Vector3(-500, -500, 0);
        SpriteRenderer spriteRenderer = PlayerController.instance.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    public void NewGameButton()
    {
        // Initiate new game here.
        SceneService sceneService = new SceneService();
        SaveSystem.instance.initNewGame = true;
        sceneService.JumpToScene("SaveSelect");
    }

    public void LoadGameButton()
    {
        SceneService sceneService = new SceneService();
        SaveSystem.instance.initNewGame = false;
        sceneService.JumpToScene("SaveSelect");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
