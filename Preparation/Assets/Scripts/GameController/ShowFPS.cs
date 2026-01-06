using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class ShowFPS : MonoBehaviour
{
    // pubs
    public bool showInfo;
    public int sampleInterval;
    public int sampleSize;

    public Inputs playerInput;

    // priv
    private float currentFPS;
    private float frameMin;
    private float frameMax;
    private float fpsAverageBySample;

    private int frameRounded;
    private int minRounded;
    private int maxRounded;
    private int sampledAVG;
    private int currentSampleInterval;

    List<float> sampleDateSetFPS = new List<float>();

    private void Awake()
    {
        frameMin = 900;
    }

    void Update()
    {
        
        // Update current frame rate
         
        currentFPS = 1 / Time.unscaledDeltaTime;

        // Toggle GUI text on screen

        if (playerInput.showGameControllerInfo == true && showInfo == false)
        {
            showInfo = true;
        }
        else if (playerInput.showGameControllerInfo == true && showInfo == true)
        {
            showInfo = false;
        }

        // Handles sample interval - logs current frame as the sample
        // Higher the interval the less accurate the fps (cheaper tho)

        if (currentSampleInterval == 0)
        {
            sampleDateSetFPS.Add(currentFPS);
            currentSampleInterval = sampleInterval;
        }
        else
        {
            currentSampleInterval--;
        }

        // Add up our frame samples 
        
        if (sampleDateSetFPS.Count == sampleSize)
        {
            float sampleSum = 0f;

            for (int i = 0; i < sampleSize; i++)
            {
                sampleSum += sampleDateSetFPS[i];
            }

            fpsAverageBySample = sampleSum / sampleDateSetFPS.Count;

            sampleDateSetFPS.Clear();

            if (fpsAverageBySample > frameMax)
            {
                frameMax = fpsAverageBySample;
            }
            if (fpsAverageBySample < frameMin)
            {
                frameMin = fpsAverageBySample;
            }
        }

        // Round Sample Data to ints
        
        sampledAVG = Mathf.RoundToInt(fpsAverageBySample);
        frameRounded = Mathf.RoundToInt(currentFPS);
        minRounded = Mathf.RoundToInt(frameMin);
        maxRounded = Mathf.RoundToInt(frameMax);
    }

    private void OnGUI()
    {
        // Gui Label

        if (showInfo == true)
        {
            GUI.Label(new Rect(10, 100, 300, 40), "FPS: " + sampledAVG.ToString());
            GUI.Label(new Rect(10, 120, 300, 40), "Low FPS: " + minRounded.ToString());
            GUI.Label(new Rect(10, 140, 300, 40), "High FPS: " + maxRounded.ToString());
        }
    }
}
