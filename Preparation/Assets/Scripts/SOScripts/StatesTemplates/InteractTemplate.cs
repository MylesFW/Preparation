using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractMode
{
    HoldToComplete,
    Instant
}

[CreateAssetMenu(fileName = "State", menuName = "State/Interact State", order = 3)]
public class InteractTemplate : ScriptableObject
{
    [Header("Base Attibutes")]
    public string stateName;
    public int priority;
    public bool forceOverride;
    public bool locked;

    [Header("Interactable Attributes")]
    public InteractMode interactMode;
    public bool playAnimation;
    public bool loop;
    public bool indefinite;
    public float animationSpeed;
    public float interactTimer;
    public DirectionalSpriteCollection directionSpriteSheet;

} 