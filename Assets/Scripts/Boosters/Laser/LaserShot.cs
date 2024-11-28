using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class LaserShot : MonoBehaviour
{
    [SerializeField] private Rigidbody _laserPrefab;
    [SerializeField] private float _shootForce;
    private int _delay = 7000;
    private bool _isActive;

    private ObjectPool<Rigidbody> _laserPool;

    private void Awake()
    {
        _laserPool = new ObjectPool<Rigidbody>(Create, Get, Release);
    }

    private void Release(Rigidbody laser)
    {
        laser.gameObject.SetActive(false);
        laser.transform.position = transform.position;
        laser.transform.rotation = Quaternion.identity;
    }

    private void Get(Rigidbody laser)
    {
        laser.gameObject.SetActive(true);
    }

    private Rigidbody Create()
    {
        return Instantiate(_laserPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var laser = _laserPool.Get();
                var shootDirection = (hit.point - transform.position).normalized;
                laser.AddForce(shootDirection * _shootForce, ForceMode.Impulse);
                TimerToRelease(laser).Forget();
                _isActive = false;
            }
        }
    }
    
    public void Shot()
    {
        _isActive = true;
        
    }

    private async UniTask TimerToRelease(Rigidbody laser)
    {
        await UniTask.Delay(_delay);
        _laserPool.Release(laser);
    }
}