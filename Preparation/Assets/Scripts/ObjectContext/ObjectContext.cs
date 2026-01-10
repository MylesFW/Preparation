using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContext
{
    // Unity Components
    public Transform transform;
    public Rigidbody2D rigidbody;
    public BoxCollider2D collider;
    public Animator2D animator2D;
    public AudioListener audioListener;
    public AudioSource audioSource;

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

    // General Object Components
    public Inventory inventory;

    // Weather Machine
    public WeatherController weatherController;
}
