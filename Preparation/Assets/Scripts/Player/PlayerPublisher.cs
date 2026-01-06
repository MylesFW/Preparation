using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPublisher : MonoBehaviour
{
    public bool enableLog;

    public FiniteStateMachine fsm;
    public PlayerController playerController;
    public Inputs playerInput;
    
    public PlayerHealth playerHealth;
    public PlayerStamina playerStamina;
    public PlayerTemp playerTemp;
    public PlayerCalories playerCalories;
    public PlayerFatigue playerFatigue;
    public PlayerThirst playerThirst;


    private void Awake()
    {
        fsm = GetComponent<FiniteStateMachine>(); 
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
