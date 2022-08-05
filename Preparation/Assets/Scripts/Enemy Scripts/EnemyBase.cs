using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Enemy", menuName= "Enemy/Create new enemy")]

public class EnemyBase : ScriptableObject
{
    [SerializeField] string name;    

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;

    [SerializeField] Sprite backSprite;

    //base enemy stats
    [SerializeField] int maxHp;
    [SerializeField] int maxAp;
    [SerializeField] int lightAttack;
    [SerializeField] int lightDefense;
    [SerializeField] int heavyAttack;
    [SerializeField] int heavyDefense;

    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }
    public Sprite BackSprite
    {
        get { return backSprite; }
    }
    public int MaxHp
    {
        get { return maxHp; }
    }
    public int MaxAp
    {
        get { return maxAp; }
    }
    public int LightAttack
    {
        get { return lightAttack; }
    }
    public int LightDefense
    {
        get { return lightDefense; }
    }
    public int HeavyAttack
    {
        get { return heavyAttack; }
    }
    public int HeavyDefense
    {
        get { return heavyDefense; }
    }
}
