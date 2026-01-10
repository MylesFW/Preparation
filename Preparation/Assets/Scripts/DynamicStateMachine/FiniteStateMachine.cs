using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FiniteStateMachine : MonoBehaviour
{
    public bool enableLogging;
    public ObjectContext context;
    private State currentState;
    private State _nextState;
    
    List<State> stateQueue = new List<State>();
    [HideInInspector] public Action<State> OnStateSwitch;

    public void LogNewState()
    {
        if (!enableLogging)
        {
            return;
        }
        
        Debug.Log(this.name + " Changed State: " + currentState.name);
    }
    public void EnqueueState(State _state)
    {           
        stateQueue.Add(_state);      
    }
    private void SwitchState(State _state)
    {
        currentState.Exit();        
        currentState = _state;
        currentState.Enter();       
        LogNewState();
        stateQueue.Clear();        
    }
    public void HandleSwitchState(State _state)
    {
        if (currentState.name == _state.name)
        {
            return;
        }

        if (currentState.locked == true && _state.forceOverride == false)
        {
            return;
        }
        SwitchState(_state);
        OnStateSwitch?.Invoke(currentState);
    }
        
    private void Start()
    {
        currentState = new NullIdle(this, context);
        currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        if (stateQueue.Count > 0)
        {
            stateQueue.Sort((left, right) => left.priority.CompareTo(right.priority));
            int i = stateQueue.Count - 1;
            _nextState = stateQueue[i];
            HandleSwitchState(_nextState);           
        }
        stateQueue.Clear();
        currentState.Run();   
    }
}
