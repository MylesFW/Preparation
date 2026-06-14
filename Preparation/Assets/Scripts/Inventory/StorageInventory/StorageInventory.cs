using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInventory : Inventory
{
    // Storage Inventory, attached to storable containers. Stores items and
    // short term UI events for storable containers
    // Brennan
    // 1/24/26 stores items and shorterm UI events for storable containers
    // RF (1) 2/22/26

    public InteractTemplate interactTemplate;
    public StorageContainerController storageContainerController;   
    public ListEventChannelSO playerStored;
    public ListEventChannelSO storedItemIndex;   
    [HideInInspector] public bool interactionComplete;
    [HideInInspector] public bool indexed;
    [HideInInspector] public bool showInvUI;
    public float thisInteractTimer;
    public bool uiIndexed;
    [SerializeField] private float storageCapacity;
    public SortFilter currentSortFilter;
    public SortType currentSortType;
    private Register register;
    private ObjectRegistry objectRegistry;
    private GameObject player;
    private PlayerController playerController;
    private PlayerInventory playerInventory;

    public float InteractTimer { get; set; }

    public void Awake()
    {
        playerController = PlayerController.instance;
        this.enabled = false;
        storageContainerController = GetComponent<StorageContainerController>();
        register = GetComponent<Register>();
        InteractTimer = thisInteractTimer;
    }

    private void Start()
    {
        CacheReferences();   
        MaxCapacity = storageCapacity;
        CalculateCapacity();
    }

    public void OnEnable()
    {
        onSimulationHour.onEventRaised += ItemStackDecay;
        InventoryChanged += OnInventoryChanged;
        uiIndexed = false;      
    }
    public void OnDisable()
    {
        InventoryChanged -= OnInventoryChanged;
    }

    // Update is called once per frame
    void Update()
    {
        // React to changes in the player inventory sort filter and sort type
        // Eventually Refactor to events: OnInvetoryFilterChanged and OnInventorySortTypeChanged
        if (playerInventory != null)
        {
            // Filter by type
            if (playerInventory.sortFilter == SortFilter.All)
            {
                sortFilter = playerInventory.sortFilter;
                SyncDisplay();
                ApplySortType(playerInventory.sortType);
                return;
            }
            else if (playerInventory.sortFilter != sortFilter)
            {
                sortFilter = playerInventory.sortFilter;
                ApplyFilterDisplay(playerInventory.sortFilter);
            }
            // Sort by property (weight, condition, etc.)
            if (playerInventory.sortType != sortType)
            {
                sortType = playerInventory.sortType;
                ApplySortType(sortType);
            }
        }      
        ProcessUIInputs();
    }

    private void OnGUI()
    {
        if (showInvUI == true)
        {
            // Actual inventory ui drawing
            //inventoryUtils.DrawInventory(1500, 340, inventoryList);

            // origins
            int _x = 1200;
            int _y = 340;

            GUI.Label(new Rect(_x, _y, 250, 20), "Item");
            GUI.Label(new Rect(_x + 80, _y, 250, 20), "Qty");
            GUI.Label(new Rect(_x + 140, _y, 250, 20), "Weight");
            GUI.Label(new Rect(_x + 190, _y, 250, 20), "Condition");

            if (GUI.Button(new Rect(_x - 10, _y - 40, 125, 40), "Take"))
            {
                StoreItemStack(playerInventory, listIndex);
            }           
            if (GUI.Button(new Rect(_x + 130, _y - 40, 125, 40), "Store"))
            {
                playerInventory.StoreItemStack(this, playerInventory.ListIndex);
            }   
            if (itemsDisplay.Count != 0)
            {
                int heightDisplacement = 20;
                for (int i = 0; i < itemsDisplay.Count; i++)
                {
                    if (playerInventory.uiIndexed == true)
                    {
                        uiIndexed = false;
                    }
                    else if (playerInventory.uiIndexed == false)
                    {
                        uiIndexed = true;
                    }

                    int offset = 0;
                    if (i == listIndex && uiIndexed == true)
                    {
                        offset = 10;
                    }
                    else if (uiIndexed == false)
                    {
                        offset = 0;
                    }

                    var guiItemIndex = itemsDisplay[i];
                    
                    GUI.Label(new Rect(_x - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.Item.ItemName);
                    GUI.Label(new Rect(_x + 80 - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.StackAmount.ToString() + "x");
                    GUI.Label(new Rect(_x + 140 - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.StackWeight.ToString() + " kg");
                    GUI.Label(new Rect(_x + 190 - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.Condition.ToString() + " %");
                }
            }
        }    
    }
    
    // Iinteractable Methods
    public void QueueInteract() 
    {
        SyncDisplay();
        storageContainerController.isInteracting = true;
        storageContainerController.interactionComplete = false;
    }   
    public void Interact(GameObject other) 
    {
        showInvUI = true;
        playerInventory.showInvUI = true;
    }
    public void EndInteraction() 
    {
        showInvUI = false;
        playerInventory.showInvUI = false;
        storageContainerController.isInteracting = false;
        interactionComplete = true;
        this.enabled = false;
    }
    private void ProcessUIInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            listIndex--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            listIndex++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            uiIndexed = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            uiIndexed = false;
        }
        if (playerController.playerContext.playerInput.inventoryPressed)
        {
            if (showInvUI == false)
            {
                showInvUI = true;
            }
            else
            {
                showInvUI = false;
            }
        }
        listIndex = Mathf.Clamp(listIndex, 0, items.Count - 1);
    }
    private void CacheReferences()
    {
        objectRegistry = register.ObjectRegistry;
        player = objectRegistry.Get("BundledGuy");
        playerController = player.GetComponent<PlayerController>();
        playerInventory = player.GetComponent<PlayerInventory>();
    }
}
