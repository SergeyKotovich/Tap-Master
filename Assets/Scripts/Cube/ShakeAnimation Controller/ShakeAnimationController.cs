using System.Collections;
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
        foreach (var hit in _hitsShakeAnimation)
        {
           // _soundsManager.PlayCollision();
            hit.collider.transform.DOPunchScale(Vector3.one * 0.5f, _obstacleShakeDuration);
            hit.collider.transform.DOScale(Vector3.one, _obstacleShakeDuration);
            await UniTask.Delay(100);
        }

        _hitsShakeAnimation.Clear();
    }
    
}
