using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DialogueCollider : MonoBehaviour
{
    public DialogueController tutorial;
    private bool flag = false;
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.CompareTag("Player") && !flag)
        {
            flag = true;
            tutorial.StartDialogue();
            DOVirtual.DelayedCall(6, ()=> {tutorial.FinishDialogue(); flag = false;});
        }
    }
}
