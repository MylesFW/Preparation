using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : Singleton<SaveSystem>
{
    // Data
    public static GameData data;

    // Json Dependency
    public JsonSerializer jsonSerializer;
    public FileDataService fileDataService;
    public DataService dataService;
    public bool loadSceneData; 

    private ObjectRegistry objectRegistry;

    public int saveSlot;
    public bool initNewGame; // Tells the selectSave screen if it should be starting new game or loading and existing one.
    protected override void Awake()
    {
        base.Awake();

        // Json
        jsonSerializer = new JsonSerializer();
        fileDataService = new FileDataService();

        // Data
        GameDataLoader dataLoader = new GameDataLoader();
        data = dataLoader.InitGameData();
        loadSceneData = false;
        saveSlot = 0;
    }
    public void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start()
    {
        objectRegistry = FindObjectOfType<ObjectRegistry>();
    }
    public void NewGame(int slot)
    {
        //Clear the savefile
        DeleteSave(slot);
        saveSlot = slot;
        GameDataLoader dataLoader = new GameDataLoader();
        data = dataLoader.InitGameData();

        // Loop through Isavable compenents per object in savable reg
        foreach (GameObject obj in objectRegistry.saveRegistry.Values)
        {
            var savableComponents = obj.GetComponents<ISavable>();
            if (savableComponents != null)
            {
                foreach (ISavable inst in savableComponents)
                {
                    inst.NewGame();
                }
            }
        }

        SceneLibrary.instance.sceneService.JumpToScene(SceneLibrary.instance.newGameScene);
    }

    public void SaveGameJSON()
    {
        SceneData sceneData = new SceneData(SceneLibrary.instance.currentScene);
        data.SceneName = sceneData.name;
        data.TryAddScene(sceneData);
        data.GetSceneListIndex(SceneLibrary.instance.currentScene);

        // Loop through Isavable compenents per object in savable reg
        foreach (GameObject obj in objectRegistry.saveRegistry.Values)
        {
            var savableComponents = obj.GetComponents<ISavable>();
            if (savableComponents != null)
            {
                foreach (ISavable inst in savableComponents)
                {
                    data = inst.SaveInstance(data);
                }
            }
        }

        // Serialize Save State
        string json = jsonSerializer.Serialize(data);

        // Save Json to file
        fileDataService.SaveJson(json, saveSlot);
    }   

    public GameData LoadGameJSON() 
    {
        
        GameData loadData = jsonSerializer.Deserialize(fileDataService.LoadJson(saveSlot));
        data = loadData;
        if (data.SceneName == "")
        {
            data.SceneName = SceneLibrary.instance.currentScene;
        }

        //check if we need to transition to another scene
        if (data.SceneName != SceneLibrary.instance.currentScene)
        {
            SceneService sceneService = new SceneService();
            sceneService.JumpToSceneAndLoad(data.SceneName);
        }
        data.GetSceneListIndex(data.SceneName);        
        
        // Return Data if needed elswhere
        return data;
    }
    public GameData LoadSceneJSON()
    {
        GameData loadData = jsonSerializer.Deserialize(fileDataService.LoadJson(saveSlot));
        data = loadData;
        data.GetSceneListIndex(SceneManager.GetActiveScene().name);
        data.SceneName = data.sceneList[data.indexedScene].name;
        if (data.SceneName == "")
        {
            data.SceneName = SceneLibrary.instance.currentScene;
        }
        //objectRegistry.RenewPersistent();
        // Call restore state on savable objects
        foreach (GameObject obj in objectRegistry.saveRegistry.Values)
        {
            var savableComponents = obj.GetComponents<ISavable>();
            if (savableComponents != null)
            {
                foreach (ISavable inst in savableComponents) 
                {
                    inst.LoadInstance(data);
                }
            }
        }

        // Return Data if needed elswhere
        return data;
    }

    public void DeleteSave(int slot)
    {
        fileDataService.ClearSaveFile(slot);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (loadSceneData == false)
        {
            return;
        }
        else
        {
            LoadSceneJSON();
        }
    }
}
