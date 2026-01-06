using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBackpackState : State
{
    // Called once on State enter
    public override void Enter()
    {

    }
    // Called once per frame until the State is switched
    public override void Run()
    {

    }
    // Called once on State switch
    public override void Exit()
    {

    }

    //Constructor
    public OpenBackpackState(FiniteStateMachine _fsm, ObjectContext _context, string _name = "OpenBackpackState", int _priority = 3, bool _locked = false, bool _forceOverride = false)
    {
        fsm = _fsm;
        name = _name;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
