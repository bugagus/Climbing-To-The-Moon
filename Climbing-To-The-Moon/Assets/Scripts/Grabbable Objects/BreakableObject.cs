using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BreakableObject : MonoBehaviour
{
    [Header("HealthObject")]
    [SerializeField, Range(0.0f, 100.0f)] protected float _health;
    [SerializeField, Range(1f, 100.0f)] protected float _decreaseHealth;
    [SerializeField, Range(1f, 100.0f)] protected float _increaseHealth;

    protected bool _isDestroyed;

    protected virtual void Awake()
    {
        _isDestroyed = false;
    }

    protected abstract void Update();

    protected abstract void RepairObject();


    protected abstract void DestroyObject();

}
