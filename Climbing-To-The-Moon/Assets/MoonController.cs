using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("SoundController")]
    [SerializeField] private SoundController _soundController;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private float powerUpAnimationDuration;
    public void ShutDown()
    {
        animator.SetTrigger("ShutDown");
        DOVirtual.DelayedCall(2.5f,() =>_soundController.MoonLight());
        
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Player"))
        {
            Debug.Log("Acabo de colisionar");
            collider2D.gameObject.SetActive(false);
            FindObjectOfType<CinemachineVirtualCamera>().Follow = this.transform;
            animator.SetTrigger("PowerUp");
            DOVirtual.DelayedCall(powerUpAnimationDuration, ()=> endScreen.SetActive(true));
        }
    }
}
