using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Enemy", menuName= "Enemy/Create new enemy")]

public class EnemyBase : ScriptableObject
{
    [SerializeField]
    string name;

    [TextArea]
    [SerializeField]
    string description;

    [SerializeField]
    Sprite frontSprite;

    [SerializeField]
    Sprite backSprite;

    //base enemy stats
    [SerializeField] int maxHp;
    [SerializeField] int maxAP;
    [SerializeField] int lightAttack;
    [SerializeField] int lightDefense;
    [SerializeField] int heavyAttack;
    [SerializeField] int heavyDefense;


}
