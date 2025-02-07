using Cube;
using JetBrains.Annotations;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class TapHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private bool _canClick = true;
    private bool _isLaserActive;

    private AsyncMessageBus _messageBus;
    private ITurnHandler _movesCounter;

    [Inject]
    public void Construct(AsyncMessageBus messageBus, ITurnHandler movesCounter)
    {
        _movesCounter = movesCounter;
        _messageBus = messageBus;
    }

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        HandleStandaloneInput();
#elif UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL
    HandleMobileInput();
#endif
    }

    private void HandleStandaloneInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var clickPosition = Input.mousePosition;
        ProcessTap(clickPosition);
    }

    private void HandleMobileInput()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Vector3 touchPosition = touch.position;
                ProcessTap(touchPosition);
            }
        }
    }

    private void ProcessTap(Vector3 position)
    {
        if (!_canClick) return;

        var ray = _camera.ScreenPointToRay(position);
        if (!Physics.Raycast(ray, out var hit)) return;

        if (hit.transform.CompareTag(GlobalConstants.CUBE_TAG))
        {
            if (_isLaserActive)
            {
                _messageBus.Publish(new LaserTargetPositionSet(hit.point));
                _isLaserActive = false;
                return;
            }

            if (_movesCounter.HasMoves())
            {
                _movesCounter.SpendOneMove();
            }

            var cube = hit.transform.GetComponent<IMover>();
            if (cube.IsMoving) return;

            cube.TryMove();
            SoundsManager.Instance.PlayClick();
        }
    }



    public void ClickEnabled(bool onOff)
    {
        _canClick = onOff;
    }

    [UsedImplicitly]
    public void EnableLaser()
    {
        _isLaserActive = true;
    }
}