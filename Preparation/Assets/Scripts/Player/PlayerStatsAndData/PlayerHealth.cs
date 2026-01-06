using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float currentDrainRate;
    public float maxDrainRate;

    public int myHealth;
    public bool invulnerable;
    public bool drainHealth;

    public Action isAfflicted;
    public Action isCritical;
    public Action isDead;

    public SimTime simTime;
    private FiniteStateMachine fsm;
    public PlayerTemp playerTemp;
    public PlayerStamina playerStamina;
    public PlayerThirst playerThirst;
    public PlayerCalories playerCalories;
    public PlayerFatigue playerFatigue;
    
    private void HealthDrainOnTick()
    {
        if (drainHealth == true)
        {
            simTime.OnSimulationTick += HandleHealthDrain;
        }
        else if (drainHealth == false)
        {
            simTime.OnSimulationTick -= HandleHealthDrain;
        }
    }
    private void HandleHealthDrain()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 1 * currentDrainRate;
        }
    }
    public void OnDeath()
    {
        if (currentHealth <= 0 && invulnerable == false)
        {
            fsm.EnqueueState(new DeathState(fsm, fsm.context));
        }
    }
    private void HandleEvents()
    {
        if (myHealth == 50)
        {
            isAfflicted?.Invoke();
        }
        else if (myHealth == 25)
        {
            isCritical?.Invoke();
        }
        else if (myHealth == 0)
        {
            isDead?.Invoke();
        }
    }

    private void Awake()
    {
        fsm = GetComponent<FiniteStateMachine>();
        maxHealth = 100;
        currentHealth = maxHealth;       
        maxDrainRate = 0.1f;
        currentDrainRate = 0f;
        drainHealth = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        myHealth = Mathf.RoundToInt(currentHealth);
        myHealth = Mathf.Clamp(myHealth, 0, Mathf.RoundToInt(maxHealth));
       
        OnDeath();
        HealthDrainOnTick();
        HandleEvents();
        OnDeath();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 960, 300, 20), "Health: " + myHealth.ToString() + "%");
    }
}
