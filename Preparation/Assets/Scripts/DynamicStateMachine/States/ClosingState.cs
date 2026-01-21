using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ClosingState : State
{
    public ObjectContext self;
    public StorageContainerController controller;
    public SpriteCollection spriteSheet;
    public float animSpeed;
    public bool loopAnim;
    public bool reverseAnime;

    public ClosingState(ClosingStateTemplate template, FiniteStateMachine _fsm, ObjectContext _context)
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
        loopAnim = template.loopAnim;
        reverseAnime = template.loopAnim;
    }

    public override void Enter()
    {

    }
    public override void Run()
    {
        if (self.animator2D.frameIndex == self.animator2D.currentStrip.Length - 1)
        {
            //fsm.EnqueueState(new Closed)
        }
    }
    public override void Exit()
    {
        self.animator2D.playAnimation = false;
    }
}
