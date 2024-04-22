using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            character = collision.transform;
            character.parent = transform; // Hacer que el personaje sea hijo de la plataforma
            character.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            character.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

}