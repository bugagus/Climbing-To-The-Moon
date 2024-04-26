using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _grab, _jump, _jetpack, _charging, _fullCharged, _text, _hit, _hitFloor, _moonLight;
    [SerializeField, Range(0f, 1f)] private float _grabV, _jumpV, _jetpackV, _chargingV, _fullChargedV, _textV, _hitV, _hitFloorV, _moonlightV;
    [SerializeField] private AudioSource _audioSource;
    private bool _isPlaying = false;

    public void Grab()
    {
        _audioSource.PlayOneShot(_grab, _grabV);
    }
    public void Jump()
    {
        _audioSource.PlayOneShot(_jump, _jumpV);
    }
    public void Jetpack()
    {
        _audioSource.PlayOneShot(_jetpack, _jetpackV);
    }
    public void Charging()
    {
       
    }
    public void FullCharged()
    {
        _audioSource.Stop();
        _audioSource.clip = _fullCharged;
        _audioSource.volume = _fullChargedV;
        _audioSource.loop = false;
        _audioSource.Play();

    }

    public void EndFullCharged()
    {
        if(_audioSource.clip == _fullCharged)
        {
            _audioSource.Stop();
        }
        
    }
    public void TextS()
    {
        _audioSource.PlayOneShot(_text, _textV);
    }
    public void Hit()
    {
        _audioSource.PlayOneShot(_hit, _hitV);
    }
    public void HitFloor()
    {
        _audioSource.PlayOneShot(_hitFloor, _hitFloorV);
    }

    public void MoonLight()
    {
        _audioSource.PlayOneShot(_moonLight, _moonlightV);
    }

    public void setIsPlaying(bool isPlaying)
    {
        _isPlaying = isPlaying;

        if (!isPlaying)
        {
            _audioSource.loop = false;
            if (_audioSource.clip == _fullCharged)
            {
                _audioSource.Stop();
                _audioSource.clip = null;
                _audioSource.volume = 1f;
            }
        }
    }


}
