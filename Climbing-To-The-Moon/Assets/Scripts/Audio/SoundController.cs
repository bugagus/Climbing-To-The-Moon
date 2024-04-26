using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _grab, _jump, _jetpack, _charging, _fullCharged, _text, _hit, _hitFloor;
    [SerializeField, Range(0f, 1f)] private float _grabV, _jumpV, _jetpackV, _chargingV, _fullChargedV, _textV, _hitV, _hitFloorV;
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
        if(!_isPlaying)
        {
            _audioSource.loop = false;
            _audioSource.clip = _charging;
            _audioSource.Play();
        }
    }
    public void FullCharged()
    {
        if(!_isPlaying)
        {
            _audioSource.loop = false;
            _audioSource.clip = _charging;
            _audioSource.Play();
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
