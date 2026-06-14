using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTime : MonoBehaviour
{
    // Brennan 3/2/26
    // Reduces/scales the Simulations minute scaling, speeding up time

    public VoidEventChannelSO OnSimulationHour;

    [SerializeField] private SimTime simTime;
    private int m_Time;
    private bool showUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private FiniteStateMachine fsm;
    [SerializeField] private PlayerContext playerContext;
    private bool showWaitButton;
    private WorldTimer timer;

    void Start()
    {
        GameObject inst = ObjectRegistry.instance.Get("BundledGuy");
        if (inst != null)
        {
            var pc = inst.GetComponent<PlayerController>();
            playerContext = pc.playerContext;
        }

        showUI = false;
        showWaitButton = false;
    }
    void Update()
    {
        ProcessInputs();
    }
    private void OnGUI()
    {
        DrawUI(500, 500);
    }
    private void ProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (showUI == false)
            {
                IndexWarpMenu();
            }
            else
            {
                DeindexWarpMenu();
            }
        }
        
        if (showUI)
        {
            if (playerContext.playerInput.exitPressed || playerContext.playerInput.interactDown)
            {
                DeindexWarpMenu();
            }
        }
    }
    private void DrawUI(int _x, int _y)
    {
        if (showUI == false) return;
        GUI.Label(new Rect(_x - 10, _y - 100, 90, 40), "Wait Time: " + m_Time.ToString());

        // Buttons         
        if (GUI.Button(new Rect(_x - 10, _y - 80, 90, 40), "-"))
        {
            m_Time--;
            m_Time = Mathf.Clamp(m_Time, 0, 24);
        }
        if (GUI.Button(new Rect(_x + 80, _y - 80, 90, 40), "+"))
        {
            m_Time++;
            m_Time = Mathf.Clamp(m_Time, 0, 24);
        }
        if (showWaitButton == false) { return; }
        if (m_Time > 0)
        {
            if (GUI.Button(new Rect(_x + 170, _y - 80, 85, 40), "Wait"))
            {
                if (m_Time <= 0) { return; }
                BeginWait();
            }
        }
    }
    private void BeginWait()
    {
        simTime.minuteScale = simTime.minuteScale / 100;
        timer = new WorldTimer(m_Time);
        OnSimulationHour.onEventRaised += UpdateWait;
        showWaitButton = false;
    }
    private void UpdateWait()
    {
        
        timer.updateTimer();
        m_Time = timer.timer;
        if (timer.complete == true || m_Time < 0)
        {
            OnSimulationHour.onEventRaised -= UpdateWait;
            timer = null;
            simTime.MinuteScale = 1f;
            showUI = false;
            fsm.ForceState(new IdleState(playerController.playerForcedIdle, fsm, playerContext));
            showWaitButton = true;
        }
    }
    private void IndexWarpMenu()
    {
        fsm.EnqueueState(new IdleState(playerController.playerForcedLockedIdle, fsm, playerContext));
        showWaitButton = true;
        showUI = true;
    }
    private void DeindexWarpMenu()
    {
        fsm.ForceState(new IdleState(playerController.playerForcedIdle, fsm, playerContext));
        showWaitButton = false;
        showUI = false;
    }
}

