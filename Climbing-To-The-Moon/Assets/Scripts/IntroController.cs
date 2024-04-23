using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class IntroController : MonoBehaviour
{
    [SerializeField] private DialogueController introDialogue;
    [SerializeField] private float cameraMoveDuration, introMoonAnimationDuration;
    [SerializeField] private JumpController jumpController;
    [SerializeField] private MoveCamera mainCamera;
    [SerializeField] private Transform introPoint, playerInitialPoint;
    private bool introEnded = false;

    void Start()
    {
        StartIntro();
    }

    
    public void StartIntro()
    {
        mainCamera.gameObject.GetComponent<CinemachineBrain>().enabled = false;
        jumpController.StopMovement();
        DOVirtual.DelayedCall(introMoonAnimationDuration, () => MoveCameraDown());
        DOVirtual.DelayedCall(introMoonAnimationDuration + cameraMoveDuration + 2, () => introDialogue.StartDialogue());
    }

    private void MoveCameraDown()
    {
        mainCamera.MoveObjectToPoint(introPoint.position, cameraMoveDuration);
    }

    public void EndIntro()
    {
        if(!introEnded)
        {
            mainCamera.MoveObjectToPoint(playerInitialPoint.position, 1);
            DOVirtual.DelayedCall(1, () => {
                introEnded = true;
                jumpController.ContinueMovement();
                mainCamera.gameObject.GetComponent<CinemachineBrain>().enabled = true;
                });
        }

    }
}
