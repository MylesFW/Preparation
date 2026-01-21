using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTemplate : ScriptableObject
{
    [Header("Sprite Images")]
    [Tooltip("The sprite Potrait")]
    public Sprite sprite;

    [Header("String Data ")]
    [Tooltip("Name of Item to be displayed in game")]
    public string itemName;

    [Tooltip("Description of Item to be displayed in game")]
    public string cause;

    [Tooltip("Description of Item to be displayed in game")]
    public string remedy;
    
    [Header("Base Attributes")]
    [Tooltip("Non-Stackable: consume partial. Stackable: Cannot decay, cannot consume partial stack")]
    public bool stackable;

    [Tooltip("The rate the Item decays per simulation tick")]
    
    public float decayRate;
    [Tooltip("Decay rate override")]
    public bool indefinite;
}

