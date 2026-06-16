using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenState : State
{
    public ObjectContext self;
    public SpriteCollection spriteSheet;
    public float animSpeed;

    public OpenState(OpenStateTemplate template, FiniteStateMachine _fsm, ObjectContext _context)
    {
        fsm = _fsm;
        self = _context;
        name = template.name;
        priority = template.priority;
        locked = template.locked;
        forceOverride = template.forceOverride;

        self.animator2D.playAnimation = template.playAnimation;
        self.animator2D.frameMultiplier = template.animationSpeed;
        spriteSheet = template.spriteSheet;
        animSpeed = template.animationSpeed;
    }
}
