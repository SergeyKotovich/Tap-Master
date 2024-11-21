using UnityEngine;


public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _background;

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

    public void PlayBackground()
    {
        _audioSource.Play();
    }

    public void StopBackground()
    {
        _audioSource.Stop();
    }

    public void PlayClick()
    {
        _audioSource.PlayOneShot(_click);
    }

    public void PlayButtonClick()
    {
        _audioSource.PlayOneShot(_buttonClick);
    }

    public void PlayCollision()
    {
        _audioSource.PlayOneShot(_collision);
    }

    public void PlayVictory()
    {
        _audioSource.PlayOneShot(_victory);
    }

    public void PlayDefeat()
    {
        _audioSource.PlayOneShot(_defeat);
    }

    public void PlayShotLaser()
    {
        _audioSource.PlayOneShot(_laser);
    }

    public void PlayShotRocket()
    {
        _audioSource.PlayOneShot(_rocket);
    }

    public void PlayExplosion()
    {
        _audioSource.PlayOneShot(_explosion);
    }

    public void PlayBlackHole()
    {
        _audioSource.PlayOneShot(_blackHole);
    }

    public void PlayDestroyCubeInHole()
    {
        _audioSource.PlayOneShot(_destroyCubeInHole);
    }

    public void MuteUnmute()
    {
        _audioSource.mute = !_audioSource.mute;
        _background.mute = !_background.mute;
    }
}