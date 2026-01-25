using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "State/Interact State", order = 3)]
public class InteractTemplate : ScriptableObject
{
    [Header("Base Attibutes")]
    public new string name;
    public int priority;
    public bool forceOverride;
    public bool locked;

    [Header("Interactable Attributes")]
  
    public bool playAnimation;
    public bool loop;
    public bool indefinite;
    public float animationSpeed;
    public float interactTimer;
    public DirectionalSpriteCollection directionSpriteSheet;

}