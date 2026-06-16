using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerExample : Singleton<PlayerExample>, ISavable
{
    public int level = 1;
    public int health = 100;

    private void Start()
    {
        Debug.Log("Player created with level " + level + " and health " + health);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        // Example of player taking damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            if (health <= 0)
            {
                SceneLibrary.instance.sceneService.JumpToScene("DeathScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }

    public GameData SaveInstance(GameData data)
    {
        data.playerData.health = health;
        data.playerData.level = level;
        return data;
    }

    public void LoadInstance(GameData  data)
    {
        var playerData = data.playerData;
        if (playerData != null)
        {
            level = playerData.level;
            //health = playerData.health;
            Debug.Log("Player loaded with level " + level + " and health " + health);
        }
        else
        {
            Debug.LogError("Failed to load player data.");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + health);
        
    }
    public void LevelUp()
    {
        level++;
        Debug.Log("Player leveled up! Current level: " + level);
    }
    public void NewGame()
    {
        level = 1;
        health = 100;
    }
}
