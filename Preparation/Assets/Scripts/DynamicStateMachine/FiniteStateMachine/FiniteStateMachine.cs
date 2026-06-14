using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FiniteStateMachine : MonoBehaviour
{
    public bool enableLogging;
    public ObjectContext context;
    protected State currentState;
    private State _nextState;
    protected List<State> stateQueue = new List<State>();
    [HideInInspector] public Action<State> OnStateSwitch;

    // Properties
    public State CurrentState {  get { return currentState; } }
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

    public void EnqueueState(State _state)
    {
        stateQueue.Add(_state);
    }
    public void ForceState(State _state) 
    {
        SwitchState(_state);
    }
    private void SwitchState(State _state)
    {
        currentState.Exit();
        currentState = _state;
        currentState.Enter();
        LogNewState();
    }
    private void HandleSwitchState(State _state)
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
    private void LogNewState()
    {
        if (!enableLogging)
        {
            return;
        }

        Debug.Log(this.name + " Changed State: " + currentState.name);
    }
}
