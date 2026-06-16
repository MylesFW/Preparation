using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileDataService
{
    string targetSave = "0";
    string path;
    
    public FileDataService()
    {
        CreateDirectory();
        CreateFiles();
    }
    public void CreateDirectory()
    {
        path = string.Concat(Application.persistentDataPath, "/GameData");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    
    public void CreateFiles()
    {
        for (int i = 1; i < 4; i++)
        {
            path = string.Concat(Application.persistentDataPath, $"/GameData/saveslot_{i}.json");
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }
            path = string.Concat(Application.persistentDataPath, $"/GameData/GlobalData.json");
        }
    }
     
    public void SaveJson(string json, int _saveSlot)
    {
        _saveSlot = Mathf.Clamp(_saveSlot, 1, 3);
        targetSave = _saveSlot.ToString();
        path = string.Concat(Application.persistentDataPath, $"/GameData/saveslot_{targetSave}.json");
        System.IO.File.WriteAllText(path, json);
    }
    public string LoadJson(int _saveSlot)
    {
        if (_saveSlot == 0)
        {
            // New Game!
            return null;
        }
        _saveSlot = Mathf.Clamp(_saveSlot, 1, 3);
        targetSave = _saveSlot.ToString();
        path = string.Concat(Application.persistentDataPath, $"/GameData/saveslot_{targetSave}.json");
        string json = System.IO.File.ReadAllText(path);
        return json;
    }

    public bool ContainsSave(int checkSlot)
    {
        checkSlot = Mathf.Clamp(checkSlot, 1, 3);
        path = string.Concat(Application.persistentDataPath, $"/GameData/saveslot_{checkSlot}.json");
        string json = System.IO.File.ReadAllText(path);
        if (json != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ClearSaveFile(int checkSlot)
    {
        checkSlot = Mathf.Clamp(checkSlot, 1, 3);
        path = string.Concat(Application.persistentDataPath, $"/GameData/saveslot_{checkSlot}.json");
        System.IO.File.WriteAllText(path, "");
    }
}
