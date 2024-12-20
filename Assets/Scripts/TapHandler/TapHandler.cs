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
        if (!Input.GetMouseButtonDown(0)) return;

        if (_canClick)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

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
                if (cube.IsMoving)
                {
                    return;
                }

                cube.TryMove();
            }
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