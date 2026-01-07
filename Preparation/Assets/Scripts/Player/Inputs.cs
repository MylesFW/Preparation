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
    public KeyCode checkInventory   = KeyCode.E;
    public KeyCode quickMenu        = KeyCode.Space;

    [HideInInspector] public Vector2 mousePos;

    // Input check bools // pressed = once per input until reset.
    
    [HideInInspector] public bool leftPressed, rightPresseed, upPressed, downPressed;
    [HideInInspector] public bool crouchPressed, inventoryPressed, sprintHold, interactDown, interactUp;
    [HideInInspector] public bool interact, altClick, cycleEquiped, showGameControllerInfo;


    // Input Axis (Project Settings)
    
    [HideInInspector] public Vector2 inputVector;
    private float horizontalInput;
    private float verticalInput;

    // Input Settings
    
    [HideInInspector] public bool crouchToggle;
    [HideInInspector] public bool autoWalk;

    // Methods
    public void UpdateButtonInputs()
    {
        // WASD on pressed Bool (only true on first frame pressed)
        leftPressed = Input.GetKeyDown(leftKey);
        rightPresseed = Input.GetKeyDown(rightKey);
        upPressed = Input.GetKeyDown(upKey);
        downPressed = Input.GetKeyDown(downKey);

        // Keyboard pressed
        crouchPressed = Input.GetKeyDown(KeyCode.LeftControl);
        inventoryPressed = Input.GetKeyDown(KeyCode.Tab);
        sprintHold = Input.GetKey(KeyCode.LeftShift);
        cycleEquiped = Input.GetKeyDown(KeyCode.Q);
        showGameControllerInfo = Input.GetKey(KeyCode.X);
        
        interact = Input.GetKey(KeyCode.E);
        interactDown = Input.GetKeyDown(KeyCode.E);
        interactUp = Input.GetKeyUp(KeyCode.E);

        // Mouse clicks 
        altClick = Input.GetMouseButton(1);
    }
    public void UpdateInputAxis()
    {
        // WASD Inputs (Used for movement Velocity)
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(horizontalInput, verticalInput);

        // Mouse Inputs
        mousePos = Input.mousePosition;
    }

    private void Awake()
    {
        /*
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;
        upKey = KeyCode.W;
        downKey = KeyCode.S;
        crouchKey = KeyCode.C;
        backKey = KeyCode.Escape;
        checkInventory = KeyCode.E;
        quickMenu = KeyCode.Space;
        crouchKey = KeyCode.C;
        */

        crouchToggle = false;
        autoWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check inputs per frame
        UpdateInputAxis();
        UpdateButtonInputs();
    }
}
