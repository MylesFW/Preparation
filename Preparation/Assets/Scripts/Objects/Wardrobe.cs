using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wardrobe : Interactable
{
    [Header("Contents")]
    public Item contents;
    public bool isSearched;
    public BoolValue storedSearched;

    [Header("Signals and Dialog")]
    public Inventory playerInventory;
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Search Animation")]
    public Animator anim; 
    // Start is called before the first frame update

    void Start()
    {
        anim = GetComponent<Animator>();
        isSearched = storedSearched.RuntimeValue;
        if (isSearched)
        {
            anim.SetBool("searched", true);
        }
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
        storedSearched.RuntimeValue = isSearched;
    }

    public void WardrobeSearched()
    {
       //dialog off
       dialogBox.SetActive(false);
       //raise the signal to the player to stop animating
       raiseItem.Raise();
        playerInRange = false;
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
