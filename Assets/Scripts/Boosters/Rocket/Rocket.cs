using Cube;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _explosiveSmoke;
    [SerializeField] private Vector3 _startPosition;
    private readonly int _delay = 5000;

    private void OnEnable()
    {
        SetDefaultParameters().Forget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.CUBE_TAG))
        {
            other.GetComponent<ICubeDestroyer>().DestroyCube();
            var hitPoint = other.ClosestPoint(transform.position);
            SoundsManager.Instance.PlayExplosion();
            _explosiveSmoke.transform.position = hitPoint;
            _explosiveSmoke.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private async UniTask SetDefaultParameters()
    {
        await UniTask.Delay(_delay);
        gameObject.SetActive(false);
        transform.position = _startPosition;
        _explosiveSmoke.SetActive(false);
    }
}