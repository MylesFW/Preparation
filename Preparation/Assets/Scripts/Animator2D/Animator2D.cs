using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngineInternal;

public class Animator2D : MonoBehaviour
{
    // Brennan
    // Custom Animator 2D Component. Owns the current sprite to show, and handles frame looping

    public Sprite[] awakeSprite;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Sprite[] currentStrip;

    // Hidden Pubs

    [HideInInspector] public bool playAnimation;
    [HideInInspector] public float frameMultiplier;
    [HideInInspector] public int frameIndex;

    // privs
    public float frameSpeed;
    public bool loop;
    public bool reverse;

    private bool flag;

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
            if (reverse == true)
            {
                frameIndex--;
            }
            else if (reverse == false) 
            {
                frameIndex++;
            }
        }
        frameIndex = Mathf.Clamp(frameIndex, 0, currentStrip.Length - 1);
    }
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameIndex = 0;
        
        loop = true;
        playAnimation = false;
        reverse = false;
        currentStrip = awakeSprite;
        spriteRenderer.sprite = currentStrip[frameIndex];
    }

    // Update is called once per frame
    private void Update()
    {
        if (flag == true)
        {
           Debug.Log("Flag Reached");
        }

        
        if (frameIndex == currentStrip.Length - 1)
        {
            if (loop == true)
            {
                frameIndex = 0;
            }
        }
        
        FrameRate();
        spriteRenderer.sprite = currentStrip[frameIndex];
    }
    
    public void HandleLooping()
    {
        if (reverse == false)
        {
            if (frameIndex == currentStrip.Length - 1)
            {
                if (loop == true)
                {
                    frameIndex = 0;
                }
            }
        }
        else if (reverse == true) 
        {
            if (frameIndex == 0)
            {
                if (loop == true)
                {
                    frameIndex = currentStrip.Length - 1;
                }
            }
        }
    }

    public void SwitchSpriteStrip(Sprite[] newStrip)
    {
        currentStrip = newStrip;
        Debug.Log("Sprite strip changed");
        flag = true;
    }
}
