using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchWalkState : State
{
    public PlayerContext self;
    private Vector2 velocity;
    private Vector2 position;
    private float crouchwalkSpeed;

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

    }

    // Called once on State enter
    public override void Enter()
    {
        crouchwalkSpeed = 0.025f;
        self.animator2D.playAnimation = true;
        self.animator2D.frameMultiplier = 0.2f;
    }
    // Called once per frame until the State is switched
    public override void Run()
    {
        MovePlayerWithInputs(crouchwalkSpeed);
        WalkAnimation();
    }
    // Called once on State switch
    public override void Exit()
    {
        self.animator2D.playAnimation = false;
    }

    //Constructor
    public CrouchWalkState(FiniteStateMachine _fsm, PlayerContext _context, string _name = "CrouchWalkState", int _priority = 1, bool _locked = false, bool _forceOverride = false)
    {
        fsm = _fsm;
        name = _name;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
