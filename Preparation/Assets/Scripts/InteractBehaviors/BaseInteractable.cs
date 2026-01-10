using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour 
{
    public float interactTimer;
    public virtual void ExecuteInteraction(ObjectContext _self) { }

}
