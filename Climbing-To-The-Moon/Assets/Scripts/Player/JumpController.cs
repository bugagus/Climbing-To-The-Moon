using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private Transform leftHand, rightHand;
    [SerializeField, Range(0f, 10f)] private float rotationSpeed, jumpForce, extraImpulseGrowth, _maxSpeed;
    [SerializeField, Range(0f, 10f)] private float _horizontalForce;
    private float _baseJumpForce;
    private Rigidbody2D _rb;
    private Animator _animator;
    bool _grabbedRight, _grabbedLeft;
    private Vector3 _horizontalDirection;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _grabbedRight = false;
        _grabbedLeft = false;
        _baseJumpForce = jumpForce;
        _animator = GetComponent<Animator>();
        _horizontalDirection = Vector3.zero;
    }

    void FixedUpdate()
    {
        if(_grabbedRight && !_grabbedLeft)
        {
            ResetHorizontalDir();
            _rb.velocity = new Vector3(0f, 0f, 0f);
            _rb.gravityScale = 0f;
            Vector3 _relativePosition = transform.position - rightHand.position;
            transform.RotateAround(rightHand.position, -Vector3.forward, rotationSpeed);
            Debug.Log("Giro en torno a la derecha");
        }
        else if(!_grabbedRight && _grabbedLeft)
        {
            ResetHorizontalDir();
            _rb.velocity = new Vector3(0f, 0f, 0f);
            _rb.gravityScale = 0f;
            Vector3 _relativePosition = transform.position - leftHand.position;
            transform.RotateAround(leftHand.position, Vector3.forward, rotationSpeed);
            Debug.Log("Giro en torno a la izquierda");
        }
        else if(_grabbedRight && _grabbedLeft)
        {
            ResetHorizontalDir();
            _animator.SetBool("GrabBoth", true);
            _rb.velocity = new Vector3(0f, 0f, 0f);
            _rb.gravityScale = 0f;
            if(jumpForce < 10) jumpForce = jumpForce + extraImpulseGrowth * Time.deltaTime;
        }
        else if(!_grabbedRight && !_grabbedLeft)
        {
            _rb.gravityScale = 1f;
            if (_horizontalDirection != Vector3.zero)
                _rb.AddForce(_horizontalDirection * _horizontalForce, ForceMode2D.Force);
        }

        if (_rb.velocity.y < -_maxSpeed)
            _rb.velocity = new Vector3(_rb.velocity.x, -_maxSpeed, 0f);
        

        if (_rb.velocity.x > _maxSpeed)
            _rb.velocity = new Vector3(_maxSpeed, _rb.velocity.y, 0f);
        else if (_rb.velocity.x < -_maxSpeed)
            _rb.velocity = new Vector3(-_maxSpeed, _rb.velocity.y, 0f);

    }

    public void GrabRight()
    {
        _animator.SetBool("GrabRight", true);
        _grabbedRight = true;
    }

    public void GrabLeft()
    {
        _animator.SetBool("GrabLeft", true);
        _grabbedLeft = true;
    }

    public void ReleaseRight()
    {
        _grabbedRight = false;
        if(!_grabbedLeft) 
        {
            _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpForce = _baseJumpForce;
        }
        _animator.SetBool("GrabRight", false);
        _animator.SetBool("GrabBoth", false);
    }

    public void ReleaseLeft()
    {
        _grabbedLeft = false;
        if(!_grabbedRight) 
        {
            _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpForce = _baseJumpForce;
        }
        _animator.SetBool("GrabLeft", false);
        _animator.SetBool("GrabBoth", false);
    }

    public void MoveRight()
    {
        _horizontalDirection = Vector3.right;
    }

    public void MoveLeft()
    {
        _horizontalDirection = Vector3.left;
    }

    public void ResetHorizontalDir()
    {
        _horizontalDirection = Vector3.zero;
    }

}

