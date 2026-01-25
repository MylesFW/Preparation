using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEditor.UI;
using UnityEngine;

public class StorageContainerController : MonoBehaviour
{
    // Brennan 1/19/26

    public ObjectContext context;
    private FiniteStateMachine fsm;
    private LootableInventory lootableInventory;

    public ClosedStateTemplate closedTemplate;
    public OpeningStateTemplate openingTemplate;

    public bool isInteracting;
    public bool interactionComplete;
    public bool locked;

    public void Awake()
    {
        fsm = GetComponent<FiniteStateMachine>();
        lootableInventory = GetComponent<LootableInventory>();
        context = BuildContext();
    }
    public void Start()
    {
        fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context, locked));
    }

    public void Update()
    {
        if (isInteracting == true)
        {
            fsm.EnqueueState(new OpeningState(openingTemplate, fsm, context));           
        }
        else if (isInteracting == false)
        {
            fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context, locked));
        }

        if (interactionComplete == true)
        {
            fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context, locked));
            interactionComplete = false;
        }
    }

    private StorageContext BuildContext()
    {
        var context = new StorageContext();
        context.transform = transform;
        context.animator2D = GetComponent<Animator2D>();
        context.collider = GetComponent<BoxCollider2D>();
        context.audioSource = GetComponent<AudioSource>();
        context.fsm = fsm;
        context.controller = GetComponent<StorageContainerController>();

        return context;
    }
}

