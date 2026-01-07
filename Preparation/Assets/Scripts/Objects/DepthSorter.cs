using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSorter : MonoBehaviour
{
    // Brennan
    // Re-sorts object's sprite renderer component based on Y height
    // sort order is scaled to increase precision

    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int sortIndex = Mathf.RoundToInt(transform.position.y);
        
        sortIndex *= -1;
        sortIndex *= 10;
        spriteRenderer.sortingOrder = sortIndex;
    }
}
