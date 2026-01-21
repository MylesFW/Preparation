using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "State/Open State")]
public class OpenStateTemplate : ScriptableObject
{
    public int priority;
    public bool locked;
    public bool forceOverride;

    public SpriteCollection spriteSheet;
    public float animationSpeed;
    public bool playAnimation;
}
