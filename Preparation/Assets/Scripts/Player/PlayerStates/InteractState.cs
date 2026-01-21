using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : State
{
    public PlayerContext self;
    private float interactTimer;
    private LootableInventory interactable;

    // Called once on State enter
    public override void Enter()
    {
        interactable.isInteracting = true;
        self.animator2D.playAnimation = false;
        self.animator2D.frameIndex = 0;
    }
    // Called once per frame until the State is switched
    public override void Run()
    {
        interactTimer -= 0.2f * Time.deltaTime;
        //Debug.Log(interactTimer.ToString());
        self.animator2D.playAnimation = false;

        if (interactTimer <= 0f)
        {
            interactable.ExecuteInteraction(self);
            self.interactManager.FinishInteract();
        }        
    }  
    // Called once on State switch
    public override void Exit()
    {
        interactable.isInteracting = false;
        interactable.interactionComplete = false;
    }

    //Constructor
    public InteractState(FiniteStateMachine _fsm, PlayerContext _context, float _timer,LootableInventory interactablecompenent, string _name = "InteractState", int _priority = 4, bool _locked = false, bool _forceOverride = false)
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
