using System.Collections;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float blinkInterval = 0.5f;

    void Start()
    {
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            textMeshPro.enabled = false;
            yield return new WaitForSeconds(blinkInterval);

            textMeshPro.enabled = true;
            yield return new WaitForSeconds(blinkInterval * 2);
        }
    }
}