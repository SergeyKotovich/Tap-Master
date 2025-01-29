using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed;

    [SerializeField] private float _minFOV;
    [SerializeField] private float _maxFOV;

    private Camera _camera;
    private float _initialPinchDistance;
    private bool _isActive = true;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!_isActive) return;
        
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        _camera.fieldOfView -= scroll * _zoomSpeed;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _minFOV, _maxFOV);
        
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            var prevTouchZeroPos = touchZero.position - touchZero.deltaPosition;
            var prevTouchOnePos = touchOne.position - touchOne.deltaPosition;

            var prevDistance = Vector2.Distance(prevTouchZeroPos, prevTouchOnePos);
            var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
            var deltaDistance = prevDistance - currentDistance;

            _camera.fieldOfView += deltaDistance * _zoomSpeed * Time.deltaTime;
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _minFOV, _maxFOV);
        }
    }


    public void EnableZoom(bool value)
    {
        _isActive = value;
    }
}