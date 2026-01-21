using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Collection", menuName ="SpriteCollection/New Directional Collection", order = 2)]
public class DirectionalSpriteCollection : ScriptableObject
{
    // Brennan 1/18/26
    // Store Rerences to sprite strips for directional animations

    [Header("[2-way Horizontal]:")]
    public Sprite[] left;
    public Sprite[] right;

    [Header("[Optional 4-way]:")]
    public Sprite[] up;
    public Sprite[] down;

    [Header("[Optional 8-way]:")]
    public Sprite[] downLeft;
    public Sprite[] downRight;
    public Sprite[] upLeft;
    public Sprite[] upRight;    

}
