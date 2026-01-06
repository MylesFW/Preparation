using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ExhaustedBuff : AfflictionBuff
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

    public ExhaustedBuff
        (
        SimTime _simTime,
        ObjectContext _context,
        string _name = "Exhausted",
        string _description = "You will collapse soon if you don't get some sleep",
        string _cause = "Caused by lack of sleep",
        string _remedy = "Sleep for 8+ hours to regain strength",
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


