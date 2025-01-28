using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _swipePanSpeed;

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
        if (_isActive)
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            _camera.fieldOfView += -scroll * _zoomSpeed;

            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _minFOV, _maxFOV);

            if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    _initialPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                }
                else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                {
                    var currentPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    var deltaDistance = currentPinchDistance - _initialPinchDistance;

                    _camera.transform.Translate(_camera.transform.forward * deltaDistance * _swipePanSpeed * Time.deltaTime,
                        Space.World);

                    _initialPinchDistance = currentPinchDistance;
                }
            }
        }
    }

    public void EnableZoom(bool value)
    {
        _isActive = value;
    }
}