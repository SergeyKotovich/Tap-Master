using System;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 1f;

    private bool _canRotate = true;
    private IDisposable _subscriptions;
    private Vector2 _previousTouchPosition;

    [Inject]
    public void Construct(AsyncMessageBus messageBus)
    {
        _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(_ => ResetRotation());
    }

    private void ResetRotation()
    {
        transform.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        if (!_canRotate) return;

#if UNITY_STANDALONE || UNITY_EDITOR
        HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
        HandleTouchInput();
#endif
    }

    private void HandleMouseInput()
    {
        if (!Input.GetMouseButton(0)) return;

        var rotationX = Input.GetAxis("Mouse X") * _sensitivity;
        var rotationY = Input.GetAxis("Mouse Y") * _sensitivity;

        Rotate(rotationX, rotationY);
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount != 1) return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            _previousTouchPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            var delta = touch.position - _previousTouchPosition;
            _previousTouchPosition = touch.position;

            var rotationX = delta.x * _sensitivity * 0.1f;
            var rotationY = delta.y * _sensitivity * 0.1f;

            Rotate(rotationX, rotationY);
        }
    }

    private void Rotate(float rotationX, float rotationY)
    {
        transform.RotateAround(transform.position, Vector3.up, -rotationX);
        transform.RotateAround(transform.position, Vector3.right, rotationY);
    }

    public void RotateEnabled(bool onOff)
    {
        _canRotate = onOff;
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}