using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteMatrix
{
    // Brennan 1/18/26.
    // Store sprite strips into a 2d directional matrix.

    public SpriteMatrix()
    {
        //blank construction
    }
    
    
    public Sprite[] GetStripFromDirection(DirectionalSpriteCollection spriteSheet, Vector2 direction)
    {

        // Horizontal
        if (direction.Equals(Vector2.left))
        {
            return spriteSheet.left;
        }

        if (direction.Equals(Vector2.right))
        {
            return spriteSheet.right;
        }

        // Vertical
        if (direction.Equals(Vector2.up))
        {
            return spriteSheet.up;
        }
        if (direction.Equals(Vector2.down))
        {
            return spriteSheet.down;
        }

        // Diagonal
        if (direction.Equals(new Vector2(-1, 1)))
        {
            return spriteSheet.upLeft;
        }
        
        if (direction.Equals(new Vector2(1, 1)))
        {
            return spriteSheet.upRight;
        }
        
        if (direction.Equals(new Vector2(-1, -1)))
        {
            return spriteSheet.downLeft;
        }
        
        if (direction.Equals(new Vector2(1, -1)))
        {
            return spriteSheet.downRight;
        }

        return null;
    }
}
