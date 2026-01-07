using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPublisher : MonoBehaviour
{
    public bool enableLog;

    private FiniteStateMachine fsm;
    private PlayerController playerController;
    private Inputs playerInput;
    private PlayerHealth playerHealth;
    private PlayerStamina playerStamina;
    private PlayerTemp playerTemp;
    private PlayerCalories playerCalories;
    private PlayerFatigue playerFatigue;
    private PlayerThirst playerThirst;


    private void Awake()
    {
        fsm = GetComponent<FiniteStateMachine>(); 
        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<Inputs>();
        playerHealth = GetComponent<PlayerHealth>();
        playerStamina = GetComponent<PlayerStamina>();
        playerTemp = GetComponent<PlayerTemp>();
        playerCalories = GetComponent<PlayerCalories>();
        playerFatigue = GetComponent<PlayerFatigue>();
        playerThirst = GetComponent<PlayerThirst>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
