using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GrabbableObject : MonoBehaviour
{
    public bool IsBeingGrabbed { get; set; }

    private StaminaBar _staminaBar;
    private Animator _animator;

    [SerializeField, Range(0, 100)] private int _discrargeStamina;

    private void Start()
    {
        _staminaBar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBar>();
        _animator = GetComponent<Animator>();
        IsBeingGrabbed = false;
    }

    private void Update()
    {
        if (IsBeingGrabbed)
        {
            _staminaBar.discharge(_discrargeStamina);
            _animator.SetBool("Grabbed", true);
        }
        else
            _animator.SetBool("Grabbed", false);

        if (_staminaBar.stamina <= 0)
        {
            IsBeingGrabbed = false;

        }
    }
}