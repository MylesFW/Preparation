using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableInventory : BaseInteractable, IInteractable
{
    // Brennan
    //1/24/26 stores items and shorterm UI events for storable containers

    public PlayerController playerController;
    public ObjectContext playerContext;
    public Inventory playerInventory;
    public FiniteStateMachine fsm;
    public StorageContainerController storageContainerController;

    public InteractTemplate interactTemplate;

    [HideInInspector] public bool isInteracting;
    [HideInInspector] public bool interactionComplete;
    [HideInInspector] public bool indexed;
    [HideInInspector] public bool showInvUI;

    public int listIndex;

    public List<Item> inventoryList;

    public void Awake()
    {
        this.enabled = false;
        storageContainerController = GetComponent<StorageContainerController>();
    }

    private void Start()
    {
        interactTimer = 0.1f;
        storageContainerController.isInteracting = false;
        showInvUI = false;

        inventoryList = new List<Item>();
    }
    private void OnGUI()
    {
        if (showInvUI == true)
        {
            // origins
            int _x = 1200;
            int _y = 340;

            GUI.Label(new Rect(_x, _y, 250, 20), "Item");
            GUI.Label(new Rect(_x + 80, _y, 250, 20), "Qty");
            GUI.Label(new Rect(_x + 140, _y, 250, 20), "Weight");
            GUI.Label(new Rect(_x + 190, _y, 250, 20), "Condition");

            if (GUI.Button(new Rect(_x - 10, _y - 40, 125, 40), "Consume/Use"))
                //UseConsumeItem(listIndex);

            GUI.Button(new Rect(_x + 130, _y - 40, 125, 40), "Drop Item");

            if (inventoryList.Count != 0)
            {
                int heightDisplacement = 20;
                for (int i = 0; i < inventoryList.Count; i++)
                {
                    int offset = 0;
                    if (i == listIndex)
                    {
                        offset = 10;
                    }

                    var guiItemIndex = inventoryList[i];
                    GUI.Label(new Rect(_x - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.name);
                    GUI.Label(new Rect(_x + 80 - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.currentStackAmount.ToString() + "x");
                    GUI.Label(new Rect(_x + 140 - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.currentStackWeight.ToString() + " kg");
                    GUI.Label(new Rect(_x + 190 - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.condition.ToString() + " %");
                }
            }
        }
    }
    public void QueueInteract(GameObject other, IInteractable self) 
    {
        storageContainerController.isInteracting = true;
        FiniteStateMachine fsm = other.GetComponent<FiniteStateMachine>();
        PlayerController controller = other.GetComponent<PlayerController>();
        fsm.EnqueueState(new InteractState(interactTemplate, fsm, controller.playerContext, self));
    }   
    public void Interact(GameObject other) 
    {
        showInvUI = true;
    }
    public void EndInteraction() 
    {
        showInvUI = true;
        storageContainerController.isInteracting = true;
        interactionComplete = true;
    }













}
