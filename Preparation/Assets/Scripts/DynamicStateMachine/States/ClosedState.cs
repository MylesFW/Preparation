using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ClosedState : State
{
    public ObjectContext self;
    public SpriteCollection spriteSheet;
    public float animSpeed;
    public bool loopAnim;
    public bool reverseAnime;

    public ClosedState(ClosedStateTemplate template, FiniteStateMachine _fsm, ObjectContext _context, bool _locked)
    {
        fsm = _fsm;
        self = _context;
        name = template.name;
        priority = template.priority;
        locked = _locked;
        forceOverride = template.forceOverride;

        self.animator2D.playAnimation = template.playAnimation;
        self.animator2D.frameMultiplier = template.animationSpeed;
        spriteSheet = template.spriteSheet;
        animSpeed = template.animationSpeed;

        self.animator2D.loop = template.loopAnim;
        reverseAnime = template.reverseAnim;

    }
    public override void Enter()
    {


        self.animator2D.currentStrip = spriteSheet.spriteStrips;
        self.animator2D.playAnimation = true;
        self.animator2D.frameMultiplier = animSpeed;
        self.animator2D.loop = loopAnim;
        self.animator2D.reverse = reverseAnime;
    }
    public override void Run()
    {

    }
    public override void Exit()
    {
        self.animator2D.playAnimation = false;
    }
}
