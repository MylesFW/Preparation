using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : State
{
    public PlayerContext self;
    public float maxTimer;
    public bool indefinite;
    
    private float interactTimer;
    private float animSpeed;
    private bool playAnimation;
    private bool loop;
    private IInteractable interactable;

    //Constructor
    public InteractState(InteractTemplate template, FiniteStateMachine _fsm, PlayerContext _context, IInteractable _interactable)
    {

        fsm = _fsm;
        name = template.name;
        self = _context;
        priority = template.priority;
        locked = template.locked;
        forceOverride = template.forceOverride;
 
        interactable = _interactable;
        maxTimer = template.interactTimer;
        indefinite = template.indefinite;
    }

    // Called once on State enter
    public override void Enter()
    {
        interactTimer = maxTimer;
        self.animator2D.playAnimation = false;
        self.animator2D.frameIndex = 0;
    }
    // Called once per frame until the State is switched
    public override void Run()
    {
        interactTimer -= 0.2f * Time.deltaTime;
        self.animator2D.playAnimation = false;

        if (self.playerInput.exitPressed == true)
        {
            fsm.EnqueueState(new IdleState(self.playerController.playerIdle, fsm, self));
        }

        if (interactTimer <= 0f)
        {
            interactable.Interact(fsm.gameObject);
            self.interactManager.FinishInteract();
        }        
    }
    // Called once on State switch
    public override void Exit()
    {
        interactable.EndInteraction();
    }
}
