using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class HypothermiaBuff : AfflictionBuff
{
    public override void Enter()
    {
        context.playerHealth.drainHealth = true;
        context.playerHealth.currentDrainRate += 0.00005f;
    }
    public override void Run()
    {
        
    }
    public override void Exit()
    {
        context.playerHealth.currentDrainRate -= 0.00005f;
    }

    public HypothermiaBuff
        (
        SimTime _simTime, 
        ObjectContext _context,
        string _name = "Hypothermia",
        string _description = "You will freeze to death unless you find somewhere warm, soon",
        string _cause = "Caused by exposure to freezing temperatures",
        string _remedy = "Stay above freezing for 24 hours to alleviate",
        bool _indefinite = true,
        bool _stackable = false,
        int _maxDuration = 1_440       
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


