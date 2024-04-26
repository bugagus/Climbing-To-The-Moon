using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

public class IntroController : MonoBehaviour
{
    [SerializeField] private DialogueController introDialogue;
    [SerializeField] private float cameraMoveDuration, introMoonAnimationDuration;
    [SerializeField] private JumpController jumpController;
    [SerializeField] private MoveCamera mainCamera;
    [SerializeField] private Transform introPoint, playerInitialPoint;
    [SerializeField] private float logoFadeSpeed;
    [SerializeField] private FadeOut logo;
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private MoonController moonController;
    [SerializeField] private GameObject superman;
    [SerializeField] private GameObject musicManager;
    public float fadeInDuration = 1.0f;
    public float delayBetweenBackgrounds = 1.0f;

    private bool _introEnded = false, _introStarted = false;

    void Start()
    {
        foreach(GameObject go in objectsToActivate)
        {
            Color color = go.GetComponent<Image>().color;
            color.a = 0;
            go.GetComponent<Image>().color = color;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !_introStarted)
        {
            logo.StartFadeOut(logoFadeSpeed);
            DOVirtual.DelayedCall((1/logoFadeSpeed) + 1, () => StartIntro());
        }
    }

    
    public void StartIntro()
    {
        _introStarted = true;
        moonController.ShutDown();
        DOVirtual.DelayedCall(introMoonAnimationDuration, () => MoveCameraDown());
        DOVirtual.DelayedCall(introMoonAnimationDuration + cameraMoveDuration + 2, () => introDialogue.StartDialogue());
    }

    private void MoveCameraDown()
    {
        superman.SetActive(false);
        mainCamera.MoveObjectToPoint(introPoint.position, cameraMoveDuration);
    }

    public void EndIntro()
    {
        FindObjectOfType<FadeBackground>().NewBettle();
        if(!_introEnded)
        {
            mainCamera.MoveObjectToPoint(playerInitialPoint.position, 1);
            DOVirtual.DelayedCall(1, () => {
                _introEnded = true;
                foreach(GameObject go in objectsToActivate)
                {
                    Color color = go.GetComponent<Image>().color;
                    color.a = 1;
                    go.GetComponent<Image>().color = color;
                }
                jumpController.ContinueMovement();
                mainCamera.gameObject.GetComponent<CinemachineBrain>().enabled = true;
                musicManager.SetActive(true);
                });
        }

    }
}
