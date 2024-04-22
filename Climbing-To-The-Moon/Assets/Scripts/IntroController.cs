using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField] private DialogueController introDialogue;
    public void StartIntro()
    {
        introDialogue.StartDialogue();
    }
}
