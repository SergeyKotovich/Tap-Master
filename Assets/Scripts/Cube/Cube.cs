using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniTaskPubSub;
using UnityEngine;

namespace Cube
{
    public class Cube : MonoBehaviour, ICubeDestroyer , IMover
    {
        public bool IsMoving { get; private set; }

        private readonly float _moveDuration = 4f;
        private readonly float _obstacleReturnDuration = 0.2f;
        private readonly int _distanceToDisable = 30;
        private float _shift = 0.9f;

        private bool _isGoingToObstacle;

        private RaycastHit _hit;
        private Vector3 _initialPosition;
        private ShakeAnimationController _shakeAnimationController;
        private ObstacleDetector _obstacleDetector;
        private EffectFactory _effectFactory;
        private AsyncMessageBus _messageBus;


        public void Initialize(ObstacleDetector obstacleDetector, ShakeAnimationController shakeAnimationController,
            EffectFactory effectFactory, AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
            _effectFactory = effectFactory;
            _shakeAnimationController = shakeAnimationController;
            _obstacleDetector = obstacleDetector;
        }

        public void TryMove()
        {
            if (_isGoingToObstacle || IsMoving)
            {
                return;
            }

            if (IsWayFree())
            {
                MoveForward();
            }
            else
            {
                MoveToObstacle();
            }
        }

        private void MoveForward()
        {
            IsMoving = true;
            transform.SetParent(null);
            _messageBus.Publish(new CubeWasDestroyedEvent());
            var endPosition = transform.position - transform.forward * _distanceToDisable;
            var effect = _effectFactory.ShowEffect(gameObject);
            transform.DOMove(endPosition, _moveDuration).OnComplete(() =>
            {
                _effectFactory.HideEffect(effect);
                Destroy(gameObject);
            });
        }

        private void MoveToObstacle()
        {
            _isGoingToObstacle = true;

            _initialPosition = transform.localPosition;

            var targetPosition = _hit.transform.localPosition -
                                 (_hit.transform.localPosition - transform.localPosition).normalized * _shift;

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMove(targetPosition, _obstacleReturnDuration));
            sequence.AppendCallback(() => _shakeAnimationController.ShakeAnimation().Forget());
            sequence.Append(transform.DOLocalMove(_initialPosition, _obstacleReturnDuration));
            sequence.OnComplete(() =>_isGoingToObstacle = false);
        }


        private bool IsWayFree()
        {
            _obstacleDetector.PerformRaycast(out _hit, transform);
            _obstacleDetector.FindObstacleNeighbours();

            return _hit.collider == null || _hit.transform.GetComponent<Cube>()?.IsMoving == true;
        }

        public void DestroyCube()
        {
            transform.DOKill();
            Destroy(gameObject);
            _messageBus.Publish(new CubeWasDestroyedEvent());
        }
    }
}