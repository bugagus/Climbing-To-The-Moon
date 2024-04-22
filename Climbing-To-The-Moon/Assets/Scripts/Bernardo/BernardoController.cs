using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BernardoController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _bounceForce;
    [SerializeField, Range(0f, 10f)] protected float _stunTime;

    private Animator _animator;
    private JumpController _jumpController;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Player" || tag == "LeftHand" || tag == "RightHand")
        {
            Vector2 bounceDirection = other.transform.position.x > transform.position.x ? Vector2.right : Vector2.left;
            other.GetComponent<Rigidbody2D>().AddForce(bounceDirection * _bounceForce, ForceMode2D.Impulse);
        }
    }

}
