using System.Collections.Generic;
using UnityEngine;

public class RocketsPhysicsHandler : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private List<Rigidbody> _rockets;

    public void LaunchRockets()
    {
        foreach (var rocket in _rockets)
        {
            rocket.gameObject.SetActive(true);
            rocket.velocity = transform.forward * _moveSpeed;
            rocket.angularVelocity = transform.forward * _rotationSpeed;
        }
    }
}