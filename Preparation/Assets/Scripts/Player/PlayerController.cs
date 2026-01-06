using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ObjectContext playerContext;
    public SimTime simTime;
    public BuffManager buffManager;
    [HideInInspector] public Action OnInteractComplete;

    public int currentEquiped;
    public bool onInteractCompleteFlag;
    public NewItem[] equiped;

    private FiniteStateMachine fsm;
    private Inputs playerInput;
    private Animator2D animator;  

    private bool crouchToggle;
    private bool inventoryToggle;

    // Methods
    private void IdleCrouchWalk()
    {
        // Decides if the crouched/uncrouched version of idle and walk should be used    
        // Crouch
        
        if (playerInput.crouchPressed && crouchToggle)
        {
            crouchToggle = false;
        }
        else if (playerInput.crouchPressed && !crouchToggle)
        {
            crouchToggle = true;
        }
        
        // Check Crouch/Standing
         
        if (crouchToggle)
        {
            if (playerInput.inputVector != Vector2.zero)
            {
                fsm.EnqueueState(new CrouchWalkState(fsm, playerContext));
            }
            else if (playerInput.inputVector == Vector2.zero)
            { 
                fsm.EnqueueState(new CrouchIdleState(fsm, playerContext));
            }
        }
        else if (!crouchToggle)
        {
            if (playerInput.inputVector != Vector2.zero)
            {
                fsm.EnqueueState(new WalkState(fsm, playerContext));
            }
            else if(playerInput.inputVector == Vector2.zero)
            {
                fsm.EnqueueState(new IdleState(fsm, playerContext));
                
            }
        }
    }
    private void HandleSprint()
    {
        if (playerInput.sprintHold && playerInput.inputVector != Vector2.zero)
        {
            fsm.EnqueueState(new SprintState(fsm, playerContext));
            crouchToggle = false;
        }
    }
    private void HandleInteract(float _timer)
    {
        if (playerInput.interactDown == true)
        {
            fsm.EnqueueState(new InteractState(fsm, playerContext, _timer));
           
            Debug.Log("interact queued");
        }
    }
    private void HandCheckBackpack()
    {
        // Check Inventory Toggle Singleton
        if (playerInput.inventoryPressed && inventoryToggle)
        {
            inventoryToggle = false;
        }
        else if (playerInput.inventoryPressed && !inventoryToggle)
        {
            inventoryToggle = true;
        }

        if (inventoryToggle)
        {
            fsm.EnqueueState(new OpenBackpackState(fsm, playerContext));
        }
    }
    private void CycleEquiped()
    {
        if (playerInput.cycleEquiped)
        {
            currentEquiped++;
            
            if (currentEquiped > 3)
            {
                currentEquiped = 0;
            }
        
            Debug.Log(equiped[currentEquiped].name);
        }       
    } 
    private void UnfulfilledNeed(string _name, Buff _buff)
    {
        if (buffManager.BuffExistsByName(_name) == false)
        {
            buffManager.EnqueueBuff(_buff);
        }
        else
        {
            buffManager.SetBuffIndefinite(_name, true);
        }
    }
    private void OnNeedFulfilled(string _name)
    {
        if (buffManager.BuffExistsByName(_name) == false)
        {
            return;
        }
        else
        {
            buffManager.SetBuffIndefinite(_name, false);
        }
    }
    
    #region Event methods (Listeners)
    private void RespondToFreezing()
    {
        UnfulfilledNeed("Hypothermia", new HypothermiaBuff(simTime, playerContext));
    }
    private void RespondToCold()
    {
        OnNeedFulfilled("Hypothermia");
    }
    private void RespondToStarving()
    {
        UnfulfilledNeed("Starvation", new StarvationBuff(simTime, playerContext));
    }
    private void RespondToHungry()
    {
        OnNeedFulfilled("Starvation");
    }
    private void RespondToDehydrated()
    {
        UnfulfilledNeed("Dehydration", new DehydrationBuff(simTime, playerContext));
    }
    private void RespondToThirsty()
    {
        OnNeedFulfilled("Dehydration");
    }
    private void RespondToExhausted()
    {
        UnfulfilledNeed("Exhausted", new ExhaustedBuff(simTime, playerContext));
    }
    private void RespondToTired()
    {
        OnNeedFulfilled("Exhausted");
    }
    #endregion

    private void Awake()
    {
        playerContext = new ObjectContext
        {
            transform = transform,
            rigidbody = GetComponent<Rigidbody2D>(),
            collider = GetComponent<BoxCollider2D>(),
            animator2D = GetComponent<Animator2D>(),
            audioListener = GetComponent<AudioListener>(),
            audioSource = GetComponent<AudioSource>(),

            playerInput = GetComponent<Inputs>(),
            playerMovement = GetComponent<PlayerMovement>(),
            playerCalories = GetComponent<PlayerCalories>(),
            playerFatigue = GetComponent<PlayerFatigue>(),
            playerHealth = GetComponent<PlayerHealth>(),
            playerStamina = GetComponent<PlayerStamina>(),
            playerTemp = GetComponent<PlayerTemp>(),
            playerThirst = GetComponent<PlayerThirst>(),
            playerBuffManager = GetComponent<BuffManager>(),
            playerController = this,
        };

        fsm = GetComponent<FiniteStateMachine>();
        playerInput = GetComponent<Inputs>();
        animator = GetComponent<Animator2D>();

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        crouchToggle = false;
        inventoryToggle = false;

        equiped = new NewItem[] { new NullItem(), new NullItem(), new NullItem(), new NullItem() };
        currentEquiped = 0;

        // Subscribe Buff events
        playerContext.playerTemp.isFreezing += RespondToFreezing;
        playerContext.playerTemp.isCold += RespondToCold;

        playerContext.playerCalories.isStarving += RespondToStarving;
        playerContext.playerCalories.isHungry += RespondToHungry;

        playerContext.playerFatigue.isTired += RespondToTired;
        playerContext.playerFatigue.isSleepDeprived += RespondToExhausted;

        playerContext.playerThirst.isThirsty += RespondToThirsty;
        playerContext.playerThirst.isDehydrated += RespondToDehydrated;
    }

    private void Update()
    {

        IdleCrouchWalk();
        HandCheckBackpack();
        HandleSprint();
        CycleEquiped();
        HandleInteract(0.001f);

        if (onInteractCompleteFlag == true) 
        {
            OnInteractComplete?.Invoke();
            onInteractCompleteFlag = false;
            
        }
    
    }
}
