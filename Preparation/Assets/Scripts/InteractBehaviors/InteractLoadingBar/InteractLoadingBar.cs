using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLoadingBar : MonoBehaviour
{
    public Vector2 position;
    public int heightOffset;
    public float loadSpeed;

    public void Awake()
    {
        //percentProgress = 0;
        position = transform.position;
        position.y += heightOffset; 
    }

    public void Start()
    {
        this.enabled = false;
    }

    public void Update()
    {       
        loadSpeed -= 0.2f * Time.deltaTime;
        if (loadSpeed <= 0)
        {
            this.enabled = false;   
        }
    }        
    public void OnGUI()
    {
        var newspeed = loadSpeed * 100;
        newspeed = Mathf.RoundToInt(newspeed);
        GUI.Label(new Rect(960, 700, 100, 40), newspeed.ToString());
    }
    
    public void EnableThis(float interactTimer)
    {
        loadSpeed = interactTimer;
        this.enabled = true;
    }
}
