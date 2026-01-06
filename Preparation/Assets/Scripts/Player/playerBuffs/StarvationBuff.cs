using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class StarvationBuff : AfflictionBuff
{
    public override void Enter()
    {
        context.playerHealth.drainHealth = true;
        context.playerHealth.currentDrainRate += 0.0000001f;
    }
    public override void Run()
    {

    }
    public override void Exit()
    {
        context.playerHealth.currentDrainRate -= 0.0000001f;
    }

    public StarvationBuff
        (
        SimTime _simTime,
        ObjectContext _context,
        string _name = "Starvation",
        string _description = "You will starve to death unless you find something to eat",
        string _cause = "Caused by too much time spent without eating",
        string _remedy = "Maintain a calorie surplus above 0",
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


