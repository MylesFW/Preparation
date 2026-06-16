using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "State", menuName = "State/Closing State")]
public class ClosingStateTemplate : ScriptableObject
{
    public int priority;
    public bool locked;
    public bool forceOverride;

    public SpriteCollection spriteSheet;
    public float animationSpeed;
    public bool playAnimation;

    public bool loopAnim;
    public bool reverseAnim;
}
