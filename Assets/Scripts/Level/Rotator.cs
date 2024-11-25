using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity;

    private bool CanRotate = true;
    private IDisposable _subscriptions;

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
        if (!Input.GetMouseButton(0)) return;

        if (CanRotate)
        {
            var rotationX = Input.GetAxis("Mouse X") * _sensitivity;
            var rotationY = Input.GetAxis("Mouse Y") * _sensitivity;
            
            transform.RotateAround(transform.position, Vector3.up, -rotationX);
            transform.RotateAround(transform.position, Vector3.right, rotationY);
        }
    }

    public void RotateAround()
    {
        var currentRotate = transform.eulerAngles;
        currentRotate.y += 600;
        currentRotate.x += 600;
        transform.DORotate(currentRotate, 4f,RotateMode.FastBeyond360);

    }
    
    public void RotateEnabled(bool onOff)
    {
        CanRotate = onOff;
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}
