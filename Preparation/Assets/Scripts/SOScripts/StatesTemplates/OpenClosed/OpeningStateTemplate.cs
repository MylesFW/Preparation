using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;


[CreateAssetMenu(fileName = "State", menuName = "State/Opening State")]
public class OpeningStateTemplate : ScriptableObject
{
    public int priority;
    public bool locked;
    public bool forceOverride;

    public SpriteCollection spriteSheet;
    public float animationSpeed;
    public bool playAnimation;
    public bool loopAnim;
    public bool reverseAnim;

    public AudioClip openingSound;
    public AudioClip closedSound;

}
