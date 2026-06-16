using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    public void OnEnter();
    public void OnStay();
    public void OnExit();
}

