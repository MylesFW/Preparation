using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ObjectContext self;
    public DirectionalSpriteCollection directionalSpriteSheet;   
    public float animSpeed;
    private SpriteMatrix spriteMatrix;

    //Constructor
    public IdleState(IdleStateTemplate template, FiniteStateMachine _fsm, ObjectContext _context)
    {
        fsm = _fsm;
        name = template.name;
        self = _context;
        priority = template.priority;
        locked = template.locked;
        forceOverride = template.forceOverride;

        self.animator2D.playAnimation = template.playAnimation;
        self.animator2D.frameMultiplier = template.animationSpeed;
        self.animator2D.loop = template.loop;
        directionalSpriteSheet = template.directionSpriteSheet;
        animSpeed = template.animationSpeed;

    }

    // Called once on State enter
    public override void Enter()
    {

    }
    // Called once per frame until the State is switched
    public override void Run()
    {

    }
    // Called once on State switch
    public override void Exit()
    {

    }
}
