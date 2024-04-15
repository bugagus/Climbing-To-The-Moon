using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private InputControls _input;
    private JumpController _jumpController;
    public bool IsLeftHandColliding {  get; set; }
    public bool IsRightHandColliding {  get; set; }

    private void Awake()
    {
        _input = new InputControls();
        _jumpController = gameObject.GetComponent<JumpController>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDestroy()
    {
        _input.Disable();
    }

    private void Start()
    {
        _input.Controls.LeftHand.started += ctx => StartLeftHand(ctx);   
        _input.Controls.LeftHand.canceled += ctx => EndLeftHand(ctx);
        _input.Controls.RightHand.started += ctx => StartRightHand(ctx);
        _input.Controls.RightHand.canceled += ctx => EndRightHand(ctx);
    }

    private void StartRightHand(InputAction.CallbackContext context)
    {
        if (IsRightHandColliding)
            _jumpController.GrabRight();
        Debug.Log("Pulso la E");
    }

    private void EndRightHand(InputAction.CallbackContext context)
    {
        _jumpController.ReleaseRight();
    }

    private void StartLeftHand(InputAction.CallbackContext context)
    {
        if (IsLeftHandColliding)
            _jumpController.GrabLeft();
        Debug.Log("Pulso la Q");
    }

    private void EndLeftHand(InputAction.CallbackContext context)
    {
        _jumpController.ReleaseLeft();
    }
}