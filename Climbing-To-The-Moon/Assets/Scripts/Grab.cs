using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    private InputManager _inputManager;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = GameObject.Find("Player").GetComponent<InputManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftHand"))
            _inputManager.IsLeftHandColliding = true;
        else if (other.CompareTag("RightHand"))
            _inputManager.IsRightHandColliding = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LeftHand"))
            _inputManager.IsLeftHandColliding = false;
        else if (other.CompareTag("RightHand"))
            _inputManager.IsRightHandColliding = false;
    }
}
