using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{      
    public float interactTimer{get; set;}
    public bool Locked { get; set; }
    public MaterialItemDataSO Key { get; set; }
    public InteractTemplate InteractTemplate { get; set; }
    public InteractMode InteractMode { get; set; }
    void QueueInteract();
    void Interact(GameObject interactor);
    void EndInteraction();
    void AbortInteraction();
}
