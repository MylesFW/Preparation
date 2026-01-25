using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootableInventory : BaseInteractable, IInteractable
{
    // Brennan 1/17/26
    // Handles the final accessable loot Data available to the player on loot interaction
    // Kiki says meow i miss you !!!
    
    // Declare pieces needed to generate items for player
    public PlayerContext playerContext;
    public PlayerController playerController;
    public StorageContainerController storageContainerController;
    public Inventory inventory;
    public StorableInventory storableInventory;
    public InteractTemplate interactTemplate;

    [HideInInspector] public bool wasLooted;
    [HideInInspector] public bool isInteracting;
    [HideInInspector] public bool interactionComplete;

    // Final lists of lootable templates to generate
    public List<FoodItemTemplate> foodItems = new List<FoodItemTemplate>();
    public List<ToolItemTemplate> toolItems = new List<ToolItemTemplate>();
    public List<FirstAidItemTemplate> firstAidItems = new List<FirstAidItemTemplate>();
    public List<ClothesItemTemplate> clothesItems = new List<ClothesItemTemplate>();
    public List<MaterialItemTemplate> materialItems = new List<MaterialItemTemplate>();


    // Privs
    private ItemUtils itemUtils;

    private bool flag;

    private void Awake()
    {
        itemUtils = GetComponent<ItemUtils>();
        storableInventory = GetComponent<StorableInventory>();
        storageContainerController = GetComponent<StorageContainerController>();    
    }
    
    private void Start()
    {
        wasLooted = false;
        isInteracting = false;
        playerContext = playerController.playerContext;
        flag = false;
    }

    public void GenerateItemsFromTable()
    {
        // Mass produce Items from templates and context
        itemUtils.ManufactureFoodItemInstances(foodItems, inventory, playerContext);
        itemUtils.ManufactureToolItemInstances(toolItems, inventory, playerContext);
        itemUtils.ManufactureFirstAidItemInstances(firstAidItems, inventory, playerContext);
        itemUtils.ManufactureClothesItemInstances(clothesItems, inventory, playerContext);
        itemUtils.ManufactureMaterialItemInstances(materialItems, inventory, playerContext);
    }

    public void QueueInteract(GameObject other, IInteractable self)
    {

        float intTimer = interactTimer;
        storageContainerController.isInteracting = true;

        FiniteStateMachine fsm = other.GetComponent<FiniteStateMachine>();
        PlayerController controller = other.GetComponent<PlayerController>();

        fsm.EnqueueState(new InteractState(interactTemplate, fsm, controller.playerContext, self));
    }    
    
    public void Interact(GameObject other)
    {
        // Perform Interaction     
        if (wasLooted == true)
        {
            return;
        }
        else if (wasLooted == false)
        {
            GenerateItemsFromTable();
            foodItems.Clear();
            toolItems.Clear();
            clothesItems.Clear();
            firstAidItems.Clear();
            materialItems.Clear();
        }

        storageContainerController.interactionComplete = true;
    }
    
    public void EndInteraction()
    {
        storageContainerController.isInteracting = false;
        flag = true;
        storableInventory.enabled = true;
        Destroy(this);
    }
}
