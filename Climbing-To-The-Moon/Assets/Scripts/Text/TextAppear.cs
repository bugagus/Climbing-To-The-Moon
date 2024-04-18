using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppear : MonoBehaviour
{
    public List<string> dialogues; // Lista de textos a mostrar
    public TMP_Text text;
    public float appearSpeed = 0.1f; // Velocidad de aparición de las letras
    public KeyCode skipKey = KeyCode.Space; // Tecla para saltar al siguiente texto
    

    private int currentDialogueIndex = 0;
    private string currentText = "";
    private int currentIndex = 0;
    private bool isAppearing = false;

    void Start()
    {
        // Comenzar la aparición del texto
        StartAppear();
    }

    void Update()
    {
        if (isAppearing)
        {
            // Si el jugador presiona la tecla para saltar, pasar al siguiente texto
            if (Input.GetKeyDown(skipKey))
            {
                ShowNextDialogue();
                return;
            }

            // Añadir la siguiente letra al texto mostrado
            currentText += dialogues[currentDialogueIndex][currentIndex];
            text.text = currentText;

            // Incrementar el índice para apuntar a la siguiente letra
            currentIndex++;

            // Verificar si se han mostrado todas las letras del texto actual
            if (currentIndex >= dialogues[currentDialogueIndex].Length)
            {
                isAppearing = false;
            }
            else
            {
                // Esperar un tiempo antes de mostrar la siguiente letra
                Invoke("UpdateText", appearSpeed);
            }
        }
    }

    void StartAppear()
    {
        // Limpiar el texto mostrado en el componente TextMeshPro
        text.text = "";

        // Iniciar la aparición del texto
        isAppearing = true;
    }

    void UpdateText()
    {
        // Actualizar el texto para mostrar la siguiente letra
        Update();
    }

    void ShowNextDialogue()
    {
        // Mostrar el siguiente texto en la lista si hay más
        if (currentDialogueIndex < dialogues.Count - 1)
        {
            currentDialogueIndex++;
            currentText = "";
            currentIndex = 0;
            StartAppear();
        }
        else
        {
            // Si no hay más textos, cerrar el diálogo
            CloseDialogue();
        }
    }

    void CloseDialogue()
    {
        // Aquí puedes agregar código para cerrar el diálogo, por ejemplo, desactivar el objeto que contiene el script
        gameObject.SetActive(false);
    }
}