using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed, animationDuration;
    [SerializeField] private string[] displayedLines;
    [SerializeField] private GameObject continueIcon;
    private int _currentLine;
    private Animator _animator;
    private Coroutine _displayLineCoroutine;
    private bool _canContinue;
    private bool _isInDialogue;
    private bool _lineDisplayedCompletely;
    [SerializeField] private GameObject player;
    [SerializeField] private bool stopPlayer;

    void Start()
    {
        _currentLine = 0;
        _isInDialogue = false;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isInDialogue && Input.GetKeyDown(KeyCode.Space) )
        {
            if (_lineDisplayedCompletely && _canContinue)
            {
                NextLine();
            }
            else
            {
                SkipLine();
            }
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(InitiateDialogue());
    }

    private void NextLine()
    {
        Debug.Log("VOY A REPRODUCIR UNA LINEA");
        if (_currentLine < displayedLines.Length)
        {
            if (_displayLineCoroutine != null) StopCoroutine(_displayLineCoroutine);
            _displayLineCoroutine = StartCoroutine(DisplayLine(displayedLines[_currentLine]));
            _currentLine++;
        }
        else
        {
            FinishDialogue();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        continueIcon.SetActive(false);
        _canContinue = false;
        dialogueText.text = "";
        _lineDisplayedCompletely = false; // Restablecer la variable antes de mostrar la línea

        bool isAddingRichTextTag = false;
        foreach (char letter in line.ToCharArray())
        {
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
                _isInDialogue = true;
            }
        }
        _lineDisplayedCompletely = true; // Marcar la línea como mostrada completamente
        _canContinue = true;
        continueIcon.SetActive(true);
    }
    private IEnumerator InitiateDialogue()
    {
        if(stopPlayer)
        {
            player.GetComponent<JumpController>().StopMovement();
        }
        _animator.SetTrigger("Appear");
        yield return new WaitForSeconds(animationDuration);
        dialogueText.gameObject.SetActive(true);
        NextLine();
    }

    private void FinishDialogue()
    {
        if(stopPlayer)
        {
            player.GetComponent<JumpController>().ContinueMovement();
        }
        continueIcon.SetActive(false);
        _animator.SetTrigger("Disappear");
        _isInDialogue = false;
    }

    private void SkipLine()
    {
        Debug.Log("SKIPPEO");
        StopCoroutine(_displayLineCoroutine); // Detiene la animación de la línea
        dialogueText.text = displayedLines[_currentLine - 1]; // Muestra la línea completa
        _lineDisplayedCompletely = true; // Marca la línea como mostrada completamente
        _canContinue = true; // Permite avanzar
        continueIcon.SetActive(true); // Muestra el icono de continuar
    }
}