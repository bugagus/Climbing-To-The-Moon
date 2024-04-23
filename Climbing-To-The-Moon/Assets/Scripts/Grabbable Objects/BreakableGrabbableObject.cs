using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableGrabbableObject : BreakableObject
{
    private GrabbableObject _grabbableObject;
    private Animator _animator;

    [Header("Shaker")]
    [SerializeField] private DOTween _doTween;
    protected override void Awake()
    {
        base.Awake();
        _isDestroyed = false;
        _grabbableObject = GetComponentInParent<GrabbableObject>();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        _animator.SetFloat("Health", _health);
        if (_grabbableObject.IsBeingGrabbed)
            DestroyObject();
        if (_isDestroyed)
            RepairObject();
    }

    protected override void RepairObject()
    {
        _health += _decreaseHealth * Time.deltaTime;
        _animator.SetFloat("Health", _health);
        if (_health >= 100)
        {
            _isDestroyed = false;
            gameObject.GetComponentInParent<Collider2D>().enabled = true;
        }
    }

    protected override void DestroyObject()
    {
        _health -= _decreaseHealth * Time.deltaTime;
        gameObject.transform.parent.DOShakePosition(0.005f, 0.01f, 1, 30, false, true, ShakeRandomnessMode.Harmonic);
        if (_health <= 0)
        {
            _isDestroyed = true;
            _grabbableObject.IsBeingGrabbed = false;
            gameObject.GetComponentInParent<Collider2D>().enabled = false;
        }
    }

    

}


