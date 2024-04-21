using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(GrabbableObject))]
public class BreakableGrabbableObject : BreakableObject
{
    private GrabbableObject _grabbableObject;
    protected override void Awake()
    {
        base.Awake();
        _isDestroyed = false;
        _grabbableObject = GetComponent<GrabbableObject>();
    }

    protected override void Update()
    {
        if (_grabbableObject.IsBeingGrabbed)
            DestroyObject();
    }

    protected override void DestroyObject()
    {
        _health -= _decreaseHealth * Time.deltaTime;
        if (_health <= 0)
        {
            _isDestroyed = true;
            _grabbableObject.IsBeingGrabbed = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    

}


