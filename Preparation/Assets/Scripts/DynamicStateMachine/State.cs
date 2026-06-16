using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{  
    public int priority         = 0;
    public bool locked          = false;
    public bool forceOverride   = false;
    public string name = "BaseState"; 
    protected FiniteStateMachine fsm;
    
    public virtual void Enter() { }
    public virtual void Run() { }
    public virtual void Exit() { }
   
}
