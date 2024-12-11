using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundsLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayerAnotherPlanet;

    private readonly Dictionary<Enum, string> _videos = new();

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

        var value = _videos[type];
        var videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, value);

        _videoPlayerAnotherPlanet.url = videoPath;

        _videoPlayerAnotherPlanet.Play();
        _currentType = type;
    }
}