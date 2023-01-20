using System;
using System.Dynamic;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = System.Numerics.Vector3;

public class HumanoidLandInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool SprintInput { get; private set; }
    public bool MouseLeft { get; private set; }
    
    public bool InvertMouseY { get; private set; } = true;
    public bool ChangeCameraWasPressThisFrame { get; private set; } = true;
    
    private bool MovePressed;


    private InputActions _input;


    private void OnEnable()
    {
        _input = new InputActions();
        _input.HumanoidLand.Enable();

        _input.HumanoidLand.Move.performed += SetMove;
        _input.HumanoidLand.Move.canceled += SetMove;
        
        _input.HumanoidLand.Look.performed += SetLook;
        _input.HumanoidLand.Look.canceled += SetLook;

        _input.HumanoidLand.Sprint.performed += SetSprint;
        _input.HumanoidLand.Sprint.canceled += SetSprint;

        _input.HumanoidLand.MouseLeft.performed += SetLeftMouse;
        _input.HumanoidLand.MouseLeft.canceled += SetLeftMouse;



    }

    private void OnDisable()
    {
        
        _input.HumanoidLand.Move.performed -= SetMove;
        _input.HumanoidLand.Move.canceled -= SetMove;
        
        _input.HumanoidLand.Look.performed -= SetLook;
        _input.HumanoidLand.Look.canceled -= SetLook;
        
        _input.HumanoidLand.Sprint.performed -= SetSprint;
        _input.HumanoidLand.Sprint.canceled -= SetSprint;
        
        _input.HumanoidLand.MouseLeft.performed -= SetLeftMouse;
        _input.HumanoidLand.MouseLeft.canceled -= SetLeftMouse;
        
        _input.HumanoidLand.Disable();
    }

    private void SetMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        MovePressed = !(MoveInput == Vector2.zero);
    }
    
    private void SetLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
    
    private void SetSprint(InputAction.CallbackContext context)
    {
        SprintInput = context.ReadValue<float>() > 0;
    }
    
    private void SetLeftMouse(InputAction.CallbackContext context)
    {
       MouseLeft = context.ReadValue<float>() > 0;
    }

    private void Update()
    {
        ChangeCameraWasPressThisFrame = _input.HumanoidLand.ChangeCamera.WasPressedThisFrame();
    }
}
