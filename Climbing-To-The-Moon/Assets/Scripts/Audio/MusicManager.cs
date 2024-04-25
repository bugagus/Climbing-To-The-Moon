using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs; // Array de canciones
    [SerializeField, Range(0f, 40f)] private float _bg1Height, _bg2Height, _bg3Height, _bg4Height;
    [SerializeField] private AudioSource audioSource; // AudioSource para reproducir las canciones
    [SerializeField, Range(0f, 10f)] private float fadeDuration; // Duración del fade entre canciones
    [SerializeField, Range(0f, 1f)] private float maxVolume = 0.8f; // Volumen máximo durante el fade
    public Animator animatorVolume;
    private GameObject _beetle;

    private void Awake()
    {
        _beetle = GameObject.FindGameObjectWithTag("Player");
        audioSource.Play();
    }

    private void Update()
    {
        if ((_beetle.transform.position.y >= _bg1Height && _beetle.transform.position.y < _bg2Height) || (_beetle.transform.position.y < _bg1Height))
        {
            animatorVolume.SetBool("BajarVolumen", false);
            Debug.Log("111");
        }
        else
        {
            // Desactivar el animador del fondo 1 si el personaje está fuera de su área
            animatorVolume.SetBool("BajarVolumen", true);
        }

        // Activar el animador del fondo 2 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg2Height && _beetle.transform.position.y < _bg3Height) || (_beetle.transform.position.y < _bg2Height))
        {
            animatorVolume.SetBool("BajarVolumen", false);
            Debug.Log("222");
        }
        else
        {
            // Desactivar el animador del fondo 2 si el personaje está fuera de su área
            animatorVolume.SetBool("BajarVolumen", true);
        }

        // Activar el animador del fondo 3 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg3Height && _beetle.transform.position.y < _bg4Height) || (_beetle.transform.position.y < _bg3Height))
        {
            animatorVolume.SetBool("BajarVolumen", false);
            Debug.Log("333");
        }
        else
        {
            // Desactivar el animador del fondo 3 si el personaje está fuera de su área
            animatorVolume.SetBool("BajarVolumen", true);
        }

        // Activar el animador del fondo 4 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg4Height) || (_beetle.transform.position.y < _bg4Height))
        {
            animatorVolume.SetBool("BajarVolumen", false);
            Debug.Log("444");
        }
        else
        {
            // Desactivar el animador del fondo 4 si el personaje está fuera de su área
            animatorVolume.SetBool("BajarVolumen", true);
        }

        if(audioSource.volume == 0)
        {
            if ((_beetle.transform.position.y >= _bg1Height && _beetle.transform.position.y < _bg2Height) || (_beetle.transform.position.y < _bg1Height))
        {
            audioSource.Stop();
            audioSource.clip = songs[1];
            audioSource.Play();
        }
        

        // Activar el animador del fondo 2 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg2Height && _beetle.transform.position.y < _bg3Height) || (_beetle.transform.position.y < _bg2Height))
        {
            audioSource.Stop();
            audioSource.clip = songs[2];
            audioSource.Play();
        }
        

        // Activar el animador del fondo 3 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg3Height && _beetle.transform.position.y < _bg4Height) || (_beetle.transform.position.y < _bg3Height))
        {
            audioSource.Stop();
            audioSource.clip = songs[3];
            audioSource.Play();
        }
        
        // Activar el animador del fondo 4 si el personaje está dentro de su área
        if ((_beetle.transform.position.y >= _bg4Height) || (_beetle.transform.position.y < _bg4Height))
        {
            audioSource.Stop();
            audioSource.clip = songs[4];
            audioSource.Play();
        }
        
        }

    }


}
