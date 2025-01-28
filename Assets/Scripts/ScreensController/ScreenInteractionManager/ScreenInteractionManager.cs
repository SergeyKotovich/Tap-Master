using UnityEngine;

public class ScreenInteractionManager : MonoBehaviour
{
    [SerializeField] private TapHandler _tapHandler;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private CameraZoom _cameraZoom;

    public void EnableInteraction(bool value)
    {
        _tapHandler.ClickEnabled(value);
        _rotator.RotateEnabled(value);
        _cameraZoom.EnableZoom(value);
    }
    
}
