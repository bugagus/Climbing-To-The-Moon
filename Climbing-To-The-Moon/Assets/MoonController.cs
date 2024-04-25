using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void ShutDown()
    {
        animator.SetTrigger("ShutDown");
    }
}
