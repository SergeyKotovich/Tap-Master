using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BlackHole
{
    public class BlackHoleController : MonoBehaviour
    {
        [SerializeField] private Transform _targetPosition;
        
        private readonly int _maxDistance = 15;
        private readonly List<RaycastHit> _raycastHits = new();
        private readonly Vector3 _endValueForRotation = new(0, 360, 0);
        private readonly float _duration = 2f;
        private readonly int _millisecondsDelay = 2000;
        private readonly Vector3 _startScale = new(20,20,20);
        
        private Vector3 _direction;
        private Vector3[] _offsets;
        


        private void Start()
        {
            _direction = _targetPosition.transform.position - transform.position;
    
            _offsets = new Vector3[]
            {
                new(1, 0, 0),
                new(0, 1, 0),
                new(0, 0, 1),
                new(2, 0, 0),
                new(0, 2, 0),
                new(0, 0, 2),
                new(3, 0, 0),
                new(0, 3, 0),
                new(0, 0, 3),
                new(4, 0, 0),
                new(0, 4, 0),
                new(0, 0, 4),
                new(5, 0, 0),
                new(0, 5, 0),
                new(0, 0, 6),
                new(7, 0, 0),
                new(0, 7, 0),
                new(0, 0, 7),
            };
        }
    
        public void EnableBlackHole()
        {
            transform.DOScale(_startScale, _duration);
            FindTargets();
            GravitationalPull();
            _raycastHits.Clear();
            DisableBlackHole().Forget();
        }

        private void FindTargets()
        {
            for (var i = 0; i < _offsets.Length; i++)
            {
                var rayDirection = (_direction + _offsets[i]).normalized;

                if (Physics.Raycast(transform.position, rayDirection, out var hit, _maxDistance))
                {
                    _raycastHits.Add(hit);
                }
            }
        }

        private void GravitationalPull()
        {
            foreach (var raycastHit in _raycastHits)
            {
                raycastHit.transform.DOMove(transform.position, _duration);
                raycastHit.transform.DORotate(_endValueForRotation, _duration, RotateMode.FastBeyond360)
                    .SetLoops(-1, LoopType.Incremental)
                    .SetEase(Ease.Linear);
            }
        }
        private async UniTask DisableBlackHole()
        {
            await UniTask.Delay(_millisecondsDelay);
            transform.DOScale(0,_duration);
        }
    }
}