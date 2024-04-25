using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float fadeSpeed = 0.5f; // Velocidad de desvanecimiento

    public Image image;
    public TextMeshProUGUI textMeshPro;
    private Color currentColor;
    private bool fadingOut = false;

    void Start()
    {
        currentColor = image.color;
    }

    void Update()
    {
        if (fadingOut)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            textMeshPro.color = currentColor;
            image.color = currentColor;
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