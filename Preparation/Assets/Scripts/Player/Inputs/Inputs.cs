using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    // KeyBindings
    public KeyCode leftKey          = KeyCode.A;
    public KeyCode rightKey         = KeyCode.D;
    public KeyCode upKey            = KeyCode.W;
    public KeyCode downKey          = KeyCode.S;
    public KeyCode crouchKey        = KeyCode.C;
    public KeyCode backKey          = KeyCode.Escape;
    public KeyCode checkInventoryKey = KeyCode.Tab;
    public KeyCode quickMenu        = KeyCode.Space;
    public KeyCode interactKey      = KeyCode.E;
    public KeyCode exitKey          = KeyCode.Escape;

    [HideInInspector] public Vector2 mousePos;

    // Input check bools // pressed = once per input until reset.
    [HideInInspector] public bool leftPressed, rightPresseed, upPressed, downPressed, exitPressed;
    [HideInInspector] public bool crouchPressed, inventoryPressed, sprintHold, interactDown, interactUp;
    [HideInInspector] public bool interact, altClick, cycleEquiped, showGameControllerInfo;

    // Input Axis (Project Settings) 
    [HideInInspector] public Vector2 inputVector;
    private float horizontalInput;
    private float verticalInput;

    public bool enableBias;
    public int biasInt;

    // Input Settings    
    [HideInInspector] public bool crouchToggle;
    [HideInInspector] public bool autoWalk;

    private bool leftHeld, rightHeld, upHeld, downHeld;

    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }
    
    private void Awake()
    {
        crouchToggle = false;
        autoWalk = false;
        enableBias = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check inputs per frame
        UpdateInputAxis();
        UpdateButtonInputs();
        horizontalInput = Convert.ToInt32(rightHeld) - Convert.ToInt32(leftHeld);
        verticalInput = Convert.ToInt32(upHeld) - Convert.ToInt32(downHeld);

        SteeringBias(enableBias, biasInt);

    }
    // Methods
    public void UpdateButtonInputs()
    {
        string logx = horizontalInput.ToString();
        string logy = verticalInput.ToString();

        //Debug.Log(logx + " " + logy);  

        // WASD on pressed Bool (only true on first frame pressed)
        leftPressed = Input.GetKeyDown(leftKey);
        rightPresseed = Input.GetKeyDown(rightKey);
        upPressed = Input.GetKeyDown(upKey);
        downPressed = Input.GetKeyDown(downKey);

        leftHeld = Input.GetKey(leftKey);
        rightHeld = Input.GetKey(rightKey);
        upHeld = Input.GetKey(upKey);
        downHeld = Input.GetKey(downKey);

        // Keyboard pressed
        crouchPressed = Input.GetKeyDown(KeyCode.LeftControl);
        inventoryPressed = Input.GetKeyDown(checkInventoryKey);
        sprintHold = Input.GetKey(KeyCode.LeftShift);
        cycleEquiped = Input.GetKeyDown(KeyCode.Q);
        showGameControllerInfo = Input.GetKey(KeyCode.X);
        exitPressed = Input.GetKeyDown(exitKey);

        interact = Input.GetKey(interactKey);
        interactDown = Input.GetKeyDown(interactKey);
        interactUp = Input.GetKeyUp(interactKey);

        // Mouse clicks 
        altClick = Input.GetMouseButton(1);
    }
    public void UpdateInputAxis()
    {                      
        // WASD Inputs (Used for movement Velocity)
        // horizontalInput = Input.GetAxisRaw("Horizontal");
        // verticalInput = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(horizontalInput, verticalInput);

        // Mouse Inputs
        mousePos = Input.mousePosition;
    }

    public void SteeringBias(bool enable, int vertinput)
    {
        if (enable == true)
        {
            if (horizontalInput == 1)
            {
                verticalInput = vertinput;
            }
            else if (horizontalInput == -1)
            {
                verticalInput = vertinput * -1;
            }
        }     
    }
}
