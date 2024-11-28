using Cube;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _explosiveSmoke;
    [SerializeField] private Vector3 _startPosition;
    private readonly int _delay = 5000;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.CUBE_TAG))
        {
            other.GetComponent<ICubeDestroyer>().DestroyCube();
            var hitPoint = other.ClosestPoint(transform.position);
            _explosiveSmoke.transform.position = hitPoint;
            _explosiveSmoke.SetActive(true);

            gameObject.SetActive(false);
            SetDefaultParameters().Forget();
        }
    }

    private async UniTask SetDefaultParameters()
    {
        await UniTask.Delay(_delay);
        transform.position = _startPosition;
        _explosiveSmoke.SetActive(false);
    }
}