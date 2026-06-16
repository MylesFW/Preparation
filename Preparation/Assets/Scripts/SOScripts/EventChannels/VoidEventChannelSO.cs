using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Event/Void Event", order = 4)]
public class VoidEventChannelSO : ScriptableObject
{
    public Action onEventRaised;
    
    public void raiseEvent() 
    {  
        onEventRaised?.Invoke();     
    }
}
