using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningState : State
{
    public ObjectContext self;
    public SpriteCollection spriteSheet;
    public float animSpeed;
    public bool loopAnim;
    public bool reverseAnime;

    public AudioClip openingSound;
    public AudioClip closedSound;

    public OpeningState(OpeningStateTemplate template, FiniteStateMachine _fsm, ObjectContext _context)
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
        self.animator2D.loop = template.loopAnim;
        reverseAnime = template.loopAnim;
        openingSound = template.openingSound;
        closedSound = template.closedSound;
    }

    public override void Enter()
    {       
        self.audioSource.PlayOneShot(openingSound);

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
        self.audioSource.PlayOneShot(closedSound);
        self.animator2D.playAnimation = false;
    }

}

