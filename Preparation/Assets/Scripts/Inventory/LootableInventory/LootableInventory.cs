using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootableInventory : Inventory
{
    // Brennan 1/17/26
    // Handles the final accessable loot Data available to the player on loot interaction
    // Kiki says meow i miss you !!!
    
    // Declare pieces needed to generate items for player
    public PlayerContext playerContext;
    public PlayerController playerController;
    public InteractTemplate interactTemplate;
    public ListItemStackEventChannelSO playerLooted;
    private StorageContainerController storageContainerController;
    private StorageInventory storableInventory;

    [HideInInspector] public bool wasLooted;
    [HideInInspector] public bool isInteracting;
    [HideInInspector] public bool interactionComplete;

    // Final lists of lootable templates to generate
    public List<FoodItemDataSO> foodItems = new List<FoodItemDataSO>();
    public List<ToolItemDataSO> toolItems = new List<ToolItemDataSO>();
    public List<FirstAidItemDataSO> firstAidItems = new List<FirstAidItemDataSO>();
    public List<ClothesItemDataSO> clothesItems = new List<ClothesItemDataSO>();
    public List<MaterialItemDataSO> materialItems = new List<MaterialItemDataSO>();

    public List<Item> lootablInventory = new List<Item>();

    public float thisInteractTimer = 0.25f;
    public float interactTimer {  get; set; }

    // Privs
    private ItemUtils itemUtils;

    private void Awake()
    {
        playerContext = PlayerController.instance.playerContext;
        interactTimer = thisInteractTimer;
        itemUtils = GetComponent<ItemUtils>();
        storableInventory = GetComponent<StorageInventory>();
        storageContainerController = GetComponent<StorageContainerController>();
        wasLooted = false;
        isInteracting = false;
    }
    
    private void Start()
    {
        //wasLooted = false;
        //isInteracting = false;

        //flag = false;
    }
    public void clearTemplateLists()
    {
        foodItems.Clear();
        toolItems.Clear();
        clothesItems.Clear();
        firstAidItems.Clear();
        materialItems.Clear();
    }

    public void GenerateItemsFromTable()
    {
        // Mass produce Items from templates and context
        itemUtils.ManufactureFoodItemInstances(foodItems, lootablInventory, playerContext);
        itemUtils.ManufactureToolItemInstances(toolItems, lootablInventory, playerContext);
        itemUtils.ManufactureFirstAidItemInstances(firstAidItems, lootablInventory, playerContext);
        itemUtils.ManufactureClothesItemInstances(clothesItems, lootablInventory, playerContext);
        itemUtils.ManufactureMaterialItemInstances(materialItems, lootablInventory, playerContext);
    }

    public void QueueInteract()
    {
        // Trigger open/closing anims and sounds
        storageContainerController.isInteracting = true;
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
            // instantiate winning items to a list
            GenerateItemsFromTable();
            var stacks = new List<ItemStack>(ToItemStackList(lootablInventory));            
            foreach (ItemStack itemStack in stacks)
            {
                int dice = Random.Range(0, 3);
                dice *= 10;

                itemStack.condition = 100;
                if (itemStack.item.isIndefinite == false)
                {
                    itemStack.condition -= dice;
                }
            }
            playerLooted.raiseEvent(stacks);
            clearTemplateLists();
        }
    }
    public void EndInteraction()
    {
        storageContainerController.isInteracting = false;
        storageContainerController.interactionComplete = true;
        storageContainerController.wasLooted = true;
        storableInventory.enabled = true;
        Destroy(this);
    }
    public void AbortInteraction()
    {
        storageContainerController.isInteracting = false;
        storageContainerController.interactionComplete = false;
        storageContainerController.wasLooted = false;
        interactTimer = thisInteractTimer;
    }
}
