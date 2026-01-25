using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    // Brennan
    // 1/5/26
    
    // DayNightCycle Simulation script reference
    // used in performing logic "On GameTick"

    public SimTime simTime;

    // Scriptable Object State Templates

    public IdleStateTemplate playerIdle;
    public IdleStateTemplate playerTrueIdle;
    public IdleStateTemplate playerCrouchIdle;

    public MoveStateTemplate playerWalk;
    public MoveStateTemplate playerCrouchWalk;
    public MoveStateTemplate playerSprint;

    // PCHandles the creation of the playerobject context
    // PC handles a few buff creation/deletion actions
    // Action for completing a interact (Longdark loading circle thing)

    [HideInInspector] public PlayerContext playerContext;
    [HideInInspector] public BuffManager buffManager;
   
    // Equiped Items, index and array

    [HideInInspector] public int currentEquiped;
    [HideInInspector] public Item[] equiped;

    // PC handles a few important state requesting tasks related to player input
    // The inputs script does'nt have any logic really, it justs relays and is processed here
    // Animator 2d helps with "Posture" changes for the FSM's "Motor" changes. Otherwise states handle anims

    private FiniteStateMachine fsm;
    private Inputs playerInput;
    private Animator2D animator;

    // More bools
    private bool inventoryToggle;
    private bool crouchToggle;

    // Methods ============================================
    private void IdleCrouchWalk()
    {    
        if (playerInput.crouchPressed && crouchToggle)
        {
            crouchToggle = false;
        }
        else if (playerInput.crouchPressed && !crouchToggle)
        {
            crouchToggle = true;
        }
        
        if (crouchToggle)
        {
            if (playerInput.inputVector != Vector2.zero)
            {
                fsm.EnqueueState(new PlayerMoveState(playerCrouchWalk, fsm, playerContext));
            }
            else if (playerInput.inputVector == Vector2.zero)
            { 
                fsm.EnqueueState(new IdleState(playerCrouchIdle, fsm, playerContext));
            }
        }
        else if (!crouchToggle)
        {
            if (playerInput.inputVector != Vector2.zero)
            {
                fsm.EnqueueState(new PlayerMoveState(playerWalk, fsm, playerContext));
            }
            else if(playerInput.inputVector == Vector2.zero)
            {
               fsm.EnqueueState(new IdleState(playerIdle, fsm, playerContext));              
            }
        }
    }
    private void HandleSprint()
    {
        if (playerInput.sprintHold && playerInput.inputVector != Vector2.zero)
        {
            fsm.EnqueueState(new PlayerMoveState(playerSprint, fsm, playerContext));
            crouchToggle = false;
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
            //fsm.EnqueueState(new OpenBackpackState(fsm, playerContext));
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
    private void NeedFulfilled(string _name)
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
    
    // Listener Behaviours ================================
    
    #region Event methods (Listeners)
    private void OnFreezing()
    {
        UnfulfilledNeed("Hypothermia", new HypothermiaBuff(simTime, playerContext));
    }
    private void OnCold()
    {
        NeedFulfilled("Hypothermia");
    }
    private void OnStarving()
    {
        UnfulfilledNeed("Starvation", new StarvationBuff(simTime, playerContext));
    }
    private void OnHungry()
    {
        NeedFulfilled("Starvation");
    }
    private void OnDehydrated()
    {
        UnfulfilledNeed("Dehydration", new DehydrationBuff(simTime, playerContext));
    }
    private void OnThirsty()
    {
        NeedFulfilled("Dehydration");
    }
    private void OnExhausted()
    {
        UnfulfilledNeed("Exhausted", new ExhaustedBuff(simTime, playerContext));
    }
    private void OnTired()
    {
        NeedFulfilled("Exhausted");
    }
    #endregion

    private void Awake()
    {
        // Build player context (Important)        
        playerContext = new PlayerContext
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
            interactManager = GetComponent<InteractManager>(),

            inventory = GetComponent<Inventory>(),
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

        equiped = new Item[] { new NullItem(), new NullItem(), new NullItem(), new NullItem() };
        currentEquiped = 0;

        // Subscribe Buff events
        playerContext.playerTemp.isFreezing += OnFreezing;
        playerContext.playerTemp.isCold += OnCold;

        playerContext.playerCalories.isStarving += OnStarving;
        playerContext.playerCalories.isHungry += OnHungry;

        playerContext.playerFatigue.isTired += OnTired;
        playerContext.playerFatigue.isSleepDeprived += OnExhausted;

        playerContext.playerThirst.isThirsty += OnThirsty;
        playerContext.playerThirst.isDehydrated += OnDehydrated;
    }

    private void Update()
    {
        IdleCrouchWalk();
        HandCheckBackpack();
        HandleSprint();
        CycleEquiped();
    }
}
