using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{      
    void QueueInteract(GameObject interactor, IInteractable self);
    void Interact(GameObject interactor);
    void EndInteraction();
}
