using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Collection", menuName = "SpriteCollection/New Sprite Dataset", order = 2)]
public class SpriteDataset : ScriptableObject
{
    public new string name;

    public List<List<SpriteCollection>> Sprite;
}
