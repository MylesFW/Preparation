using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTimer
{
    public int timer;
    public bool complete = false;

    public WorldTimer (int timer)
    {
        this.timer = timer;
    }
    
    public void updateTimer()
    {
        timer--;
        if (timer == 0)
        {
            complete = true;
        }
    }
}
