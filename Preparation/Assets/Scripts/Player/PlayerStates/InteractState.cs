using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : State
{
    private float interactTimer;
    private BaseInteractable interactable;
    // Called once on State enter
    public override void Enter()
    {
             
    }
    // Called once per frame until the State is switched
    public override void Run()
    {
        interactTimer -= 0.2f * Time.deltaTime;
        //Debug.Log(interactTimer.ToString());

        if (interactTimer <= 0f)
        {
            interactable.ExecuteInteraction(self);
            self.interactManager.FinishedInteract();    
        }        
    }  
    // Called once on State switch
    public override void Exit()
    {
       
    }

    //Constructor
    public InteractState(FiniteStateMachine _fsm, ObjectContext _context, float _timer,BaseInteractable interactablecompenent, string _name = "InteractState", int _priority = 4, bool _locked = false, bool _forceOverride = false)
    {
        interactable = interactablecompenent;
        interactTimer = _timer;
        fsm = _fsm;
        name = _name;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
