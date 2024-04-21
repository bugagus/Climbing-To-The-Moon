using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina {get; set;}
    public float maxStamina = 100f;
    public Slider slider;

    void Start()
    {
        slider.value = maxStamina;
    }


    void Update()
    {

        if (stamina <= 0f)
        {
            //Poner que no se mueva
        }

        slider.value = stamina;
    }

    public void discharge(float increaseStamina)
    {
        if (stamina > 0)
        {
            stamina -= increaseStamina * Time.deltaTime;
        }
    }

    public void recharge(float decreaseStamina)
    {
        if (stamina <= maxStamina)
        {
            stamina += decreaseStamina * Time.deltaTime;
        }
    }
}
