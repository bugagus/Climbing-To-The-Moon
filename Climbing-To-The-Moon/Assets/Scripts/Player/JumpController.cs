using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float rotationSpeed, jumpForce, extraImpulseGrowth, _maxSpeed;
    [SerializeField, Range(0f, 10f)] private float _horizontalForce;
    private float _baseJumpForce;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector3 _horizontalDirection;

    [Header("Hands")]
    [SerializeField] private Hand leftHand;
    [SerializeField] private Hand rightHand;

    [Header("GroundChecks")]
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Transform floorCheck;
    [SerializeField] private GameObject[] objectsDisabilitedOnGround;
    private bool _grounded;

    private StaminaBar staminaBar;

    void Awake()
    {
        staminaBar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBar>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _baseJumpForce = jumpForce;
        _animator = GetComponent<Animator>();
        _horizontalDirection = Vector3.zero;
    }

    void FixedUpdate()
    {
        GroundCheck();
        if (!_grounded)
        {
            
            if (rightHand.IsHandGrabbed == true && leftHand.IsHandGrabbed == false)
            {
                Rotate(rightHand);
                staminaBar.discharge();
            }
            else if (rightHand.IsHandGrabbed == false && leftHand.IsHandGrabbed == true)
            {
                Rotate(leftHand);
                staminaBar.discharge();
            }
            else if (rightHand.IsHandGrabbed == true && leftHand.IsHandGrabbed == true)
            {
                ResetHorizontalDir();
                _animator.SetBool("GrabBoth", true);
                _rb.velocity = new Vector3(0f, 0f, 0f);
                _rb.gravityScale = 0f;
                if (jumpForce < 10) jumpForce = jumpForce + extraImpulseGrowth * Time.deltaTime;
                staminaBar.discharge();
            }
            else if (rightHand.IsHandGrabbed == false && leftHand.IsHandGrabbed == false)
            {
                _rb.gravityScale = 1f;
                if (_horizontalDirection != Vector3.zero)
                    _rb.AddForce(_horizontalDirection * _horizontalForce, ForceMode2D.Force);
            }
        }
        VelocityConstraints();

    }

    public void Grab(Hand hand)
    {
        if (!_grounded)
        {
            if(hand == leftHand)
            {
                _animator.SetBool("GrabLeft", true);
            }
            else if(hand == rightHand)
            {
                _animator.SetBool("GrabRight", true);
            }
            hand.IsHandGrabbed = true;
            hand.touchedGrabbableObject.IsBeingGrabbed = true;
        }
        else
        {
            _animator.SetBool("GroundCharging", true);
        }
    }

    public void Release(Hand releasedHand)
    {
        Hand otherHand;
        if(releasedHand == rightHand)
        {
            otherHand = leftHand;
        }else
        {
            otherHand = rightHand;
        }
        releasedHand.IsHandGrabbed = false;

        if (otherHand.IsHandGrabbed == false)
        {
            if (releasedHand.touchedGrabbableObject != null)
                releasedHand.touchedGrabbableObject.IsBeingGrabbed = false;

            _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpForce = _baseJumpForce;
        }
        else
        {
            if (otherHand.touchedGrabbableObject != releasedHand.touchedGrabbableObject)
                if (releasedHand.touchedGrabbableObject != null)
                    releasedHand.touchedGrabbableObject.IsBeingGrabbed = false;
        }

        if(releasedHand == leftHand)
        {
            _animator.SetBool("GrabLeft", false);
        }
        else if(releasedHand == rightHand)
        {
            _animator.SetBool("GrabRight", false);
        }
        _animator.SetBool("GrabBoth", false);

    }

    public void Rotate(Hand hand)
    {
        ResetHorizontalDir();
        _rb.velocity = new Vector3(0f, 0f, 0f);
        _rb.gravityScale = 0f;
        Vector3 _relativePosition = transform.position - hand.transform.position;
        if(hand == rightHand)
        {
            transform.RotateAround(hand.transform.position, -Vector3.forward, rotationSpeed);
        }else if(hand == leftHand)
        {
            transform.RotateAround(hand.transform.position, Vector3.forward, rotationSpeed);
        }
    }

    public void Jump(float angle)
    {
        if (_grounded)
        {
            Vector2 upVector = transform.up;
            float angleInRadians = angle * Mathf.Deg2Rad;
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

    public void Move(Vector3 direction)
    {
        if (!_grounded)
        {
            _horizontalDirection = direction;
        }
        else
        {
            _animator.SetBool("GroundCharging", true);
        }
    }

    public void ResetHorizontalDir()
    {
        _horizontalDirection = Vector3.zero;
    }

    private void GroundCheck()
    {
        if (rightHand.IsHandGrabbed == false && leftHand.IsHandGrabbed == false)
        {
            bool _lastGrounded = _grounded;

            Vector2 boxSize = new Vector2(0.2f, 1.2f);
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

    public void StartRightAction()
    {
        if (rightHand.IsHandColliding == false)
            Move(Vector3.right);
        else
            Grab(rightHand);
    }

    public void StartLeftAction()
    {
        if (leftHand.IsHandColliding == false)
            Move(Vector3.left);
        else
            Grab(leftHand);
    }

    public void EndRightAction()
    {
        if (rightHand.IsHandGrabbed == true)
            Release(rightHand);
        else
            Jump(-60);
        ResetHorizontalDir();
    }

    public void EndLeftAction()
    {
        if (leftHand.IsHandGrabbed == true)
            Release(leftHand);
        else
            Jump(60);
        ResetHorizontalDir();
    }

    public void VelocityConstraints()
    {
        if (_rb.velocity.y < -_maxSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, -_maxSpeed, 0f);
        }
        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector3(_maxSpeed, _rb.velocity.y, 0f);
        }
        else if (_rb.velocity.x < -_maxSpeed)
        {
            _rb.velocity = new Vector3(-_maxSpeed, _rb.velocity.y, 0f);
        }
    }

    public void ContinueMovement()
    {

    }

    public void StopMovement()
    {
        
    }

}

