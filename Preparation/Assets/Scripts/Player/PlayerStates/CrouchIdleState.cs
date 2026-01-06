using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CrouchIdleState : State
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
    public CrouchIdleState(
        FiniteStateMachine _fsm, 
        ObjectContext _context, 
        string _name = "CrouchIdleState", 
        int _priority = 0, 
        bool _locked = 
        false, 
        bool _forceOverride = false)
    {
        fsm = _fsm;
        name = _name;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
