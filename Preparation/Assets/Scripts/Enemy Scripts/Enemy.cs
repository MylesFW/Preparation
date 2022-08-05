using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    EnemyBase _base;
    int level;

    public Enemy(EnemyBase eBase, int eLevel)
    {
        _base = eBase;
        level = eLevel;
    }
    public int MaxHp
    {
        get { return Mathf.FloorToInt((_base.MaxHp * level) / 100f) + 5; }
    }
    public int MaxAp
    {
        get { return Mathf.FloorToInt((_base.MaxAp * level) / 100f) + 5; }
    }
    public int LightAttack
    {
        get { return Mathf.FloorToInt((_base.LightAttack * level) / 100f) + 5; }
    }
    public int HeavyAttack
    {
        get { return Mathf.FloorToInt((_base.HeavyAttack * level) / 100f) + 10; }
    }
    public int LightDefense
    {
        get { return Mathf.FloorToInt((_base.LightDefense * level) / 100f) + 5; }
    }
    public int HeavyDefense
    {
        get { return Mathf.FloorToInt((_base.HeavyDefense * level) / 100f) + 10; }
    }
}

