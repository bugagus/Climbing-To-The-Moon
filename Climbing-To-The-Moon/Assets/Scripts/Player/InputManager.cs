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
    private bool _isHoldingRight;
    private bool _isHoldingLeft;

    private void Awake()
    {
        _isHoldingRight = false;
        _isHoldingLeft = false;
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
        if (IsRightHandColliding)
        {
            _jumpController.GrabRight();
            _isHoldingRight = true;
        }
        else if (!_isHoldingLeft)
            _jumpController.MoveRight();
        Debug.Log("Pulso la E");
    }

    private void EndRightHand(InputAction.CallbackContext context)
    {
        if(_isHoldingRight)
        {
            _jumpController.ReleaseRight();
            _isHoldingRight = false;
        }
        _jumpController.ResetHorizontalDir();
    }

    private void StartLeftHand(InputAction.CallbackContext context)
    {
        if (IsLeftHandColliding)
        {
            _jumpController.GrabLeft();
            _isHoldingLeft = true;
        }
        else if (!_isHoldingRight)
            _jumpController.MoveLeft();
        Debug.Log("Pulso la Q");
    }

    private void EndLeftHand(InputAction.CallbackContext context)
    {
        if(_isHoldingLeft)
        {
            _jumpController.ReleaseLeft();
            _isHoldingLeft = false;
        }
        _jumpController.ResetHorizontalDir();
    }
}