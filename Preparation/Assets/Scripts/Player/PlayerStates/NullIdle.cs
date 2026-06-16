using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullIdle : State
{
    public ObjectContext self;
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
    public NullIdle(FiniteStateMachine _fsm, ObjectContext _context, int _priority = 0, bool _locked = false, bool _forceOverride = false)
    {
        fsm = _fsm;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
