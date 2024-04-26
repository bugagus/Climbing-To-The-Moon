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
        Debug.Log("ESTOY TOCANDO ALGO");
        if(col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(UseJetpack(col.gameObject));
        }
    }

    private IEnumerator UseJetpack(GameObject player)
    {
        player.GetComponent<JumpController>().StartJetpack();
        yield return new WaitForSeconds(jetpackDuration);
        player.GetComponent<JumpController>().EndJetpack();
    }
}
