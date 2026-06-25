using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SortType
{
    Alphabetical,
    Condition,
    Weight
}
public enum SortFilter
{
    Material,
    Clothes,
    FirstAid,
    Tools,
    Food,
    All
}

public abstract class Inventory : MonoBehaviour
{
    /// <summary>
    /// Base class for all inventory types (player, chest, etc.).
    /// Provides common stack operations, display sync helpers and simple UI drawing helpers.
    /// </summary>
    // Brennan 2/21/26

    // Fields
    public SortType sortType;
    public SortFilter sortFilter;
    protected int listIndex;
    public List<ItemStack> items = new List<ItemStack>();
    protected List<ItemStack> itemsDisplay = new List<ItemStack>();
    private float maxCapacity;
    private float currentCapacity;

    //Properties
    public int ListIndex 
    { 
        get => listIndex; 
        set => listIndex = Mathf.Clamp(value, 0, items.Count - 1);
    }
    public float MaxCapacity 
    { 
        get => maxCapacity;
        protected set => maxCapacity = value; 
    }
    public float CurrentCapacity
    {
        get => currentCapacity;
        protected set => currentCapacity = Mathf.Clamp(value, 0, maxCapacity);
    }
    
    // Events
    public VoidEventChannelSO onSimulationHour;
    protected Action InventoryChanged;

    public void StoreItemStack(Inventory targetInv, int index)
    {
        // Currently only ever stores one stack. 
        // Multi stack storage can be added here once needed.
        // Item copied from display list to align with UI selection.
        if (itemsDisplay.Count == 0)
        {
            Debug.Log("No items to store.");
            return;
        }
        ItemStack sourceStack = itemsDisplay[index];
        ItemStack copiedStack = sourceStack.CopyStack();

        if (copiedStack.Item.BaseWeight + targetInv.CurrentCapacity > targetInv.MaxCapacity)
        {
            Debug.Log("Not enough capacity to store item.");
            return;
        }

        targetInv.TryAdd(copiedStack, 1, targetInv.items);
        FindAndTryRemove(sourceStack);
    }

    /// <summary>
    /// Set the currently selected list index safely.
    /// If the inventory is empty the index is set to 0 and the method returns without error.
    /// </summary>
    /// <param name="index">Desired index to set. Will be clamped to a valid range.</param>
    public void SetListIndex(int index)
    {
        if (items.Count == 0)
        {
            listIndex = 0;
            return;
        }
        ListIndex = index;
    }
    
    public bool ContainsKey(MaterialItemDataSO itemData)
    {
        foreach (ItemStack stack in items)
        {
            if (stack.Item.ItemName == itemData.ItemName)
            {
                return true;
            }
        }
        return false;
    }

