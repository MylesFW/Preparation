using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Templates", menuName = "ItemTemplates/FirstAidItem")]
public class FirstAidItemTemplate : ScriptableObject
{
    // Base Items

    public Sprite sprite;

    public Sprite worldSprite;

    public string itemName;

    public string description;

    [Range(0, 200)]
    public int dropRate;

    public float decayRate;

    public float stackWeight;

    public bool stackable;

    public bool indefiniteShelfLife;
}
