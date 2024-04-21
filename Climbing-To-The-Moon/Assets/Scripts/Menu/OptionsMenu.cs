using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    private bool pausedMenu = false;
    public Animator mAnimator;
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
        if(activeEnumerator == null)
        {
            if (!pausedMenu)
            {
                pausedMenu = true;
                pauseMenu.SetActive(true);
                mAnimator.SetBool("BOpen", true);
                activeEnumerator = StartCoroutine(waitTime(true));
                Debug.Log("Entro primero");
            }
            else
            {
                pausedMenu = false;
                mAnimator.SetBool("BOpen", false);
                activeEnumerator = StartCoroutine(waitTime(false));
                Debug.Log("Entro segundo");
            }
        }
    }

    private IEnumerator waitTime(bool isActive)
    {
        yield return new WaitForSeconds(animDuration);
        if(isActive != true)
        {
            pauseMenu.SetActive(isActive);
        }
        activeEnumerator = null;
    }


    public void closeGame()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }

}
