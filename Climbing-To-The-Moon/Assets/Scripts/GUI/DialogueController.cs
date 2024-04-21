using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct Word
{
    [Range(1, 10)] public float xOffset;
    [Range(1, 10)] public float yOffset;
    [Range(0, 5)] public float speed;
    [Range(0, 5)] public float amplitude;

    public Gradient color;
    public bool wobbleByChar;
}

[System.Serializable]
public struct DisplayedLines
{
    public string lineText;
    public List<Word> words;
}

[System.Serializable]
public enum DialogueTypeMethod
{
    LINE, WORD, CHAR
}

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueTypeMethod dialogueTypeMethod;
    [SerializeField] private float typingSpeed, animationDuration;
    [SerializeField] public DisplayedLines[] displayedLines;
    [SerializeField] private GameObject continueIcon;
    public int _currentLine;
    public int CurrentLetter { get; private set; }
    private Animator _animator;
    private Coroutine _displayLineCoroutine;
    private bool _canContinue;
    private bool _isInDialogue;
    private bool _lineDisplayedCompletely;
    [SerializeField] private GameObject player;
    [SerializeField] private bool stopPlayer;
    [SerializeField] private TextWobble textWobble;

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
            //if (_lineDisplayedCompletely && _canContinue)
            NextLine();
            //else
            //{
            //    SkipLine();
            //}
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
            _displayLineCoroutine = StartCoroutine(DisplayLine(displayedLines[_currentLine].lineText));
            DisplayLine(displayedLines[_currentLine].lineText);
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

        dialogueText.text = line;
        dialogueText.alpha = 0;

        CurrentLetter = 0;
        List<int> wordLengths = textWobble.UpdateTextState();

        switch (dialogueTypeMethod)
        {
            case DialogueTypeMethod.LINE:
                CurrentLetter = line.Length;
                break;
            case DialogueTypeMethod.WORD:

                for (int i = 0; i < wordLengths.Count; i++)
                {
                    yield return new WaitForSeconds(typingSpeed);
                    CurrentLetter += wordLengths[i] + 1; // +1 por los espacios
                }
                break;
            case DialogueTypeMethod.CHAR:
                foreach (char letter in line.ToCharArray())
                {
                    yield return new WaitForSeconds(typingSpeed);
                    CurrentLetter++;
                }
                break;
        }

        _isInDialogue = true;
        _lineDisplayedCompletely = true; // Marcar la línea como mostrada completamente
        _canContinue = true;
        continueIcon.SetActive(true);
    }
    private IEnumerator InitiateDialogue()
    {
        if(stopPlayer)
        {
            //player.GetComponent<JumpController>().StopMovement();
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
            //player.GetComponent<JumpController>().ContinueMovement();
        }
        continueIcon.SetActive(false);
        _animator.SetTrigger("Disappear");
        _isInDialogue = false;
    }

    private void SkipLine()
    {
        Debug.Log("SKIPPEO");
        StopCoroutine(_displayLineCoroutine); // Detiene la animación de la línea
        dialogueText.text = displayedLines[_currentLine - 1].lineText; // Muestra la línea completa
        _lineDisplayedCompletely = true; // Marca la línea como mostrada completamente
        _canContinue = true; // Permite avanzar
        continueIcon.SetActive(true); // Muestra el icono de continuar
    }
}