using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float rotationSpeed, verticalJumpForce, horizontalJumpForce, extraImpulseGrowth, maxFallSpeed, maxManouverSpeed;
    [SerializeField, Range(0f, 10f)] private float _horizontalForce;
    [SerializeField, Range(0f, 10f)] private float _maxForce;
    [SerializeField, Range(0f, 10f)] private float _chargedJump;

    private float _baseVerticalJumpForce, _baseHorizontalJumpForce;
    [SerializeField]private float chargedJumpGraceTime;
    private float _originalChargedJumpGraceTime;
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _comesFromBothGrab = false;
    private Vector3 _finalHorizontalDirection, _rightHorizontalDirection, _leftHorizontalDirection;
    private bool flag = false;
    private SpriteRenderer _spriteRenderer;

    [Header("Hands")]
    [SerializeField] private Hand leftHand;
    [SerializeField] private Hand rightHand;

    [Header("GroundChecks")]
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Transform floorCheck, parentTransform;
    [SerializeField] private GameObject[] objectsDisabilitedOnGround;
    private bool _grounded;


    [Header("Jetpack")]
    [SerializeField] private GameObject[] objectsDisabilitedOnJetpack;
    [SerializeField, Range(0f, 10f)] private float jetpackVerticalForce, jetpackHorizontalForce;
    private bool _isOnJetpack;

    [Header("StaminaBar")]
    private StaminaBar staminaBar;
    [SerializeField] public float incrStaminaInGround, decrsStamina;

    [Header("SoundController")]
    [SerializeField] private SoundController _soundController;


    [SerializeField] private GameObject jetpack;

    void Awake()
    {
        staminaBar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBar>();
    }


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalChargedJumpGraceTime = chargedJumpGraceTime;
        _rb = GetComponent<Rigidbody2D>();
        _baseHorizontalJumpForce = horizontalJumpForce;
        _baseVerticalJumpForce = verticalJumpForce;
        _animator = GetComponent<Animator>();
        _finalHorizontalDirection = Vector3.zero;
        StopMovement();
    }

    void Update()
    {
        if(chargedJumpGraceTime > 0)
        {
            chargedJumpGraceTime -= Time.deltaTime;
            if(chargedJumpGraceTime < 0)
            {
                verticalJumpForce = _baseVerticalJumpForce;
                horizontalJumpForce = _baseHorizontalJumpForce;
            }
        }

        if (rightHand.IsHandGrabbed == true && leftHand.IsHandGrabbed == true)
        {
            chargedJumpGraceTime = _originalChargedJumpGraceTime;
        }
    }

    void FixedUpdate()
    {
        if (!_animator.GetBool("GrabBothCharged"))
            _soundController.setIsPlaying(false);
        GroundCheck();
        if (!_grounded)
        {
            if (staminaBar.stamina > 0)
            {
                if (rightHand.IsHandGrabbed == true && leftHand.IsHandGrabbed == false)
                {
                    Rotate(rightHand);
                    _animator.SetBool("GrabLeft", false);
                    _animator.SetBool("GrabBothCharged", false);
                    _animator.SetBool("GrabBoth", false);
                }
                else if (rightHand.IsHandGrabbed == false && leftHand.IsHandGrabbed == true)
                {
                    Rotate(leftHand);
                    _animator.SetBool("GrabRight", false);
                    _animator.SetBool("GrabBothCharged", false);
                    _animator.SetBool("GrabBoth", false);

                }
                else if (rightHand.IsHandGrabbed == true && leftHand.IsHandGrabbed == true)
                {
                    _animator.SetBool("GrabBoth", true);
                    _rb.velocity = new Vector3(0f, 0f, 0f);
                    _rb.gravityScale = 0f;
                    if (verticalJumpForce < _maxForce) verticalJumpForce = verticalJumpForce + extraImpulseGrowth * Time.deltaTime;
                    if (horizontalJumpForce < _maxForce) horizontalJumpForce = horizontalJumpForce + extraImpulseGrowth * Time.deltaTime;

                    if (verticalJumpForce >= _chargedJump || horizontalJumpForce >= _chargedJump)
                    {
                        _animator.SetBool("GrabBothCharged", true);
                        _soundController.FullCharged();
                        _soundController.setIsPlaying(true);
                        gameObject.transform.parent.DOShakePosition(0.01f, 0.01f, 1, 30, false, true, ShakeRandomnessMode.Harmonic);
                    }
                }
                else if (rightHand.IsHandGrabbed == false && leftHand.IsHandGrabbed == false)
                {
                    AirControl();
                    ActiveGravity();
                    _animator.SetBool("GrabRight", false);
                    _animator.SetBool("GrabLeft", false);
                    _animator.SetBool("GrabBoth", false);
                    _animator.SetBool("GrabBothCharged", false);
                    if (_isOnJetpack)
                        _rb.AddForce(jetpackVerticalForce * transform.up, ForceMode2D.Force);
                }
            }
            else
            {
                _animator.SetBool("GrabRight", false);
                _animator.SetBool("GrabLeft", false);
                _animator.SetBool("GrabBoth", false);
                AirControl();
                ActiveGravity();
            }
        }
        else
        {
            staminaBar.recharge(incrStaminaInGround);
        }
        VelocityConstraints();
    }

    public void Grab(Hand hand)
    {
        if (!_grounded && staminaBar.stamina > 0)
        {
            if (hand == leftHand)
            {
                _animator.SetBool("GrabLeft", true);
                _soundController.Grab();
            }
            else if (hand == rightHand)
            {
                _animator.SetBool("GrabRight", true);
                _soundController.Grab();
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
        if (releasedHand == rightHand)
        {
            otherHand = leftHand;
        }
        else
        {
            otherHand = rightHand;
        }
        releasedHand.IsHandGrabbed = false;

        if (otherHand.IsHandGrabbed == false)
        {
            if (releasedHand.touchedGrabbableObject != null)
                releasedHand.touchedGrabbableObject.IsBeingGrabbed = false;
            Vector2 direction = transform.up;
            direction.y = direction.y * verticalJumpForce;
            direction.x = direction.x * horizontalJumpForce;
            _rb.AddForce(direction, ForceMode2D.Impulse);
            horizontalJumpForce = _baseHorizontalJumpForce;
            verticalJumpForce = _baseVerticalJumpForce;
        }
        else
        {
            if (otherHand.touchedGrabbableObject != releasedHand.touchedGrabbableObject)
                if (releasedHand.touchedGrabbableObject != null)
                    releasedHand.touchedGrabbableObject.IsBeingGrabbed = false;
        }

        if (releasedHand == leftHand)
        {
            _animator.SetBool("GrabLeft", false);
        }
        else if (releasedHand == rightHand)
        {
            _animator.SetBool("GrabRight", false);
        }
        _animator.SetBool("GrabBoth", false);

    }

    public void AirControl()
    {
        if ((_finalHorizontalDirection == Vector3.right && _rb.velocity.x < maxManouverSpeed) || (_finalHorizontalDirection == Vector3.left && _rb.velocity.x > -maxManouverSpeed))
        {
            if (!_isOnJetpack)
            {
                _rb.AddForce(_finalHorizontalDirection * _horizontalForce, ForceMode2D.Force);
            }
            else
            {
                _rb.AddForce(_finalHorizontalDirection * _horizontalForce * jetpackHorizontalForce, ForceMode2D.Force);
            }
        }
    }

    public void ActiveGravity()
    {
        _rb.gravityScale = 1f;
    }

    public void Rotate(Hand hand)
    {
        _rb.velocity = new Vector3(0f, 0f, 0f);
        _rb.totalForce = Vector3.zero;
        _rb.totalTorque = 0f;
        _rb.gravityScale = 0f;
        if (hand == rightHand)
        {
            transform.RotateAround(hand.transform.position, -Vector3.forward, rotationSpeed);
        }
        else if (hand == leftHand)
        {
            transform.RotateAround(hand.transform.position, Vector3.forward, rotationSpeed);
        }
    }

    public void Jump(float angle)
    {
        transform.parent = parentTransform;
        if (_grounded)
        {
            Vector2 upVector = transform.up;
            float angleInRadians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(
                Mathf.Cos(angleInRadians) * upVector.x - Mathf.Sin(angleInRadians) * upVector.y,
                Mathf.Sin(angleInRadians) * upVector.x + Mathf.Cos(angleInRadians) * upVector.y
            );
            direction.Normalize();
            direction.y = direction.y * verticalJumpForce * 1.5f;
            direction.x = direction.x * horizontalJumpForce * 0.75f;
            _rb.AddForce(direction * 50, ForceMode2D.Force);
            horizontalJumpForce = _baseHorizontalJumpForce;
            verticalJumpForce = _baseVerticalJumpForce;
        }
    }

    public void Move()
    {
        if (!_grounded)
        {
            _finalHorizontalDirection = _rightHorizontalDirection + _leftHorizontalDirection;
        }
        else
        {
            _animator.SetBool("GroundCharging", true);
        }
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
                _animator.SetBool("OnAir", false);
                if (_rb.velocity.x < 0)
                {
                    flag = true;
                    _spriteRenderer.flipX = true; // Flip the sprite
                }
                else if (_rb.velocity.x > 0)
                {
                    _spriteRenderer.flipX = false; // Do not flip the sprite
                }
                _soundController.HitFloor();
                _finalHorizontalDirection = Vector3.zero;
                _rb.velocity = Vector2.zero;
                foreach (GameObject obj in objectsDisabilitedOnGround)
                {
                    obj.SetActive(false);
                }
                transform.rotation = Quaternion.identity; // Rotar a la rotación inicial (0,0,0)
            }
            else if (_lastGrounded && !_grounded)
            {
                if (flag)
                {
                    _spriteRenderer.flipX = false;
                }
                _animator.SetBool("OnAir", true);
                _animator.SetBool("GroundCharging", false);
                _soundController.Jump();
                foreach (GameObject obj in objectsDisabilitedOnGround)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    public void StartRightAction()
    {
        if (rightHand.IsHandColliding == false)
        {
            _rightHorizontalDirection = Vector3.right;
            Debug.Log("Me muevo");
            Move();
        }
        else
        {
            Grab(rightHand);
        }
    }

    public void StartLeftAction()
    {
        if (leftHand.IsHandColliding == false)
        {
            _leftHorizontalDirection = Vector3.left;
            Move();
        }
        else
        {
            Grab(leftHand);
        }
    }

    public void EndRightAction()
    {
        if (rightHand.IsHandGrabbed == true)
            Release(rightHand);
        else
            Jump(-60);
        _rightHorizontalDirection = Vector3.zero;
        Move();
    }

    public void EndLeftAction()
    {
        if (leftHand.IsHandGrabbed == true)
            Release(leftHand);
        else
            Jump(60);
        _leftHorizontalDirection = Vector3.zero;
        Move();
    }
    public void StartJetpack()
    {
        jetpack.SetActive(true);
        foreach (GameObject obj in objectsDisabilitedOnJetpack)
        {
            obj.SetActive(false);
        }
        _isOnJetpack = true;
    }

    public void EndJetpack()
    {
        jetpack.SetActive(false);
        foreach (GameObject obj in objectsDisabilitedOnJetpack)
        {
            obj.SetActive(true);
        }
        _isOnJetpack = false;
    }

    private void VelocityConstraints()
    {
        if(_rb.velocity.y < -maxFallSpeed && !leftHand.IsHandGrabbed && !rightHand.IsHandGrabbed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -maxFallSpeed);
        }
    }

    public void ContinueMovement()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void StopMovement()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }

    public void ReleaseBoth()
    {
        EndRightAction();
        EndLeftAction();
    }
}

