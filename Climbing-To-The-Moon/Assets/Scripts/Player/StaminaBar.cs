using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina {get; set;}
    [SerializeField, Range(0f, 100f)] public float maxStamina;
    public Slider slider;

    void Start()
    {
        slider.value = maxStamina;
        stamina = maxStamina;  
    }


    void Update()
    {
        slider.value = stamina;
    }

    public void discharge(float decreaseStamina)
    {
        if (stamina > 0)
        {
            stamina -= decreaseStamina * Time.deltaTime;
        }
    }

    public void recharge(float increaseStamina)
    {
        if (stamina <= maxStamina)
        {
            stamina += increaseStamina * Time.deltaTime;
        }
    }
    
    public void dischargeNoTime(float decreaseStamina)
    {
        if (stamina <= maxStamina)
        {
            stamina -= decreaseStamina;
            if (stamina < 0f)
                stamina = 0f;
        }

    }
}
