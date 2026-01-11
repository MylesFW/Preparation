using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryList = new List<Item>();
    private PlayerController playerController;
    private ObjectContext ItemContext;
    private Item guiItemIndex;
    private int listIndex;

    public int GetItemIndexByName(string item_name)
    {
        if (inventoryList.Count == 0)
        {
            Debug.Log("GetItemIndexByClass(): desired item not found in inventory");
            return -1;
        }

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].name == item_name)
            {
                return i;
            }
        }

        Debug.Log("GetItemIndexByClass(): desired item not found in inventory");
        return -1;
    }

    public void AddItem(Item item)
    {
        // Not Stackable, no stack logic needed. Add to inventory
        // If stackable, but no items stored to stack to, add to inventory
        // If item availiable to stack, add to the stack inventory;

        if (item.stackable == false)
        {
            inventoryList.Add(item);
            return;
        }
        else if (item.stackable == true)
        {
            int _stackdetected = GetItemIndexByName(item.name);

            if (_stackdetected == -1)
            {
                inventoryList.Add(item);
                item.UpdateStackWeight();
                return;
            }
            else
            {
                inventoryList[_stackdetected].currentStackAmount += item.currentStackAmount;
                item.UpdateStackWeight();
                return;
            }
        }
    }

    public void UseConsumeItem(int _index)
    {
        if (listIndex <= inventoryList.Count - 1 && listIndex >= 0)
        {
            Item _item = inventoryList[_index];
            _item.OnConsume();
            if (_item is FoodItem)
            {
               FoodItem _food = (FoodItem)_item;
               if (_food.calories == 0)
                {
                    inventoryList.RemoveAt(_index);
                }
            }
        }
       
        listIndex = Mathf.Clamp(listIndex, 0, inventoryList.Count - 1);
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        ItemContext = playerController.playerContext;
        listIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            listIndex--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            listIndex++;
        }
        
        listIndex = Mathf.Clamp(listIndex, 0, inventoryList.Count - 1);
    }

    private void OnGUI()
    {

        GUI.Label(new Rect(1410, 340, 250, 20),"Item"); 
        GUI.Label(new Rect(1490, 340, 250, 20),"Qty");
        GUI.Label(new Rect(1550, 340, 250, 20), "Weight");
        GUI.Label(new Rect(1600, 340, 250, 20), "Condition");

        if (GUI.Button(new Rect(1400, 300, 125, 40), "Consume/Use"))
            UseConsumeItem(listIndex);

        GUI.Button(new Rect(1540, 300, 125, 40), "Drop Item");

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
                    
                guiItemIndex = inventoryList[i];
                GUI.Label(new Rect(1410 - offset, 380 + heightDisplacement * i, 300, 20), guiItemIndex.name);              
                GUI.Label(new Rect(1490 - offset, 380 + heightDisplacement * i, 300, 20), guiItemIndex.currentStackAmount.ToString() + "x");
                GUI.Label(new Rect(1550 - offset, 380 + heightDisplacement * i, 300, 20), guiItemIndex.currentStackWeight.ToString() + " kg");
                GUI.Label(new Rect(1600 - offset, 380 + heightDisplacement * i, 300, 20), guiItemIndex.condition.ToString() + " %");
            }
        }
    }
}
