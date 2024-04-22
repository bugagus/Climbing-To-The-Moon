using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private DialogueController IntroDialogue;
    private bool _introPlayed;

    void Start()
    {
        _introPlayed = false;
    }
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Backspace)) && !_introPlayed)
        {
            _introPlayed = true;
            IntroDialogue.StartDialogue();
        }
    }
}
