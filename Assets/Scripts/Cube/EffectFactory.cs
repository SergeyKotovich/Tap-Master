using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class EffectFactory
{
    private readonly Quaternion _baseRotation = Quaternion.Euler(0, 90, 90);
    private readonly Vector3 _shift = new (0,0,0.55f);
    
    private readonly GameObject _flyEffectPrefab;
    private readonly ObjectPool<GameObject> _effectsPool;
    private readonly IObjectResolver _container;

    public EffectFactory(IObjectResolver container, GameObject flyEffectPrefab)
    {
        _container = container;
        _flyEffectPrefab = flyEffectPrefab;
        _effectsPool = new ObjectPool<GameObject>(Create, Get, Release);
    }

    private void Release(GameObject effect)
    {
        effect.gameObject.SetActive(false);
        effect.transform.position = Vector3.zero;
        effect.transform.rotation = Quaternion.identity;
    }

    private void Get(GameObject effect)
    {
        effect.gameObject.SetActive(true);
    }

    private GameObject Create()
    {
        return _container.Instantiate(_flyEffectPrefab, Vector3.zero, Quaternion.identity);
    }

    public GameObject ShowEffect(GameObject cube)
    {
        var effect = _effectsPool.Get();
        effect.transform.SetParent(cube.transform);
        effect.transform.position = cube.transform.position + _shift;
        effect.transform.rotation = cube.transform.rotation * _baseRotation;
        return effect;
    }

    public void HideEffect(GameObject effect)
    {
        effect.transform.SetParent(null);
        _effectsPool.Release(effect);
    }
}