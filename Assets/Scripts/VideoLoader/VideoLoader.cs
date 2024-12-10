using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayerAnotherPlanet;

    private string _stars = "Stars.mp4"; 
    private string _earth = "Earth.mp4"; 
    private string _mars = "Mars.mp4"; 
    private string _moon = "Moon.mp4"; 
    private string _saturn = "Saturn.mp4"; 

    private void Start()
    {
        SetAnotherPlanet();
    }

    public void SetAnotherPlanet()
    {
        
        var videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, _stars);
        
        _videoPlayerAnotherPlanet.url = videoPath;
        
        _videoPlayerAnotherPlanet.Play();
    }
}
