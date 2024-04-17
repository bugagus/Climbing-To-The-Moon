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

    [Header("GroundChecks")]
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Transform floorCheck;
    [SerializeField] private GameObject[] objectsDisabilitedOnGround;
    private bool _grounded;

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
        GroundCheck();
        if(!_grounded)
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
        if(!_grounded)
        {
            _animator.SetBool("GrabRight", true);
            _grabbedRight = true;
        }else{
            _animator.SetBool("GroundCharging", true);
        }
    }

    public void GrabLeft()
    {
        if(!_grounded)
        {
            _animator.SetBool("GrabLeft", true);
            _grabbedLeft = true;
        }
    }

    public void ReleaseRight()
    {
        if(!_grounded)
        {
            _grabbedRight = false;
            if(!_grabbedLeft) 
            {
                _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                jumpForce = _baseJumpForce;
            }
            _animator.SetBool("GrabRight", false);
            _animator.SetBool("GrabBoth", false);
        }else
        {
            Vector2 upVector = transform.up;
            float angleInRadians = 45 * Mathf.Deg2Rad;
            Vector2 right45Vector = new Vector2(
                Mathf.Cos(angleInRadians) * upVector.x - Mathf.Sin(angleInRadians) * upVector.y,
                Mathf.Sin(angleInRadians) * upVector.x + Mathf.Cos(angleInRadians) * upVector.y
            );
            right45Vector.Normalize();
            _rb.AddForce(right45Vector * jumpForce, ForceMode2D.Impulse);
            jumpForce = _baseJumpForce;
            _animator.SetBool("GroundCharging", false);
        }
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

    public void JumpRight()
    {
        if(_grounded)
        {
            Vector2 upVector = transform.up;
            float angleInRadians = -60 * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(
                Mathf.Cos(angleInRadians) * upVector.x - Mathf.Sin(angleInRadians) * upVector.y,
                Mathf.Sin(angleInRadians) * upVector.x + Mathf.Cos(angleInRadians) * upVector.y
            );
            direction.Normalize();
            direction.y = direction.y * 1.5f;
            direction.x = direction.x * 0.5f;
            _rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
            jumpForce = _baseJumpForce;
            _animator.SetBool("GroundCharging", false);
        }
    }

    public void JumpLeft()
    {
        if(_grounded)
        {
            Vector2 upVector = transform.up;
            float angleInRadians = 60 * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(
                Mathf.Cos(angleInRadians) * upVector.x - Mathf.Sin(angleInRadians) * upVector.y,
                Mathf.Sin(angleInRadians) * upVector.x + Mathf.Cos(angleInRadians) * upVector.y
            );
            direction.Normalize();
            direction.y = direction.y * 1.5f;
            direction.x = direction.x * 0.5f;
            _rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
            jumpForce = _baseJumpForce;
            _animator.SetBool("GroundCharging", false);
        }
    }

    public void MoveRight()
    {
        if(!_grounded)
        {
            _horizontalDirection = Vector3.right;
        }else{
            _animator.SetBool("GroundCharging", true);
        }
    }

    public void MoveLeft()
    {
        if(!_grounded)
        {
            _horizontalDirection = Vector3.left;
        }else{
            _animator.SetBool("GroundCharging", true);
        }
    }

    public void ResetHorizontalDir()
    {
        _horizontalDirection = Vector3.zero;
    }

    private void GroundCheck()
    {
        bool _lastGrounded = _grounded;

        Vector2 boxSize = new Vector2(0.6f, 1.2f);
        Vector2 boxCenter = transform.TransformPoint(floorCheck.localPosition); // Convertir la posición local a global

        _grounded = Physics2D.OverlapBox(boxCenter, boxSize, 0, floorLayer);

        if (!_lastGrounded && _grounded)
        {
            // Si viene del aire y toca el suelo
            _rb.velocity = Vector2.zero;
            foreach (GameObject obj in objectsDisabilitedOnGround)
            {
                obj.SetActive(false);
            }
            transform.rotation = Quaternion.identity; // Rotar a la rotación inicial (0,0,0)
        }
        else if (_lastGrounded && !_grounded)
        {
            foreach (GameObject obj in objectsDisabilitedOnGround)
            {
                obj.SetActive(true);
            }
        }

        _animator.SetBool("OnAir", !_grounded);
    }

}

