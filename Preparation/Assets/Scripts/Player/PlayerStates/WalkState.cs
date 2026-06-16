using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WalkState : State
{
    public PlayerContext self;
    private Vector2 direction;
    private Vector2 velocity;
    private Vector2 position;
     
    private float walkSpeed;

    private SpriteMatrix spriteMatrix;
    
    private DirectionalSpriteCollection directionalSpriteSheet;

    //Constructor
    public WalkState(FiniteStateMachine _fsm, PlayerContext _context, string _name = "WalkState", int _priority = 1, bool _locked = false, bool _forceOverride = false)
    {
        name = _name;
        fsm = _fsm;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }

    private void MovePlayerWithInputs(float _maxSpeed)
    {
        // Set position and velocity to the players pos and vel.
        position = new Vector2(self.transform.position.x, self.transform.position.y);
        velocity = self.playerInput.inputVector;

        // Normalize to prevent faster diagonal movement
        velocity.Normalize();

        direction = new Vector2(velocity.x, velocity.y);

        // Clamp velocity to max speed
        velocity = Vector2.ClampMagnitude(velocity, _maxSpeed);
        velocity = position + velocity;

        // Set player velocity to the new calculated walk velocity
        self.playerMovement.velocity = velocity;
    }

    private void SelectSpriteSheet(Vector2 _direction)
    {
        //spriteMatrix.GetSheetFromVector(_direction);
    }
    
    // Called once per State Enter
    public override void Enter()
    {
        //spriteMatrix = new SpriteMatrix(directionalSpriteSheet);

        walkSpeed = 0.05f;
        self.animator2D.playAnimation = true;
        self.animator2D.frameMultiplier = 0.09f;
    }
    // Called once per frame until state switch is called
    public override void Run()
    {
        MovePlayerWithInputs(walkSpeed);
        SelectSpriteSheet(direction);
    }
    public override void Exit()
    {
        self.animator2D.frameIndex = 1;
        self.animator2D.playAnimation = false;
        spriteMatrix = null;
    }  
}
