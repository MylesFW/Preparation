using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBias : MonoBehaviour, ICollidable
{  
    public Inputs inputs;
    public PlayerMovement playerMovemet;
    public Vector2 direction;
    public float speedMod;
    public int bias;

    void ICollidable.OnEnter()
    {
        //playerMovemet.SetSlowSpeed(speedMod);
        inputs.enableBias = true;
    }
    void ICollidable.OnStay()
    {
        inputs.biasInt = bias;
    }
    void ICollidable.OnExit()
    {
        //playerMovemet.EndSlow();
        inputs.biasInt = 0;
        inputs.enableBias = false;
    }
}
