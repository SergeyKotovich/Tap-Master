using UnityEngine;

public class RotationZ : MonoBehaviour
{
    [SerializeField] private float rotationSpeed; // Скорость вращения

    void Update()
    {
        // Вращаем объект по Z
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}