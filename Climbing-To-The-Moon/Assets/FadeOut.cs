using TMPro;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeSpeed = 0.5f; // Velocidad de desvanecimiento

    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI textMeshPro;
    private Color currentColor;
    private bool fadingOut = false;

    void Start()
    {
        currentColor = spriteRenderer.color;
    }

    void Update()
    {
        if (fadingOut)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            textMeshPro.color = currentColor;
            spriteRenderer.color = currentColor;
            if (currentColor.a <= 0f)
            {
                fadingOut = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void StartFadeOut(float speed)
    {
        fadeSpeed = speed;
        fadingOut = true;
    }
}