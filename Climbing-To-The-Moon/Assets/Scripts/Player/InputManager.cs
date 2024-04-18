using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputControls _input;
    private JumpController _jumpController;

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
        _input.Controls.LeftHand.performed += ctx => StartLeftHand(ctx);
        _input.Controls.LeftHand.canceled += ctx => EndLeftHand(ctx);
        _input.Controls.RightHand.performed += ctx => StartRightHand(ctx);
        _input.Controls.RightHand.canceled += ctx => EndRightHand(ctx);
    }

    private void StartRightHand(InputAction.CallbackContext context)
    {
        _jumpController.StartRightAction();
        Debug.Log("Pulso la E");
    }

    private void EndRightHand(InputAction.CallbackContext context)
    {
        _jumpController.EndRightAction();

    }

    private void StartLeftHand(InputAction.CallbackContext context)
    {
        _jumpController.StartLeftAction();
        Debug.Log("Pulso la Q");
    }

    private void EndLeftHand(InputAction.CallbackContext context)
    {
        _jumpController.EndLeftAction();
    }
}