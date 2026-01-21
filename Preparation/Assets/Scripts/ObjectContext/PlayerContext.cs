using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext : ObjectContext
{
    public AudioListener audioListener;

    // Player Components
    public Inputs playerInput;
    public PlayerMovement playerMovement;
    public PlayerCalories playerCalories;
    public PlayerHealth playerHealth;
    public PlayerFatigue playerFatigue;
    public PlayerThirst playerThirst;
    public PlayerTemp playerTemp;
    public PlayerStamina playerStamina;
    public BuffManager playerBuffManager;
    public PlayerController playerController;
    public InteractManager interactManager;
    public Inventory inventory;
}
