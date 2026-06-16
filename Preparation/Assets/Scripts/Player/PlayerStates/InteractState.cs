using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : State
{
    public PlayerContext self;
    public float maxTimer;
    public bool indefinite;
    
    private float interactTimer;
    private IInteractable interactable;
    private InteractLoadingBar interactLoadingBar;
    private InteractMode interactMode;

    //Constructor
    public InteractState(InteractTemplate template, FiniteStateMachine _fsm, PlayerContext _context, IInteractable _interactable)
    {
        fsm = _fsm;
        name = template.stateName;
        self = _context;
        priority = template.priority;
        locked = template.locked;
        forceOverride = template.forceOverride;
 
        interactable = _interactable;
        maxTimer = _interactable.interactTimer;
        indefinite = template.indefinite;
        interactMode = template.interactMode;
    }

    // Called once on State enter
    public override void Enter()
    {
        // Iinteractable
        interactable.QueueInteract();
        interactTimer = maxTimer;
        // Loading Bar
        interactLoadingBar = self.interactLoadingBar;
        interactLoadingBar.EnableThis(interactTimer);

        // Animator Int
        self.animator2D.playAnimation = false;
        self.animator2D.frameIndex = 0;
    }
    // Called once per frame until the State is switched
    public override void Run()
    {
        // Run Animation
        self.animator2D.playAnimation = false;
        self.animator2D.frameIndex = 0;
        
        // Instant Trigget Mode -- Trigger
        if (interactMode == InteractMode.Instant)
        {
            interactable.Interact(fsm.gameObject);
        }
        // Hold to Complete Mode -- Trigger
        else if (interactMode == InteractMode.HoldToComplete)
        {
            interactTimer -= 0.2f * Time.deltaTime;
            if (interactTimer <= 0f)
            {
                interactable.Interact(fsm.gameObject);
            }
        }  
    }
    // Called once on State switch
    public override void Exit()
    {               
        if (interactMode == InteractMode.Instant)
        {
            interactable.EndInteraction();
        }
        
        // did we get switched due to completing or aborting?
        // This is judged by the interact timers value at time of getting the signal to switch
        if (interactTimer <= 0f && interactMode == InteractMode.HoldToComplete)
        {
            Debug.Log("P-InteractState reached exit with depleted state timer");
            interactable.EndInteraction();
        }
        else if (interactTimer > 0 && interactMode == InteractMode.HoldToComplete)
        {
            Debug.Log("P-InteractState reached exit without depleted state timer");
            interactable.AbortInteraction();
        }
        interactLoadingBar.enabled = false;
    }    
}// Kiki says purrrrrrrrrrrrr
