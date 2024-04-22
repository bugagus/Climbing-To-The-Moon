using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricObject : MonoBehaviour
{
    [SerializeField] public bool IsElectrifying { get; set; }
    [SerializeField, Range(0f, 10f)] protected float _timeBetweenElectricity;

    protected virtual void OnEnable()
    {
        if (_timeBetweenElectricity == 0)
            IsElectrifying = true;
        else
            StartCoroutine(ElectricLoop());
    }

    private IEnumerator ElectricLoop()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(_timeBetweenElectricity);
            IsElectrifying =! IsElectrifying;
        }
    }
}
