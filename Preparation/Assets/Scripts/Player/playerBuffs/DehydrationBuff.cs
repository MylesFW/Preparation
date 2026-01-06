using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class DehydrationBuff : AfflictionBuff
{
    public override void Enter()
    {
        context.playerHealth.drainHealth = true;
        context.playerHealth.currentDrainRate += 0.000001f;
    }
    public override void Run()
    {

    }
    public override void Exit()
    {
        context.playerHealth.currentDrainRate -= 0.000001f;
    }

    public DehydrationBuff
        (
        SimTime _simTime,
        ObjectContext _context,
        string _name = "Dehydration",
        string _description = "Your health will decline rapidly unless you drink water soon",
        string _cause = "Caused by not drinking enough water",
        string _remedy = "Drink clean water to survive",
        bool _indefinite = true,
        bool _stackable = false,
        int _maxDuration = 1
        )
    {
        name = _name;
        description = _description;
        cause = _cause;
        remedy = _remedy;
        indefinite = _indefinite;
        stackable = _stackable;
        maxDuration = _maxDuration;
        duration = _maxDuration;
        context = _context;
        simTime = _simTime;
    }
}


