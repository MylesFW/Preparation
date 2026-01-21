using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "State/Movement State", order = 3)]
public class MoveStateTemplate: ScriptableObject
{
    [Header("Base Attibutes")]
    public new string name;

    public int priority;

    public bool forceOverride;

    public bool locked;
    
    [Header("Movement Attributes")]           
    public float walkSpeed;

    public bool playAnimation;

    public float animationSpeed;

    public DirectionalSpriteCollection directionSpriteSheet;
    
}
