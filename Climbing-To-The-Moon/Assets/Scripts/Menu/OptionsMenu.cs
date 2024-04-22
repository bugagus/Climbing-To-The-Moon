using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    private bool pausedMenu = false;
    public Animator mAnimator, butAnimator;
    private bool _muted = false, _stopped = false;
    [SerializeField] float animDuration;
    private Coroutine activeEnumerator;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (activeEnumerator == null)
        {
            if (!pausedMenu)
            {
                pausedMenu = true;
                pauseMenu.SetActive(true);
                mAnimator.SetBool("BOpen", true);
                activeEnumerator = StartCoroutine(WaitTime(true));
                Debug.Log("Entro primero");
            }
            else
            {
                pausedMenu = false;
                mAnimator.SetBool("BOpen", false);
                activeEnumerator = StartCoroutine(WaitTime(false));
                Debug.Log("Entro segundo");
            }
        }
    }

    private IEnumerator WaitTime(bool isActive)
    {
        yield return new WaitForSeconds(animDuration);
        if (isActive != true)
        {
            pauseMenu.SetActive(isActive);
        }
        activeEnumerator = null;
    }

    public void CloseGame()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }

    public void MuteGame()
    {
        if (_muted)
        {
            AudioListener.volume = 1;
            _muted = false;
            butAnimator.SetBool("Able", false);
        }
        else
        {
            AudioListener.volume = 0;
            _muted = true;
            butAnimator.SetBool("Able", true);
        }
    }

    public void StopGame()
    {
        if (_stopped)
        {
            Time.timeScale = 1;
            _stopped = false;
        }
        else
        {
            Time.timeScale = 0;
            _stopped = true;
        }
    }


}
