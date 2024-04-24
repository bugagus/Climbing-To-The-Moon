using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBackground : MonoBehaviour
{
    public Animator animatorBg1, animatorBg2, animatorBg3, animatorBg4;
    private GameObject _beetle;
    [SerializeField, Range(0f, 25f)] private float _bg1Height, _bg2Height, _bg3Height, _bg4Height;


    private void Awake()
    {
        _beetle = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if ((_beetle.transform.position.y >= _bg1Height && _beetle.transform.position.y < _bg2Height) || (_beetle.transform.position.y < _bg1Height))
        {
            animatorBg1.SetBool("Background", true);
        }
        else
        {
            // Desactivar el animador del fondo 1 si el personaje está fuera de su área
            animatorBg1.SetBool("Background", false);
        }

        // Activar el animador del fondo 2 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg2Height && _beetle.transform.position.y < _bg3Height) || (_beetle.transform.position.y < _bg2Height))
        {
            animatorBg2.SetBool("Background", true);
        }
        else
        {
            // Desactivar el animador del fondo 2 si el personaje está fuera de su área
            animatorBg2.SetBool("Background", false);
        }

        // Activar el animador del fondo 3 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg3Height && _beetle.transform.position.y < _bg4Height) || (_beetle.transform.position.y < _bg3Height))
        {
            animatorBg3.SetBool("Background", true);
        }
        else
        {
            // Desactivar el animador del fondo 3 si el personaje está fuera de su área
            animatorBg3.SetBool("Background", false);
        }

        // Activar el animador del fondo 4 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg4Height) || (_beetle.transform.position.y < _bg4Height))
        {
            animatorBg4.SetBool("Background", true);
        }
        else
        {
            // Desactivar el animador del fondo 4 si el personaje está fuera de su área
            animatorBg4.SetBool("Background", false);
        }

    }


}

