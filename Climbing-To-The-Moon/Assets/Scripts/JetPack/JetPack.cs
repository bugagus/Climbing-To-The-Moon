using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    [SerializeField] private float jetpackDuration;
    public float jetpackVerticalForce { get; set; }
    public float jetpackHorizontalForce { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(UseJetpack(col.gameObject));
        }
    }

    private IEnumerator UseJetpack(GameObject player)
    {
        player.GetComponent<PlayerController>().StartJetpack(this);
        yield return new WaitForSeconds(jetpackDuration);
        player.GetComponent<PlayerController>().EndJetpack();
    }
}
