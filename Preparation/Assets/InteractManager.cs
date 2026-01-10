using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // Brennan
    // 1/7/26

    private PlayerController playerController;
    private FiniteStateMachine fsm;
    private Inputs playerInput;
    private BaseInteractable interactable;
    private ObjectContext playerContext;

    private float interactTimer;
    public float interactCooldown;
    
    private bool nearInteractable;
    private bool releaseReTrigger;

    public void FinishedInteract()
    {
        interactCooldown = 1;
        fsm.EnqueueState(new IdleState(fsm, playerContext, "IdleState", 0, false, true));
    }

    private void HandleInteract(float _timer, BaseInteractable _interactable)
    {
        
        if (playerInput.interactDown == true)
        {
            fsm.EnqueueState(new InteractState(fsm, playerContext, _timer, _interactable));
        }
        else if (playerInput.interactUp == true)
        {
            fsm.EnqueueState(new IdleState(fsm, playerContext, "IdleState", 5, false, false));
        }    
    }

    private void Awake()
    {
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
    void Update()
    {
        if (playerInput.interactUp)
        {
            releaseReTrigger = true;
            
            fsm.EnqueueState(new IdleState(fsm, playerContext, "IdleState", 0, false, true));
            
            Debug.Log("Interact Override Lifted, ready to interact");
        }

        if (nearInteractable == true && playerInput.interact)
        {
            fsm.EnqueueState(new InteractState(fsm, playerContext, interactTimer, interactable));
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("Collided");

        GameObject interactableObject = collider.gameObject;
        interactable = interactableObject.GetComponent<BaseInteractable>();

        if (interactable != null)
        {
            nearInteractable = true;
            interactTimer = interactable.interactTimer;
        }
        else
        {
            nearInteractable = false;
        }
    }
}
