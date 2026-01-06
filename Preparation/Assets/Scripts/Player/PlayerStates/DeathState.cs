using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public override void Enter()
    {

    }
    public override void Run()
    {

    }
    public override void Exit()
    {

    }

    //Constructor
    public DeathState(FiniteStateMachine _fsm, ObjectContext _context, string _name = "DeathState", int _priority = 5, bool _locked = false, bool _forceOverride = false)
    {
        name = _name;
        fsm = _fsm;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
