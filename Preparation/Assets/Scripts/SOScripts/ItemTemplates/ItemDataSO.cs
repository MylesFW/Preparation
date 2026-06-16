using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataSO : ScriptableObject
{
    // Template for First Aid Items, contains all data fields relevant to First Aid items,
    // used to construct First Aid items in game.
    // Brennan RF(1): 2/21/26
    // Brennan RF(2): 3/4/26 -- offload base traits to parent SO

    [Header("Sprite Images")]
    [Tooltip("The sprite Potrait")]
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    [Tooltip("Image for dropped in world Items")]
    private Sprite worldSprite;

    [SerializeField]
    [Tooltip("Optional overlay (paired with portrait sprite)")]
    private Sprite overlaySprite;

    [SerializeField]
    [Header("String Data ")]
    [Tooltip("Name of Item to be displayed in game")]
    private string itemName;

    [SerializeField]
    [Tooltip("Description of Item to be displayed in game")]
    private string description;

    [SerializeField]
    [Header("Probability")]
    [Range(0, 200)]
    [Tooltip("Higher number = more common; It is not a percantage value")]
    private int dropRate;

    [SerializeField]
    [Header("Base Attributes")]
    [Tooltip("The rate the Item decays per simulation tick")]
    private float decayRate;

    [SerializeField]
    [Tooltip("The weight of one stack of this item. Serves as Max Weight for non-stackable items")]
    private float baseWeight;

    [SerializeField]
    [Tooltip("Non-Stackable: consume partial. Stackable: Cannot decay, cannot consume partial stack")]
    private bool isStackable;

    [SerializeField]
    [Tooltip("Decay rate override, no affect on stackable items as they cannot decay")]
    private bool isIndefinite;

    // Properies
    public Sprite Sprite => sprite;
    public Sprite WorldSprite => worldSprite;
    public Sprite OverlaySprite => overlaySprite;
    public string ItemName => itemName;
    public string Description => description;
    public int DropRate => dropRate;
    public float DecayRate => decayRate;
    public float BaseWeight => baseWeight;
    public bool IsStackable => isStackable;
    public bool IsIndefinite => isIndefinite;
}
