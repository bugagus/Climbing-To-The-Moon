using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    
    float stamina = 100f;
    float maxStamina = 100f;
    public Slider slider;

    void Start()
    {
        slider.value = maxStamina;
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            //Perder stamina
            if(stamina > 0)
            {
                stamina -= 10f * Time.deltaTime;
            }
        }
        else if(!Input.GetKey(KeyCode.Q) || !Input.GetKey(KeyCode.E))
        {
            //Recuperar stamina
            if(stamina <= maxStamina)
            {
               stamina += 10f * Time.deltaTime;
            }
        }
        
        if(stamina <= 0f)
        {
            //Poner que no se mueva
        }

        slider.value = stamina;
    }
}
