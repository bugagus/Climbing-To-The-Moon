using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            // Check the velocity of the player
            float speed = Mathf.Abs(rb.velocity.x);
            
            // Flip the sprite based on the direction
            if (rb.velocity.x < 0)
            {
                spriteRenderer.flipX = true; // Flip the sprite
            }
            else if (rb.velocity.x > 0)
            {
                spriteRenderer.flipX = false; // Do not flip the sprite
            }
        }
    }
}
