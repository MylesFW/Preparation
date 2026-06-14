using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractManager : MonoBehaviour
{
    // Brennan
    // 1/7/26
    // Processes player interact inputs. Enqueues interact state. Communicates with interactable gameobject
    // RF (1): 3/1/26

    public int maxInteractCooldown;
    public InteractTemplate interactTemplate;

    private PlayerController playerController;
    private FiniteStateMachine fsm;
    private Inputs playerInput;
    private PlayerContext playerContext;

    public IInteractable inst_interactable;
    
    private float interactCooldown; // input cooldown preventing retrigger, not the state's lifetime
    private bool releaseReTrigger;
    private bool isInteracting;

    private void Awake()
    {
        isInteracting = false;
        fsm = GetComponent<FiniteStateMachine>();
        playerInput = GetComponent<Inputs>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerContext = playerController.playerContext;
        releaseReTrigger = true;
    }
    // Update is called once per frame
    // TODO: put this all in a method
    void Update()
    {        
        ProcessInputs();
    }

    // Grabs the Interactable reference
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            inst_interactable = interactable;               
        }
    }
    
    // Nullifies the interactable inst
    private void OnTriggerExit2D(Collider2D collider)
    {
        inst_interactable = null;
    }
    
    // Gets called once conditions are met to Interact with an object
    // Conditions are controlled by HandleTriggerInputs
    // Queues start of Interaction logic flow -- this is the start
    public void BeginInteract()
    {
        // Get stored InteractTemplate from the Iinteractable NOT the from inspector
        fsm.EnqueueState(new InteractState(inst_interactable.InteractTemplate, fsm, playerContext, inst_interactable));
    }

    // End of Interaction Control flow, resets bools and queues a forced Idle
    // TODO: Add an event from the interactable object
    public void FinishInteract()
    {
        interactCooldown = maxInteractCooldown;
        fsm.ForceState(new IdleState(playerController.playerForcedIdle, fsm, playerContext));
        playerInput.interactDown = false;
        releaseReTrigger = false;
    }

    // Aborts the current interaction
    // Queues and idle and exits changes the state to cutoff the interaction
    // TODO: Trigger this only when HoldToComplete
    public void AbortInteract()
    {
        fsm.ForceState(new IdleState(playerController.playerForcedIdle, fsm, playerContext));      
        releaseReTrigger = false;
        interactCooldown = 0;
        Debug.Log("Interact Override Lifted, ready to interact");
    }

    // Process inputs (Replaces HandleTriggerInputs
    private void ProcessInputs()
    {
        // Quick out if player is NOT near an interactable object
        if (inst_interactable == null)
        {
            return;
        }

        if (playerInput.interactUp == true)
        {
            releaseReTrigger = true;
        }

        if (inst_interactable.Locked == true)
        {
            if (CheckForKey() == false)
            {
                return;
            }
        }

        // Instant Input implementation by interactMode    
        if (inst_interactable.InteractMode == InteractMode.Instant)
        {
            if (playerInput.interactDown && releaseReTrigger == true)
            {
                if (fsm.CurrentState is InteractState) 
                { 
                    FinishInteract(); 
                }
                else 
                {
                    if (TryUnlock() == true)
                    {
                        BeginInteract();
                    }
                }
            }
            if (playerInput.exitPressed)
            {
                if (fsm.CurrentState is InteractState) { FinishInteract(); }
            }
            if (playerInput.inventoryPressed)
            {
                if (fsm.CurrentState is InteractState) { FinishInteract(); }
            }
        }
        // Hold to complete -- Input implementation by interactMode  
        else if (inst_interactable.InteractMode == InteractMode.HoldToComplete)
        {
            if (playerInput.interactUp)
            {
                if (fsm.CurrentState is InteractState) { FinishInteract(); }
            }
            if (playerInput.interact)
            {
                if (fsm.CurrentState is InteractState) { return; }
                else 
                {
                    if (TryUnlock() == true)
                    {
                        BeginInteract();
                    }
                }
            }
        }
    }

    // Checks if the player contains a key for the interactable.
    private bool CheckForKey()
    {
        MaterialItemDataSO key = inst_interactable.Key;
        if (playerContext.inventory.ContainsKey(key) == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    // Attemps to unlock an interact and returns true if successful
    private bool TryUnlock()
    {
        if (inst_interactable.Locked == true)
        {
            if (CheckForKey() == false)
            {
                return false;
            }
            else
            {
                inst_interactable.Locked = false;
                return true;
            }
        }
        else
        {
            return true;
        }
    }
}
