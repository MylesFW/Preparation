using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Animator2D : MonoBehaviour
{   
    // Brennan
    // Custom Animator 2D Component
    
    // Declares
    
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Sprite[] currentSheet;

    // Declare stored sprite sheets (too change!)

    public Sprite[] spr_walkDown;
    public Sprite[] spr_walkUp;
    public Sprite[] spr_walkRight;
    public Sprite[] spr_walkLeft;
    public Sprite[] spr_walkDownLeft;
    public Sprite[] spr_walkDownRight;
    public Sprite[] spr_walkUpLeft;
    public Sprite[] spr_walkUpRight;

    // Hidden Publics

    [HideInInspector] public bool playAnimation;
    [HideInInspector] public float frameMultiplier;
    [HideInInspector] public int frameIndex;
    
    // privs

    private float frameSpeed;
    private bool loop;
    
    private void FrameRate()
    {
        
        // Checks if the frame timer has reached 0
        // update animation index if so
        // if animations have been switched off, do nothing

        if (!playAnimation)
        {
            return;
        }

        if (frameSpeed > 0)
        {
            frameSpeed -= Time.deltaTime;
        }
        else if (frameSpeed <= 0)
        {
            frameSpeed = frameMultiplier;
            frameIndex++;
        }
    }
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        frameIndex = 0;
        
        loop = true;
        playAnimation = false;
        
        currentSheet = spr_walkDown;
        spriteRenderer.sprite = currentSheet[frameIndex];
    }

    // Update is called once per frame
    void Update()
    {            
        if (frameIndex == currentSheet.Length - 1)
        {
            if (loop == true)
            {
                frameIndex = 0;
            }
        }
        
        FrameRate();
        spriteRenderer.sprite = currentSheet[frameIndex];
    }
}
