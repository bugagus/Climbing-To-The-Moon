using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ElectricObject : MonoBehaviour
{
    [SerializeField] public bool IsElectrifying { get; set; }
    [SerializeField, Range(0f, 10f)] protected float _timeBetweenElectricity;
    [SerializeField, Range(0f, 10f)] protected float _stunTime;


    private Animator _animator;
    private StaminaBar _bar;
    private GrabbableObject _grabbableObject;
    private JumpController _jumpController;

    protected virtual void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _bar = FindAnyObjectByType<StaminaBar>();
        _grabbableObject = GetComponentInParent<GrabbableObject>();
        _jumpController = GameObject.FindGameObjectWithTag("Player").GetComponent<JumpController>();
        if (_timeBetweenElectricity == 0)
        {
            _animator.SetBool("Electrifying", true);
            IsElectrifying = true;
        }
        else
            StartCoroutine(ElectricLoop());
    }

    private IEnumerator ElectricLoop()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(_timeBetweenElectricity);
            IsElectrifying =! IsElectrifying;
            if (IsElectrifying)
                _animator.SetBool("Electrifying", true);
            else
                _animator.SetBool("Electrifying", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Player" || tag == "LeftHand" || tag == "RightHand")
        {
            _bar.dischargeNoTime(100);
            _grabbableObject.IsBeingGrabbed = false;
            StartCoroutine(StopPlayer());
        }
    }

    private IEnumerator StopPlayer()
    {
        _jumpController.StopMovement();
        yield return new WaitForSeconds(_stunTime);
        _jumpController.ContinueMovement();
    }
}
