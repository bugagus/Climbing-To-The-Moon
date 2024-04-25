using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GrabbableObject))]
public class MovingGrabbableObject : MovingObject
{
    private GrabbableObject _grabbableObject;
    [SerializeField] private Transform player;
    private Transform _previousParent;
    private bool _previousState;
    private void Awake()
    {
        player = FindObjectOfType<JumpController>().transform;
        _grabbableObject = GetComponent<GrabbableObject>();
        _previousParent = player.parent;
    }

    private void Update()
    {
        if (_grabbableObject.IsBeingGrabbed && !_previousState)
            StartGrab();
        else if(!_grabbableObject.IsBeingGrabbed && _previousState)
            EndGrab();
        _previousState = _grabbableObject.IsBeingGrabbed;
    }

    private void StartGrab()
    {
        player.parent = transform;
    }

    private void EndGrab()
    {
        player.parent = _previousParent;
    }
}
