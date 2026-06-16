using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    //Cooldown for continue any key
    int cooldown;
    bool unlockContinue;

    private void Start()
    {
        cooldown = 120;
        unlockContinue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
        if (cooldown <= 0)
        {
            unlockContinue= true;
        }

        if (unlockContinue)
        {
            if (Input.anyKeyDown == true)
            {
                SceneLibrary sceneLibrary = SceneLibrary.instance;
                SaveSystem saveSystem = SaveSystem.instance;

                saveSystem.DeleteSave(saveSystem.saveSlot);
                sceneLibrary.sceneService.JumpToScene("MainMenu");
            }
        } 
    }
}
