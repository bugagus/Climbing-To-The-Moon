using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private InputManager _inputManager;
    public bool IsHandColliding { get; set; }
    public bool IsHandGrabbed { get; set; }

    

    public GrabbableObject touchedGrabbableObject { get; set; }

    void Awake()
    {
        _inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GrabbableObject"))
        {
            IsHandColliding = true;
            touchedGrabbableObject = other.GetComponent<GrabbableObject>();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("GrabbableObject"))
        {
            IsHandColliding = true;
            touchedGrabbableObject = other.GetComponent<GrabbableObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GrabbableObject"))
            IsHandColliding = false;
        IsHandGrabbed = false;
    }
}