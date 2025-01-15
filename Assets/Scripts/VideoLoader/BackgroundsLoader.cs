using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundsLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayerAnotherPlanet;
    [SerializeField] private Transform _background;

    private readonly Dictionary<Enum, string> _videos = new();
    private readonly Vector3 _sizeSaturn = new(100, 100, 100);
    private readonly  Vector3 _sizeAnotherPlanet = new(150, 100, 100);
    private readonly Vector3 _positionAnotherPlanet =new(-0.2f, 0, 0);

    private BackgroundsType _currentType = BackgroundsType.Earth;

    private string _stars = "Stars.mp4";
    private string _earth = "Earth.mp4";
    private string _mars = "Mars.mp4";
    private string _moon = "Moon.mp4";
    private string _saturn = "Saturn.mp4";

    private void Start()
    {
        _videos.Add(BackgroundsType.Stars, _stars);
        _videos.Add(BackgroundsType.Earth, _earth);
        _videos.Add(BackgroundsType.Mars, _mars);
        _videos.Add(BackgroundsType.Moon, _moon);
        _videos.Add(BackgroundsType.Saturn, _saturn);
        
        SetBackground(BackgroundsType.Stars);
    }

    public void SetBackground(BackgroundsType type)
    {
        if (_currentType == type)
        {
            return;
        }

        if (type == BackgroundsType.Saturn)
        {
            _background.localScale = _sizeSaturn;
            _background.localPosition = Vector3.zero;
        }
        else
        {
            _background.localScale = _sizeAnotherPlanet;
            _background.localPosition = _positionAnotherPlanet;
        }

        var value = _videos[type];
        var videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, value);

        _videoPlayerAnotherPlanet.url = videoPath;

        _videoPlayerAnotherPlanet.Play();
        _currentType = type;
    }
}