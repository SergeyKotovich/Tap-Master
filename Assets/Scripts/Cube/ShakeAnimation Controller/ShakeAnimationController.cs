using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ShakeAnimationController : MonoBehaviour
{
    [SerializeField] private float _obstacleShakeDuration = 0.2f;
    
    private readonly List<RaycastHit> _hitsShakeAnimation = new();

    public void AddCubeForAnimation(RaycastHit cube)
    {
        _hitsShakeAnimation.Add(cube);
        
    }
    public async UniTask ShakeAnimation()
    {
        for (var i = 0; i < _hitsShakeAnimation.Count; i++)
        {
            var hit = _hitsShakeAnimation[i];
            var scaleTarget = hit.transform.localScale;
            hit.transform.DOPunchScale(scaleTarget * 0.5f, _obstacleShakeDuration);
            hit.transform.DOScale(scaleTarget, _obstacleShakeDuration);
            await UniTask.Delay(40);
        }
    }


    public void ResetHits()
    {
        _hitsShakeAnimation.Clear();
    }
}
