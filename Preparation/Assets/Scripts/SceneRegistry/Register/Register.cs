using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    [SerializeField]
    private string keyValue;
    private ObjectRegistry objectRegistry;
    public string KeyValue { get { return keyValue; } }
    public ObjectRegistry ObjectRegistry { get { return objectRegistry; } }

    public bool savable;
    public bool persistent;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        objectRegistry = FindObjectOfType<ObjectRegistry>();
        objectRegistry.Add(keyValue, this.gameObject);
        Debug.Log($"Registered {keyValue} in ObjectRegistry.");
    }
    private void OnDisable()
    {
        if (persistent == false)
        {
            objectRegistry.Remove(keyValue);
        }
    }
}
