using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : State
{
    private float interactTimer;
    private void RaiseInteractCompleted()
    {
      
    }

    // Called once on State enter
    public override void Enter()
    {
            
    }
    // Called once per frame until the State is switched
    public override void Run()
    {
        if (self.playerInput.interact == true)
        {
            interactTimer -= Time.deltaTime;
            if (interactTimer <= 0)
            {
                fsm.EnqueueState(new IdleState(fsm, self, "IdleState", 5, false, true));
                self.playerController.onInteractCompleteFlag = true;
            }
        } 
        else if (self.playerInput.interactUp == true)
        { 
            fsm.EnqueueState(new IdleState(fsm, self, "IdleState", 5, false, true)); 
        }
    }  
    // Called once on State switch
    public override void Exit()
    {

    }

    //Constructor
    public InteractState(FiniteStateMachine _fsm, ObjectContext _context, float _timer, string _name = "InteractState", int _priority = 4, bool _locked = true, bool _forceOverride = false)
    {
        interactTimer = _timer;
        fsm = _fsm;
        name = _name;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
