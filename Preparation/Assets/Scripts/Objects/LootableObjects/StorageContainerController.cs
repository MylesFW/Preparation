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

    public void Awake()
    {
        fsm = GetComponent<FiniteStateMachine>();
        lootableInventory = GetComponent<LootableInventory>();
        context = BuildContext();
    }
    public void Start()
    {
        fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context));
    }

    public void Update()
    {
        if (lootableInventory.isInteracting == true)
        {
            fsm.EnqueueState(new OpeningState(openingTemplate, fsm, context));           
        }
        else if (lootableInventory.isInteracting == false)
        {
            fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context));
        }

        if (lootableInventory.interactionComplete == true)
        {
            fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context));
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

