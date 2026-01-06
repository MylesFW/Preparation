using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WalkState : State
{
    private Vector2 velocity;
    private Vector2 position;
    private float walkSpeed;

    private void MovePlayerWithInputs(float _maxSpeed)
    {
        // Set position and velocity to the players pos and vel.
        position = new Vector2(self.transform.position.x, self.transform.position.y);
        velocity = self.playerInput.inputVector;

        // Normalize to prevent faster diagonal movement
        velocity.Normalize();

        // Clamp velocity to max speed
        velocity = Vector2.ClampMagnitude(velocity, _maxSpeed);
        velocity = position + velocity;

        // Set player velocity to the new calculated walk velocity
        self.playerMovement.velocity = velocity;
    }

    private void WalkAnimation()
    {

        if (self.playerInput.inputVector.Equals(Vector2.left))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkLeft;
        }
        if (self.playerInput.inputVector.Equals(Vector2.up))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkUp;
        }
        if (self.playerInput.inputVector.Equals(Vector2.down))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkDown;
        }
        if (self.playerInput.inputVector.Equals(Vector2.right))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkRight;
        }
        if (self.playerInput.inputVector.Equals(new Vector2(-1, 1)))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkUpLeft;
        }
        if (self.playerInput.inputVector.Equals(new Vector2(1, 1)))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkUpRight;
        }
        if (self.playerInput.inputVector.Equals(new Vector2(-1, -1)))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkDownLeft;
        }
        if (self.playerInput.inputVector.Equals(new Vector2(1, -1)))
        {
            self.animator2D.currentSheet = self.animator2D.spr_walkDownRight;
        }
        
        self.animator2D.playAnimation = true;
    }
    
    // Called once per State Enter
    public override void Enter()
    {
        walkSpeed = 0.05f;
        self.animator2D.playAnimation = true;
        self.animator2D.frameMultiplier = 0.09f;
    }
    // Called once per frame until state switch is called
    public override void Run()
    {
        MovePlayerWithInputs(walkSpeed);
        WalkAnimation();
    }
    public override void Exit()
    {
        self.animator2D.frameIndex = 1;
        self.animator2D.playAnimation = false;
    }
   
    //Constructor
    public WalkState(FiniteStateMachine _fsm, ObjectContext _context, string _name = "WalkState", int _priority = 1, bool _locked = false, bool _forceOverride = false)
    {
        name = _name;
        fsm = _fsm;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
