using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct Word
{
    [Range(1, 10)] public float xOffset;
    [Range(1, 10)] public float yOffset;

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
    private bool _isInDialogue;
    [SerializeField] private GameObject player;
    [SerializeField] private bool stopPlayer;
    [SerializeField] private TextWobble textWobble;

    void Start()
    {
        _currentLine = 0;
        _isInDialogue = false;
        _animator = GetComponent<Animator>();
        _animator.speed = 1f/animationDuration;
    } 

    void Update()
    {
        if (_isInDialogue && Input.GetKeyDown(KeyCode.Space) )
        {
            NextLine();
        }
    }

    public void StartDialogue()
    {
        NextLine();
    }

    private void NextLine()
    {
        if (_currentLine < displayedLines.Length)
        {
            if (_displayLineCoroutine != null) StopCoroutine(_displayLineCoroutine);
            _displayLineCoroutine = StartCoroutine(DisplayLine(displayedLines[_currentLine].lineText, _currentLine));
        }
        else
        {
            FinishDialogue();
        }
    }

    private IEnumerator DisplayLine(string line, int lineIndex)
    {
        if(lineIndex == 0)
        {
            dialogueText.gameObject.SetActive(true);
            _animator.SetTrigger("Appear");
        }
        continueIcon.SetActive(false);
        dialogueText.text = "";

        dialogueText.text = line;
        dialogueText.alpha = 0;

        CurrentLetter = 0;
        List<int> wordLengths = textWobble.UpdateTextState();
        if(lineIndex == 0)
        {
            yield return new WaitForSeconds(animationDuration);
        }

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
        _currentLine++;
        continueIcon.SetActive(true);
    }

    private void FinishDialogue()
    {
        continueIcon.SetActive(false);
        _animator.SetTrigger("Disappear");
        _isInDialogue = false;
    }
}