using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("SoundController")]
    [SerializeField] private SoundController _soundController;
    public void ShutDown()
    {
        animator.SetTrigger("ShutDown");
        DOVirtual.DelayedCall(2.5f,() =>_soundController.MoonLight());
        
    }
}
