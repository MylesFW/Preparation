using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wardrobe : Interactable
{
    public Item contents;
    public bool isSearched;
    public Inventory playerInventory;
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    public Animator anim; 
    // Start is called before the first frame update

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange) //Input.GetKey => if a key is held Input.GetKeyDown => if a key is pressed
        {
            if (!isSearched)
            {
                //Search the wardrobe
                SearchWardrobe();
            } else
            {
                //Wardrobe has been Searched
                WardrobeSearched();
            }
        }
    }

    public void SearchWardrobe()
    {
        //dialog box on
        dialogBox.SetActive(true);
        //dialog text = content text
        dialogText.text = contents.itemDescription;
        //add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //raise signal for player animation
        raiseItem.Raise();
        //raise context clue
        context.Raise();
        //set wardrobe to Searched
        isSearched = true;
        anim.SetBool("searched", true);
    }

    public void WardrobeSearched()
    {
       //dialog off
       dialogBox.SetActive(false);
       //raise the signal to the player to stop animating
       raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isSearched)
        {
            context.Raise();
            Debug.Log("Player in range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isSearched)
        {
            context.Raise();
            Debug.Log("Player out of range");
            playerInRange = false;
        }

    }
}
