using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
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
    public AttackState(FiniteStateMachine _fsm, ObjectContext _context, int _priority = 3, bool _locked = false, bool _forceOverride = false)
    {
        fsm = _fsm;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
