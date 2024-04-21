using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina = 100f;
    public float maxStamina = 100f;
    [SerializeField] float increaseStamina;
    [SerializeField] float decreaseStamina;
    public Slider slider;

    void Start()
    {
        slider.value = maxStamina;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            //Perder stamina
            discharge();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            //Recuperar stamina
            recharge();
        }

        if (stamina <= 0f)
        {
            //Poner que no se mueva
        }

        slider.value = stamina;
    }

    public void discharge()
    {
        if (stamina > 0)
        {
            stamina -= increaseStamina * Time.deltaTime;
        }
    }

    public void recharge()
    {
        if (stamina <= maxStamina)
        {
            stamina += decreaseStamina * Time.deltaTime;
        }
    }
}
