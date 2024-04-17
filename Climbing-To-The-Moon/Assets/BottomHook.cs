using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomHook : MonoBehaviour
{
    [SerializeField] Transform upperHook;
    private float _distanceUpper;

    void Start()
    {
        _distanceUpper = upperHook.position.y - transform.position.y;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(upperHook.position.x, upperHook.position.y - _distanceUpper, upperHook.position.z);
    }
}
