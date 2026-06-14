using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory, ISavable
{
    // Player's inventory
    // Brennan RF(1): 2/21/26
    // (DONE) to change: Off load base methods to new parent class
    // to review: Subcribing, publishing and handling inventory events.
    // also review inventory UI implementation, consider moving to new class if it becomes too complex
    // -- off load inputs (maybe events?) what should player inventory do/own?
    // refactor to expose less prett much everything

    // Fields
    public bool showInvUI; // eventually delete
    public bool uiIndexed;

    private PlayerController playerController;

    public ListItemStackEventChannelSO playerLooted; // channels not actions/delagets
    public ListEventChannelSO playerStored;
    public ListEventChannelSO storeIndexList;

    // Methods
    public void OnEnable()
    {
        onSimulationHour.onEventRaised += ItemStackDecay;
        playerLooted.onEventRaised += DropOrKeep;
        InventoryChanged += OnInventoryChanged;
    }

    public void OnDisable()
    {
        playerLooted.onEventRaised -= DropOrKeep;
        InventoryChanged -= OnInventoryChanged;
    }

    private void Start()
    {
        items = new List<ItemStack>();        
        playerController = GetComponent<PlayerController>(); // refactor to player input events
        listIndex = 0;
        showInvUI = false;
        sortType = SortType.Alphabetical;
        sortFilter = SortFilter.All;
        MaxCapacity = 100 * 100;
        CalculateCapacity();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void OnGUI()
    {
        DrawButtons(150, 340, showInvUI);
        DrawInvPlayer(150, 340, showInvUI, uiIndexed);
    }

    public void DropOrKeep(List<ItemStack> newItems)
    {        
        TryAddItemList(newItems, items);    
        playerController.playerContext.interactManager.FinishInteract();
    }
    private void ProcessInputs()// refactor to player input events. Keep hidden during refactor
    {
        // Most of this update method is just player inputs for simple UI testing,
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
        listIndex = Mathf.Clamp(listIndex, 0, items.Count - 1); // refactor to property?
    }

    GameData ISavable.SaveInstance(GameData data)
    {
        data.playerData.items.Clear();
        foreach(ItemStack itemStack in items)
        {
            var itemData = new ItemData(itemStack.item.ItemName, itemStack.StackAmount, itemStack.condition);
            data.playerData.items.Add(itemData);
        }
        return data;
    }

    void ISavable.LoadInstance(GameData data)
    {
        items.Clear();
        foreach(ItemData itemData in data.playerData.items)
        {
            //search for the itemTemplateSO with matching name
            var itemTemplateSO = ItemController.instance.itemDatabase[itemData.name];
            
            if (itemTemplateSO is FoodItemDataSO)
            {
                FoodItemDataSO foodItemDataSO = itemTemplateSO as FoodItemDataSO;
                FoodItem item = new FoodItem(foodItemDataSO, playerController.playerContext);
                items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is ToolItemDataSO)
            {
                ToolItemDataSO toolItemDataSO = itemTemplateSO as ToolItemDataSO;
                ToolItem item = new ToolItem(toolItemDataSO, playerController.playerContext);
                items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is FirstAidItemDataSO)
            {
                FirstAidItemDataSO firstAidItemDataSO = itemTemplateSO as FirstAidItemDataSO;
                FirstAidItem item = new FirstAidItem(firstAidItemDataSO, playerController.playerContext);
                items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is ClothesItemDataSO)
            {
                ClothesItemDataSO clothesItemDataSO = itemTemplateSO as ClothesItemDataSO;
                ClothesItem item = new ClothesItem(clothesItemDataSO, playerController.playerContext);
                items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            if (itemTemplateSO is MaterialItemDataSO)
            {
                MaterialItemDataSO materialItemDataSO = itemTemplateSO as MaterialItemDataSO;
                MaterialItem item = new MaterialItem(materialItemDataSO, playerController.playerContext);
                items.Add(new ItemStack(item, itemData.amount, itemData.condition));
            }
            OnInventoryChanged();
        }
    }

    void ISavable.NewGame()
    {
        
    }
    // Offload to UI systems
}

