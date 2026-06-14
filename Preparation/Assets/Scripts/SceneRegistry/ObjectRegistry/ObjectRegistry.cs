using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public struct PersistentReference
{
    public string id;
    public GameObject obj;
}

public class ObjectRegistry : Singleton<ObjectRegistry>
{
    public bool enableLogging = true;
    private Dictionary<string, GameObject> registry = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> saveRegistry = new Dictionary<string, GameObject>();
    List<PersistentReference> cachedObjects;

    protected override void Awake()
    {
        base.Awake();
        registry.Clear();
        saveRegistry.Clear();
        cachedObjects = new List<PersistentReference>();
    }
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += ClearRegistry;
    }
    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= ClearRegistry;
    }
    public void Update()
    {
        if (registry.Count == 0)
        {
            Debug.Log("Issue!");
        }
    }

    public void CachePersistent(string key, GameObject obj)
    {
        for (int i = 0; i < cachedObjects.Count; i++)
        {
            if (cachedObjects[i].id == key)
            {
                Debug.LogError("A second cached object has hit the tower");
                return;
            }
        }
        var entry = new PersistentReference();
        entry.obj = obj;
        entry.id = key;
        cachedObjects.Add(entry);
    }
    public void RenewPersistent()
    {
        for (int i = 0; i < cachedObjects.Count; i++)
        {
            var entry = cachedObjects[i];
            Add(entry.id, entry.obj);
        }
    }
    public void Add(string name, GameObject obj)
    {
        if (registry.ContainsKey(name))
        {
            Debug.LogWarning("Object: " + name + " already exists in the object registry. Overwriting.");
            registry.Remove(name);
        }

        registry.Add(name, obj);
        if (obj.GetComponent<ISavable>() != null)
        {
            AddSaveRegistry(name, obj);
        }
        if (enableLogging == true)
        {
            Debug.Log("Object: " + name + " has been ADDED to the scene registry.");
        }

    }
    public void Remove(string name)
    {
        registry.Remove(name);
        saveRegistry.Remove(name);
        if (enableLogging == true)
        {
            Debug.Log("Object: " + name + " has been REMOVED to the scene registry.");
        }
    }
    public GameObject Get(string name)
    {
        return registry[name];
    }

    private void AddSaveRegistry(string name, GameObject obj)
    {
        if (saveRegistry.ContainsKey(name))
        {
            Debug.LogWarning("Object: " + name + " already exists in the save registry. Overwriting.");
            saveRegistry.Remove(name);
        }

        saveRegistry.Add(name, obj);
        if (enableLogging == true)
        {
            Debug.Log("Object: " + name + " has been added to the save registry.");
        }
    }
    public void ClearRegistry(Scene current)
    {
        //registry.Clear();
        //saveRegistry.Clear();
    }
}
