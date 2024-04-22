using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public bool IsBeingGrabbed { get; set; }

    private StaminaBar _staminaBar;

    [SerializeField, Range(0, 100)] private int _discrargeStamina;

    private void Start()
    {
        _staminaBar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBar>();
        IsBeingGrabbed = false;
    }

   private void Update()
   {
       if (IsBeingGrabbed)
           _staminaBar.discharge(_discrargeStamina);
       if (_staminaBar.stamina <= 0)
           IsBeingGrabbed = false;
   }
}