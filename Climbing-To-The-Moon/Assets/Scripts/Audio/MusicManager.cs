using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs, loopSongs; // Array de canciones
    [SerializeField] private float _bg1Height, _bg2Height, _bg3Height, _bg4Height;
    [SerializeField] private AudioSource audioSource;
    [SerializeField, Range(0f, 10f)] private float fadeDuration;
    [SerializeField, Range(0f, 1f)] private float maxVolume;
    private float _previousYPos, _currentYPos;
    private int loopingSong = 0;
    private GameObject _beetle;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _previousYPos = 0;
        _beetle = GameObject.FindGameObjectWithTag("Player");
        audioSource.Play();
    }

    private void Update()
    {   
        if(!audioSource.isPlaying && loopingSong != -1)
        {
            audioSource.Stop();
            audioSource.clip = loopSongs[loopingSong];
            audioSource.Play();
        }
        _currentYPos = _beetle.transform.position.y;
        if((_currentYPos > _bg2Height && _previousYPos < _bg2Height) ||(_currentYPos < _bg3Height && _previousYPos > _bg3Height))
        {
            if(_currentCoroutine!=null)
            {
                StopCoroutine(_currentCoroutine);
            }
            Debug.Log("Entro en la segunda transicion");
            _currentCoroutine = StartCoroutine(TransicionVolumen(songs[1], 1));
        }
        else if((_currentYPos > _bg3Height && _previousYPos < _bg3Height) || (_currentYPos < _bg4Height && _previousYPos > _bg4Height))
        {
            if(_currentCoroutine!=null)
            {
                StopCoroutine(_currentCoroutine);
            }
            Debug.Log("Entro en la tercera transicion");
            _currentCoroutine = StartCoroutine(TransicionVolumen(songs[2], 2));
        }
        else if(_currentYPos > _bg4Height && _previousYPos < _bg4Height)
        {
            if(_currentCoroutine!=null)
            {
                StopCoroutine(_currentCoroutine);
            }
            Debug.Log("Entro en la cuarta transicion");
            _currentCoroutine = StartCoroutine(TransicionVolumen(songs[3], 3));
        }
        else if(_currentYPos < _bg2Height && _previousYPos > _bg2Height)
        {
            if(_currentCoroutine!=null)
            {
                StopCoroutine(_currentCoroutine);
            }
            Debug.Log("Entro en la primera transicion");
            _currentCoroutine = StartCoroutine(TransicionVolumen(songs[0], 4));
        }
        _previousYPos = _currentYPos;

    }

    
    private IEnumerator TransicionVolumen(AudioClip newClip, int index)
    {
        loopingSong = -1;
        audioSource.loop = false;
        float tiempoInicio = Time.time;
        float volumenInicial= audioSource.volume;
        while (Time.time - tiempoInicio < fadeDuration)
        {
            float t = (Time.time - tiempoInicio) / fadeDuration;
            audioSource.volume = Mathf.Lerp(volumenInicial, 0, t);
            yield return null;
        }

        tiempoInicio = Time.time;
        audioSource.volume = 0;
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
        volumenInicial= 0;
        while (Time.time - tiempoInicio < fadeDuration)
        {
            float t = (Time.time - tiempoInicio) / fadeDuration;
            audioSource.volume = Mathf.Lerp(volumenInicial, maxVolume, t);
            yield return null;
        }
        audioSource.volume = maxVolume;
        loopingSong = index;
    }


}
