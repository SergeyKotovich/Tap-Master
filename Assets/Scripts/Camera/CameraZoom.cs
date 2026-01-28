using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed = 2f;

    [SerializeField] private float _minSize = 5f;
    [SerializeField] private float _maxSize = 15f;

    private Camera _camera;
    private float _initialPinchDistance;
    private bool _isActive = true;

    private void Start()
    {
        _camera = Camera.main;
        // Set initial orthographic size
        _camera.orthographicSize = 10f;
    }

    private void Update()
    {
        if (!_isActive) return;
        
        // Handle mouse wheel zoom
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        _camera.orthographicSize -= scroll * _zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minSize, _maxSize);
        
        // Handle touch pinch zoom
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            var prevTouchZeroPos = touchZero.position - touchZero.deltaPosition;
            var prevTouchOnePos = touchOne.position - touchOne.deltaPosition;

            var prevDistance = Vector2.Distance(prevTouchZeroPos, prevTouchOnePos);
            var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
            var deltaDistance = prevDistance - currentDistance;

            _camera.orthographicSize += deltaDistance * _zoomSpeed * Time.deltaTime;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minSize, _maxSize);
        }
    }

    public void EnableZoom(bool value)
    {
        _isActive = value;
    }
}