using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSorter : MonoBehaviour
{
    // Brennan
    // Re-sorts object's sprite renderer component based on Y height
    // sort order is scaled to increase precision

    private SpriteRenderer spriteRenderer;
    public SortingLayer thisLayer;
    public bool fixedDepth;
    public int sortOrder;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 0;    
    }

    void Update()
    {
        if (fixedDepth == false)
        {
            sortOrder = Mathf.RoundToInt(transform.position.y);       
        }

        sortOrder *= -1;
        sortOrder *= 10;
        spriteRenderer.sortingOrder = sortOrder;
    }
}
