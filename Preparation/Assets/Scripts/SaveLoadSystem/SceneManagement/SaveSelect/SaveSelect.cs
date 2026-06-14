using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSelect : MonoBehaviour
{
    bool slot1_Occupied;
    bool slot2_Occupied;
    bool slot3_Occupied;
    bool isNewGame;
   
    private void Awake()
    {
        CheckSaves();
    }

    public void OnGUI()
    {
        int x = Screen.width / 2 - 75; // Center the buttons horizontally
        int y = Screen.height / 2 - 250; // Center the buttons vertically     

        if (GUI.Button(new Rect(x, y - 50, 150, 30), "Save 1"))
        {
            if (isNewGame == false)
            {
                if (slot1_Occupied == true)
                {
                    SaveSystem.instance.saveSlot = 1;
                    SaveSystem.instance.LoadGameJSON();
                }
                else
                {
                    Debug.Log("This Save slot is empty.");
                }
            }
            else
            {
                SaveSystem.instance.NewGame(1);
            }

        }
        ;
        if (GUI.Button(new Rect(x, y, 150, 30), "Save 2"))
        {
            if (isNewGame == false)
            {
                if (slot1_Occupied == true)
                {
                    SaveSystem.instance.saveSlot = 2;
                    SaveSystem.instance.LoadGameJSON();
                }
                else
                {
                    Debug.Log("This Save slot is empty.");
                }
            }
            else
            {
                SaveSystem.instance.NewGame(2);
            }
        }
        ;
        if (GUI.Button(new Rect(x, y +50, 150, 30), "Save 3"))
        {
            if (isNewGame == false)
            {
                if (slot1_Occupied == true)
                {
                    SaveSystem.instance.saveSlot = 3;
                    SaveSystem.instance.LoadGameJSON();
                }
                else
                {
                    Debug.Log("This Save slot is empty.");
                }
            }
            else
            {
                SaveSystem.instance.NewGame(3);
            }
        }
        ;
    }
    
    private void CheckSaves()
    {
        isNewGame = SaveSystem.instance.initNewGame;
        slot1_Occupied = SaveSystem.instance.fileDataService.ContainsSave(1);
        slot2_Occupied = SaveSystem.instance.fileDataService.ContainsSave(2);
        slot3_Occupied = SaveSystem.instance.fileDataService.ContainsSave(3);
    }
}
