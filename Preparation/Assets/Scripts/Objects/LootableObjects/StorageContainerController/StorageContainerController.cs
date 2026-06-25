using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class StorageContainerController : MonoBehaviour, IInteractable, ISavable
{
    // Brennan 1/19/26
    // Fields
    public string inventoryID;
    public ObjectContext context;
    public ClosedStateTemplate closedTemplate;
    public OpeningStateTemplate openingTemplate;
    
    [HideInInspector]
    public bool isInteracting;
    [HideInInspector]
    public bool interactionComplete;
    
    public bool locked;
    
    [HideInInspector]
    public bool wasLooted;
    public MaterialItemDataSO itemkey;

    private StorageInventory storableInventory;
    private FiniteStateMachine fsm;
    private LootableInventory lootableInventory;

    public PlayerController playerController;
    public InteractTemplate playerLooted;
    public InteractTemplate playerStored;

    // Properties
    public MaterialItemDataSO Key { get; set; }
    public bool Locked { get; set; }
    public float interactTimer { get; set; }
    public InteractTemplate InteractTemplate { get; set; }
    public InteractMode InteractMode { get; set; }

    // Methods
    public void Awake()
    {
        fsm = GetComponent<FiniteStateMachine>();
        lootableInventory = GetComponent<LootableInventory>();
        storableInventory = GetComponent<StorageInventory>();
        context = BuildContext();
        playerController = PlayerController.instance;
        wasLooted = false;
        interactTimer = lootableInventory.interactTimer;
        InteractTemplate = playerLooted;
        Locked = locked;
    }
    public void Start()
    {
        //InteractTemplate = playerLooted;
        //Locked = locked;
        fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context, false));
        //wasLooted = false;
        if (wasLooted)
        {
            interactTimer = storableInventory.InteractTimer;
        }
        else
        {
            interactTimer = lootableInventory.interactTimer;
        }
 
        Key = itemkey;
    }
    public void Update()
    {
        HandleStates();
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
    public void QueueInteract()
    {                       
        if (wasLooted == true) 
        {
            storableInventory.enabled = true;
            storableInventory.QueueInteract(); 
        }
        else if (!wasLooted) { lootableInventory.QueueInteract(); }
    }
    public void Interact(GameObject other)
    {
        if (wasLooted == true) 
        { 
            storableInventory.Interact(other); 
        }
        else if (!wasLooted)
        {
            InteractMode = InteractMode.Instant;
            lootableInventory.Interact(other); 
        }
    }
    public void EndInteraction()
    {
        if (wasLooted == true) 
        {
            storableInventory.EndInteraction();
        }
        else if (!wasLooted) 
        {
            InteractTemplate = playerStored;
            wasLooted = true;
            lootableInventory.EndInteraction();
            interactTimer = storableInventory.InteractTimer;
        }
    }
    public void AbortInteraction() 
    {
        if (wasLooted == false)
        {
            fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context, false));
            lootableInventory.AbortInteraction();
            interactionComplete = false;
            isInteracting = false;
            interactTimer = lootableInventory.interactTimer;
        }
    }
    public void HandleStates()
    {
        if (isInteracting == true)
        {           
            fsm.EnqueueState(new OpeningState(openingTemplate, fsm, context));
        }
        else if (isInteracting == false)
        {
            fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context, false));
        }
        if (interactionComplete == true)
        {
            fsm.EnqueueState(new ClosedState(closedTemplate, fsm, context, false));
            interactionComplete = false;
        }
    }

    GameData ISavable.SaveInstance(GameData data)
    {
        InventoryData invData = new InventoryData(inventoryID, wasLooted, locked);
        invData.HasBeenLooted = wasLooted;
        invData.locked = locked;
        if (storableInventory.CurrentCapacity > 0)
        {
            foreach (ItemStack itemStack in storableInventory.items)
            {
                var itemData = new ItemData(itemStack.item.ItemName, itemStack.StackAmount, itemStack.condition);
                invData.items.Add(itemData);
            }
        }
        data.sceneList[data.indexedScene].TryAddInvData(invData);
        return data;
    }

    void ISavable.LoadInstance(GameData data)
    {
        int match = -1;
        // Loop through Gamdata to find a matching inventoryID and load items
        for (var i = 0; i < data.sceneList[data.indexedScene].invObjects.Count; i++)
        {
            if (data.sceneList[data.indexedScene].invObjects[i].inventoryID == inventoryID)
            {
                match = i;
            }
        }
        if (match == -1)
        {
            return;
        }

        storableInventory.items.Clear();

        // address of matching inventory itemdata list
        // data.sceneList[data.indexedScene].invObjects[match].items;
        wasLooted = data.sceneList[data.indexedScene].invObjects[match].HasBeenLooted;
        locked = data.sceneList[data.indexedScene].invObjects[match].locked;
        Locked = locked;
        if (wasLooted == true)
        {
            InteractTemplate = playerStored;
            lootableInventory.EndInteraction();
            interactTimer = storableInventory.InteractTimer;
            InteractMode = InteractMode.Instant;
        }
        else
        {
            InteractTemplate = playerLooted;
            interactTimer = lootableInventory.interactTimer;
            InteractMode = InteractMode.HoldToComplete;
        }
        if (data.sceneList[data.indexedScene].invObjects[match].items.Count == 0)
        {
            return;
        }

        foreach (ItemData itemData in data.sceneList[data.indexedScene].invObjects[match].items)
        {
            //search for the itemTemplateSO with matching name
            var itemTemplateSO = ItemController.instance.itemDatabase[itemData.name];

            if (itemTemplateSO is FoodItemDataSO)
            {
                FoodItemDataSO foodItemDataSO = itemTemplateSO as FoodItemDataSO;
                FoodItem item = new FoodItem(foodItemDataSO, playerController.playerContext);
                storableInventory.items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is ToolItemDataSO)
            {
                ToolItemDataSO toolItemDataSO = itemTemplateSO as ToolItemDataSO;
                ToolItem item = new ToolItem(toolItemDataSO, playerController.playerContext);
                storableInventory.items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is FirstAidItemDataSO)
            {
                FirstAidItemDataSO firstAidItemDataSO = itemTemplateSO as FirstAidItemDataSO;
                FirstAidItem item = new FirstAidItem(firstAidItemDataSO, playerController.playerContext);
                storableInventory.items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is ClothesItemDataSO)
            {
                ClothesItemDataSO clothesItemDataSO = itemTemplateSO as ClothesItemDataSO;
                ClothesItem item = new ClothesItem(clothesItemDataSO, playerController.playerContext);
                storableInventory.items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is MaterialItemDataSO)
            {
                MaterialItemDataSO materialItemDataSO = itemTemplateSO as MaterialItemDataSO;
                MaterialItem item = new MaterialItem(materialItemDataSO, playerController.playerContext);
                storableInventory.items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
        }
        storableInventory.OnInventoryChanged();
    }

    void ISavable.NewGame()
    {
        wasLooted = false;
        interactTimer = lootableInventory.interactTimer;
        InteractTemplate = playerLooted;
    }
}

