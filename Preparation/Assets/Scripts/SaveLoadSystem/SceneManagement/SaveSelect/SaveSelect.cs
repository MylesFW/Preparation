using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct SaveSlot
{
    public int slot;
    public bool occupied;
    
    public SaveSlot(int slot, bool isOccupied = false)
    {
        this.slot = slot;
        this.occupied = isOccupied;
    }
}

public class SaveSelect : MonoBehaviour
{
    SaveSlot slot1 = new SaveSlot(1);
    SaveSlot slot2 = new SaveSlot(2);
    SaveSlot slot3 = new SaveSlot(3);
    
    private SaveSlot[] savesSlots = new SaveSlot[3];

    bool isNewGame;
    
    private void Awake()
    {
        CheckSaves();
        savesSlots[0] = slot1;
        savesSlots[1] = slot2;
        savesSlots[2] = slot3;
    }
    
    public void SelectSaveButton(int saveNumber)
    {
        SaveSlot saveSlot = new SaveSlot(1);
        saveSlot.slot = savesSlots[saveNumber - 1].slot;
        saveSlot.occupied = savesSlots[saveNumber - 1].occupied;

        if (isNewGame == false)
        {
            if (saveSlot.occupied == true)
            {
                SaveSystem.instance.saveSlot = saveSlot.slot;
                SaveSystem.instance.LoadGameJSON();
            }
            else
            {
                Debug.Log("This Save slot is empty.");
            }
        }
        else
        {
            if (saveSlot.occupied == true)
            {
                //... Add popup saying "Overwright existing save?"
                SaveSystem.instance.NewGame(saveSlot.slot);
            }
            else 
            { 
                SaveSystem.instance.NewGame(saveSlot.slot);
            }
        }
    }       

    
    public void BackButton()
    {
        SceneLibrary.instance.sceneService.JumpToScene("MainMenu");
    }
    
    
    private void CheckSaves()
    {
        isNewGame = SaveSystem.instance.initNewGame;
    
        slot1.occupied = SaveSystem.instance.fileDataService.ContainsSave(1);
        slot2.occupied = SaveSystem.instance.fileDataService.ContainsSave(2);
        slot3.occupied = SaveSystem.instance.fileDataService.ContainsSave(3);
    }
}
