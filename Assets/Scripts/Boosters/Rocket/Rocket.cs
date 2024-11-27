using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _explosiveSmoke;
    private SoundsManager _soundsManager;
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            var hitPoint = other.ClosestPoint(transform.position);
            _explosiveSmoke.transform.position = hitPoint;
            _explosiveSmoke.SetActive(true);

            gameObject.SetActive(false);
            transform.position = _startPosition;
        }
    }
}