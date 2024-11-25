using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public event Action<int> BlackHoleWasClosed;
    public bool IsActive;
    [SerializeField] private Transform _levelPosition;
    [SerializeField] private Rotator _rotator;
    private int _maxDistance = 100;
    private Vector3 _initialScale;
    private int _destroyedCubesCount;
    private SoundsManager _soundsManager; 
    
    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    public void Activate()
    {
        if (IsActive) return;
        
        IsActive = true;
        gameObject.SetActive(true);
        _rotator.RotateAround();
        OnEnableBlackHole();
    }

    public void OnEnableBlackHole()
    {
        var direction = _levelPosition.position - transform.position;
        var ray = new Ray(transform.position, direction);
        {
            StartCoroutine(OnEnableBlackHoleCoroutine(ray));
            _soundsManager.PlayBlackHole();
        }
    }
    public void OnCubeWasDestroyed()
    {
        _destroyedCubesCount++;
    }

    private IEnumerator OnEnableBlackHoleCoroutine(Ray ray)
    {
        transform.localScale = _initialScale;
        
        var currentTime = 2f;
        while (currentTime>=0)
        {
            currentTime -= Time.deltaTime;
            if (Physics.Raycast(ray, out var hit, _maxDistance))
            {
                Debug.Log("черная дыра попала в " + hit.collider.gameObject.name);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.isKinematic = false;
                    hit.collider.gameObject.transform.SetParent(null);
                    hit.collider.gameObject.transform.DOScale(Vector3.zero, 1f);
                }
                
            }

            yield return null;
        }

        yield return new WaitForSeconds(2);
        
        CloseBlackHole();
    }

    private void CloseBlackHole()
    {
        transform.DOScale(Vector3.zero, 1f).OnComplete(() =>
        {
            transform.gameObject.SetActive(false);
            IsActive = false;
        });
        
        BlackHoleWasClosed?.Invoke(_destroyedCubesCount);
        _destroyedCubesCount = 0;
    }
    
    
}