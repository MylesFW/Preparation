using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "State/Idle State", order = 3)]
public class IdleStateTemplate : ScriptableObject
{
    [Header("Base Attibutes")]
    public new string name;
    public int priority;
    public bool forceOverride;
    public bool locked;

    [Header("Movement Attributes")]
    public bool playAnimation;
    public bool loop;
    public float animationSpeed;
    public DirectionalSpriteCollection directionSpriteSheet;

}
