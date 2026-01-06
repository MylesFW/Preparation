using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class Buff
{
    public string name;
    public string description;
    public string cause;
    public string remedy;
    
    public int duration;
    public int maxDuration;

    public bool indefinite;
    public bool stackable;

    public SimTime simTime;
    public ObjectContext context;
   
    public virtual void Enter() { }
    public virtual void Run() { }
    public virtual void Exit() { } 
}