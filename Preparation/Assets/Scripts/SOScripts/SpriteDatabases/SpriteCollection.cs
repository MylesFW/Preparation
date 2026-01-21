using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Collection", menuName = "SpriteCollection/New Collection", order = 2)]
public class SpriteCollection : ScriptableObject
{
    public Sprite[] spriteStrips;

}