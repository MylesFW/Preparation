using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // Brennan
    // 1/7/26
    // Processes player interact inputs. Enqueues interact state. Communicates with interactable gameobject

    public int maxInteractCooldown;
    
    private PlayerController playerController;
    private FiniteStateMachine fsm;
    private Inputs playerInput;
    private PlayerContext playerContext;   
    
    private IInteractable inst_interactable;
    
    private float interactCooldown; // input cooldown preventing retrigger, not the state's lifetime
    private bool releaseReTrigger;
    
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

        if (inst_interactable == null)
        {
            return;
        }
        else
        {   
            if (playerInput.interactUp)
            {
                releaseReTrigger = false;
                interactCooldown = maxInteractCooldown;
                fsm.EnqueueState(new IdleState(playerController.playerIdle, fsm, playerContext));

                Debug.Log("Interact Override Lifted, ready to interact");
            }
            else if (playerInput.interact == true && releaseReTrigger == true)
            {
                inst_interactable.QueueInteract(this.gameObject, inst_interactable);
            }
        }
        // Release interact gate if cooldown == O
        if (interactCooldown > 0)
        {
            interactCooldown -= Time.deltaTime;
        }
        else if (interactCooldown <= 0)
        {
            releaseReTrigger = true;     
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            inst_interactable = interactable;        
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        inst_interactable = null;
    }
    public void FinishInteract()
    {
        interactCooldown = maxInteractCooldown;
        fsm.EnqueueState(new IdleState(playerController.playerIdle, fsm, playerContext));
    }
}