    public void FindAndTryRemove(ItemStack refStack)
    {
        int matchIndex = -1;

        // Seach for stack match
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Item.ItemName == refStack.Item.ItemName)
            {
                matchIndex = i;
                break;
            }
            if (items[i].Item.ItemName != refStack.Item.ItemName && i == items.Count - 1)
            {
                Debug.Log("No match found to remove.");
                return;
            }
        }

        // remove from matched stack if a match was found.
        if (matchIndex != -1)
        {
            RelayRemoveStack(items[matchIndex], 1, items);
        }
    }
    
    /// <summary>
    /// Attempt to remove a quantity from an item stack inside the provided inventory list.
    /// - If the item is stackable the method searches for a matching stack (by name) and removes the quantity.
    /// - If removal empties the stack it is removed from the inventory list.
    /// After a successful mutation this will invoke <see cref="InventoryChanged"/>.
    /// </summary>
    /// <param name="itemStackToRemove">The stack (prototype) to remove from the inventory. Matching is performed by item name for stackable items.</param>
    /// <param name="quantity">Quantity to remove from the matched stack.</param>
    /// <param name="inv">The target inventory list to perform removal in.</param>
    public void TryRemoveItem(ItemStack itemStackToRemove, float quantity, List<ItemStack> inv) 
    {
        if (itemStackToRemove.Item.IsStackable == true)
        {
            foreach (ItemStack itemStack in inv)
            {
                if (itemStack.Item.ItemName == itemStackToRemove.Item.ItemName) // Stacking occurs with name match
                {
                    RelayRemoveStack(itemStack, quantity, inv);
                    return;
                }
            }
        }        
    }
    
    /// <summary>
    /// Attempt to add a quantity of <paramref name="newItemStack"/> to the inventory list.
    /// - If the item is stackable and a matching stack name exists, the quantity is added to that stack.
    /// - Otherwise a new ItemStack entry is created (via <see cref="AddAsNewItemStack"/>).
    /// Does not invoke <see cref="InventoryChanged"/> itself; caller should invoke if needed.
    /// </summary>
    /// <param name="newItemStack">The ItemStack prototype to add. Its Item identifies the stackable behavior.</param>
    /// <param name="quantity">Amount to add to the inventory (for non-stackable this becomes the stack amount on the new entry).</param>
    /// <param name="inv">The target inventory list to perform the add in.</param>
    public void TryAdd(ItemStack newItemStack, float quantity, List<ItemStack> inv)
    {       
        // early outs (as new stack and empy list)
        // Not stackable, no match needed, add as new ItemStack
        if (newItemStack.Item.IsStackable == false || inv.Count == 0)
        {
            AddAsNewItemStack(newItemStack, quantity, inv);
            InventoryChanged?.Invoke();
            return;
        }
        else if (newItemStack.Item.IsStackable == true)
        {
            // Seach for Item, add to stack if match found
            // If now match in target inv, add as new Item Stack
            for (int i = 0; i < inv.Count; i++)
            {
                if (inv[i].Item.ItemName == newItemStack.Item.ItemName) // Stacking occurs with name match
                {
                    inv[i].AddToStack(quantity);
                    break;
                }
                else if (inv[i].Item.ItemName != newItemStack.Item.ItemName && i == inv.Count - 1)
                {
                    AddAsNewItemStack(newItemStack, quantity, inv);
                    break;
                }
            }
        }
        InventoryChanged?.Invoke();
    }
    
    /// <summary>
    /// Add multiple item stacks from <paramref name="itemList"/> into the provided inventory.
    /// Each entry in the incoming list is added via <see cref="TryAdd"/>.
    /// Invokes <see cref="InventoryChanged"/> after processing the whole list.
    /// </summary>
    /// <param name="itemList">List of ItemStacks to add (typically prototypes converted from raw Item lists).</param>
    /// <param name="inv">Target inventory list to receive the items.</param>
    public void TryAddItemList(List<ItemStack> itemList, List<ItemStack> inv)
    {
        foreach (ItemStack itemstack in itemList)
        {
            TryAdd(itemstack, 1, inv);
        }
    }
    
    /// <summary>
    /// Clear all ItemStacks from the provided inventory list.
    /// </summary>
    /// <param name="inv">The inventory list to clear.</param>
    public void Clear(List<ItemStack> inv) 
    {
        inv.Clear();
    }

    /// <summary>
    /// Copy the internal items list into the display list.
    /// This performs a shallow copy of ItemStack references and is intended for quick UI testing.
    /// Use <see cref="SyncDisplay"/> to ensure the UI-backed list instance is updated instead of replaced.
    /// </summary>
    public void SyncDisplay()
    {
        itemsDisplay.Clear();
        foreach (ItemStack itemStack in items)
        {
            itemsDisplay.Add(itemStack);
        }
    }
    
    protected float CalculateCapacity()
    {
        currentCapacity = 0;
        foreach (ItemStack itemStack in items)
        {
            currentCapacity += itemStack.StackWeight;
        }
        return currentCapacity; //optional return if needed
    }

    /// <summary>
    /// Remove <paramref name="quantity"/> from <paramref name="itemStack"/> inside <paramref name="inv"/>.
    /// If the stack is depleted after removal the stack instance is removed from the inventory list.
    /// </summary>
    /// <param name="itemStack">The ItemStack instance to remove quantity from (operates on the actual instance inside <paramref name="inv"/>).</param>
    /// <param name="quantity">Amount to remove from the stack.</param>
    /// <param name="inv">The inventory list that contains <paramref name="itemStack"/>.</param>
    protected void RelayRemoveStack(ItemStack itemStack, float quantity, List<ItemStack> inv)
    {
        var isStackDepleted = itemStack.RemoveFromStack(quantity);
        if (isStackDepleted == true) // stack == 0 after removal
        {
            inv.Remove(itemStack);
            InventoryChanged?.Invoke();
        }
    }
    
    /// <summary>
    /// Add a new ItemStack entry to <paramref name="inv"/> using <paramref name="newItemStack"/> as a prototype.
    /// The newly added stack will contain <paramref name="quantity"/> as its initial amount.
    /// </summary>
    /// <param name="newItemStack">Prototype ItemStack describing the Item to add.</param>
    /// <param name="quantity">Initial stack amount for the new entry.</param>
    /// <param name="inv">Target inventory list to add the new stack to.</param>
    protected void AddAsNewItemStack(ItemStack newItemStack, float quantity, List<ItemStack> inv)
    {
        inv.Add(new ItemStack(newItemStack.Item, quantity, newItemStack.condition));       
    }

    /// <summary>
    /// Convert a simple List&lt;Item&gt; to a List&lt;ItemStack&gt; where each Item becomes a stack of amount 1.
    /// Useful when integrating older parts of the codebase that still use raw Item lists.
    /// </summary>
    /// <param name="itemList">Source list of Item instances.</param>
    /// <returns>A new List&lt;ItemStack&gt; where each element wraps one Item with stack amount 1.</returns>
    protected List<ItemStack> ToItemStackList(List<Item> itemList)
    {
        List<ItemStack> inv = new List<ItemStack>();
        foreach (Item item in itemList)
        {
            inv.Add(new ItemStack(item, 1, 100));
        }
        return inv;
    }
    /// <summary>Applies decay to each item stack by reducing its condition based on the item's decay rate, unless the item is
    /// indefinite.
    /// </summary>
    protected void ItemStackDecay()
    {
        foreach (ItemStack itemStack in items)
        {
            itemStack.DecayItem();
        }
    }
    /// <summary>
    /// Simple immediate-mode UI drawing helpers.
    /// Draws common buttons and labels for inventory UI. This is intended for quick development tooling,
    /// not production UI. Keep logic here minimal — forward actions to your UI/presenter layer where possible.
    /// </summary>
    /// <param name="_x">X origin of the UI block.</param>
    /// <param name="_y">Y origin of the UI block.</param>
    /// <param name="showInvUI">If false the method returns without drawing anything.</param>
    protected void DrawButtons(int _x, int _y, bool showInvUI)
    {
        if (showInvUI == false)
        {
            return;
        }
        //inventoryUtils.DrawInventory(150, 340, inventoryList);           
        // Text lables
        GUI.Label(new Rect(_x, _y, 250, 20), "Item");
        GUI.Label(new Rect(_x + 80, _y, 250, 20), "Qty");
        GUI.Label(new Rect(_x + 140, _y, 250, 20), "Weight");
        GUI.Label(new Rect(_x + 190, _y, 250, 20), "Condition");

        // Sort Buttons ====         
        if (GUI.Button(new Rect(_x - 10, _y - 80, 90, 40), "Name"))
        {
            sortType = SortType.Alphabetical;
            ApplySortType(sortType);
        }
        if (GUI.Button(new Rect(_x + 80, _y - 80, 90, 40), "Condition"))
        {
            sortType = SortType.Condition;
            ApplySortType(sortType);
        }
        if (GUI.Button(new Rect(_x + 170, _y - 80, 85, 40), "Weight"))
        {
            sortType = SortType.Weight;
            ApplySortType(sortType);
        }
        //Filter Type Buttos ====
        if (GUI.Button(new Rect(_x - 10, _y - 100, 260, 20), "Material"))
        {
            sortFilter = SortFilter.Material;
            ApplyFilterDisplay(sortFilter);
        }
        if (GUI.Button(new Rect(_x - 10, _y - 120, 260, 20), "Clothes"))
        {
            sortFilter = SortFilter.Clothes;
            ApplyFilterDisplay(sortFilter);
        }
        if (GUI.Button(new Rect(_x - 10, _y - 140, 260, 20), "FirstAid"))
        {
            sortFilter = SortFilter.FirstAid;
            ApplyFilterDisplay(sortFilter);
        }
        if (GUI.Button(new Rect(_x - 10, _y - 160, 260, 20), "Tools"))
        {
            sortFilter = SortFilter.Tools;
            ApplyFilterDisplay(sortFilter);
        }
        if (GUI.Button(new Rect(_x - 10, _y - 180, 260, 20), "Food"))
        {
            sortFilter = SortFilter.Food;
            ApplyFilterDisplay(sortFilter);
        }
        if (GUI.Button(new Rect(_x - 10, _y - 200, 260, 20), "All"))
        {
            sortFilter = SortFilter.All;
            SyncDisplay();
        }
        //Use/Consume
        if (GUI.Button(new Rect(_x - 10, _y - 40, 125, 40), "Consume/Use")) { }
        if (GUI.Button(new Rect(_x + 130, _y - 40, 125, 40), "Drop Item")) { }
    } // Offload to UI systems

    /// <summary>
    /// Draws the player's inventory (itemsDisplay) to the screen.
    /// This uses <see cref="itemsDisplay"/> which should be kept in sync with the internal <see cref="items"/> list
    /// by calling <see cref="SyncDisplay"/> whenever the model changes.
    /// </summary>
    /// <param name="_x">X origin to draw the list at.</param>
    /// <param name="_y">Y origin to draw the list at.</param>
    /// <param name="showInvUI">Whether to render the UI or return early.</param>
    /// <param name="uiIndexed">If true the current selected entry will be offset for highlighting.</param>
    
    protected void DrawInvPlayer(int _x, int _y, bool showInvUI, bool uiIndexed)
    {
        if (showInvUI == false)
        {
            return;
        }
         // Display items in inventory
        int heightDisplacement = 20;
        for (int i = 0; i < itemsDisplay.Count; i++)
        {
            // Text offset by index
            int offset;
            if (i == listIndex && uiIndexed == true)
            {
                offset = 10;
            }
            else
            {
                offset = 0;
            }

            // Draw item attributes
            ItemStack guiItemIndex;
            guiItemIndex = itemsDisplay[i];
            var _condition = guiItemIndex.condition;
            _condition = Mathf.RoundToInt(_condition);
            var _weight = guiItemIndex.StackWeight;
            _weight = Mathf.Round(_weight * 100 / 100); 
            GUI.Label(new Rect(_x - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.Item.ItemName);
            GUI.Label(new Rect(_x + 80 - offset, _y + 40 + heightDisplacement * i, 300, 20), guiItemIndex.StackAmount.ToString() + "x");
            GUI.Label(new Rect(_x + 140 - offset, _y + 40 + heightDisplacement * i, 300, 20), _weight.ToString() + " kg");
            GUI.Label(new Rect(_x + 190 - offset, _y + 40 + heightDisplacement * i, 300, 20), _condition.ToString() + " %");
        }
    } // Offload to UI systems
       
    protected void ApplyFilterDisplay(SortFilter sortFilter)
    {
        if (sortFilter == SortFilter.Material) { FilterDisplay<MaterialItem>(); }
        else if (sortFilter == SortFilter.Clothes) { FilterDisplay<ClothesItem>(); }
        else if (sortFilter == SortFilter.FirstAid) { FilterDisplay<FirstAidItem>(); }
        else if (sortFilter == SortFilter.Tools) { FilterDisplay<ToolItem>(); }
        else if (sortFilter == SortFilter.Food) { FilterDisplay<FoodItem>(); }
    }
    
    protected void ApplySortType(SortType sortType)
    {
        if (sortType == SortType.Alphabetical)
        {
            itemsDisplay.Sort((left, right) => left.Item.ItemName.CompareTo(right.Item.ItemName));
        }
        else if (sortType == SortType.Condition)
        {
            itemsDisplay.Sort((left, right) => right.Condition.CompareTo(left.Condition));
        }
        else if (sortType == SortType.Weight)
        {
            itemsDisplay.Sort((left, right) => right.StackWeight.CompareTo(left.StackWeight));
        }
    }
    
    public void OnInventoryChanged()
    {
        SyncDisplay();
        ApplySortType(sortType);       
        ApplyFilterDisplay(sortFilter);
        CalculateCapacity();
    }

    private void FilterDisplay<T>()
    {
        itemsDisplay.Clear();

        foreach (ItemStack itemStack in items)
        {
            if (itemStack.Item is T)
            {
                // add item to filtered list.
                var filteredItem = itemStack.CopyStack();
                itemsDisplay.Add(filteredItem);
            }
        }
    }
}
