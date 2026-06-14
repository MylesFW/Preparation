using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerMoveState : State
{
    public PlayerContext self;
    
    private Vector2 direction;
    private Vector2 velocity;
    private Vector2 position;
    private float walkSpeed;
    private float animSpeed;
    private SpriteMatrix spriteMatrix;
    private DirectionalSpriteCollection directionalSpriteSheet;

    // Constructor
    public PlayerMoveState(MoveStateTemplate template, FiniteStateMachine _fsm, PlayerContext _context)
    {
        // Assign default State stuff
        fsm = _fsm;
        self = _context;
        name = template.name;
        priority = template.priority;
        locked = template.locked;
        forceOverride = template.forceOverride;
        
        // Walk specific
        walkSpeed = template.walkSpeed;        
        self.animator2D.playAnimation = template.playAnimation;
        self.animator2D.frameMultiplier = template.animationSpeed;
        self.animator2D.loop = template.loop;
        directionalSpriteSheet = template.directionSpriteSheet;
    }

    private void MovePlayerWithInputs(float _maxSpeed)
    {
        // vec
        position = new Vector2(self.transform.position.x, self.transform.position.y);
        velocity = self.playerInput.inputVector;
        direction = new Vector2(velocity.x, velocity.y);
        
        velocity.Normalize();

        velocity = Vector2.ClampMagnitude(velocity, _maxSpeed);
        velocity = position + velocity;

        // apply velocity
        self.playerMovement.velocity = velocity;
    }

    // Called once per State Enter
    public override void Enter()
    {
        spriteMatrix = new SpriteMatrix();
    }
    // Called once per frame until state switch is called
    public override void Run()
    {
        MovePlayerWithInputs(walkSpeed);
        Sprite[] newStrip = spriteMatrix.GetStripFromDirection(directionalSpriteSheet, direction);        
        if (newStrip != null)
        {
            self.animator2D.SwitchSpriteStrip(newStrip);
        }
    }
    public override void Exit()
    {
        self.animator2D.frameIndex = 0;
        self.animator2D.playAnimation = false;
        spriteMatrix = null;
    }
}
