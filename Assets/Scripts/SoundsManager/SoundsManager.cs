using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private List<AudioClip> _backgroundMusic;

    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _collision;
    [SerializeField] private AudioClip _victory;
    [SerializeField] private AudioClip _defeat;
    [SerializeField] private AudioClip _laser;
    [SerializeField] private AudioClip _rocket;
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private AudioClip _blackHole;
    [SerializeField] private AudioClip _destroyCubeInHole;

    public static SoundsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        var randomIndex = Random.Range(0, _backgroundMusic.Count);
       _musicSource.clip = _backgroundMusic[randomIndex];
       _musicSource.Play();
    }
    
    public void PlayClick()
    {
        _soundsSource.PlayOneShot(_click);
    }

    public void PlayButtonClick()
    {
        _soundsSource.PlayOneShot(_buttonClick);
    }

    public void PlayShakeSound()
    {
        _soundsSource.PlayOneShot(_collision);
    }

    public void PlayVictory()
    {
        _soundsSource.PlayOneShot(_victory);
    }

    public void PlayDefeat()
    {
        _soundsSource.PlayOneShot(_defeat);
    }

    public void PlayShotLaser()
    {
        _soundsSource.PlayOneShot(_laser);
    }

    public void PlayShotRocket()
    {
        _soundsSource.PlayOneShot(_rocket);
    }

    public void PlayExplosion()
    {
        _soundsSource.PlayOneShot(_explosion);
    }

    public void PlayBlackHole()
    {
        _soundsSource.PlayOneShot(_blackHole);
    }

    public void PlayDestroyCubeInHole()
    {
        _soundsSource.PlayOneShot(_destroyCubeInHole);
    }

    public void MuteUnmute()
    {
        _soundsSource.mute = !_soundsSource.mute;
        _musicSource.mute = !_musicSource.mute;
    }
}