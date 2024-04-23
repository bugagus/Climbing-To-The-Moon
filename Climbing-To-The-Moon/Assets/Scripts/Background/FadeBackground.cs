using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBackground : MonoBehaviour
{
    public Animator animatorBg1,animatorBg2,animatorBg3,animatorBg4;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (animatorBg1.GetBool("Background"))
            {
                animatorBg1.SetBool("Background", false);
            }
            else
            {
                animatorBg1.SetBool("Background", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (animatorBg2.GetBool("Background"))
            {
                animatorBg2.SetBool("Background", false);
            }
            else
            {
                animatorBg2.SetBool("Background", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (animatorBg3.GetBool("Background"))
            {
                animatorBg3.SetBool("Background", false);
            }
            else
            {
                animatorBg3.SetBool("Background", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (animatorBg4.GetBool("Background"))
            {
                animatorBg4.SetBool("Background", false);
            }
            else
            {
                animatorBg4.SetBool("Background", true);
            }
        }
    }


}

