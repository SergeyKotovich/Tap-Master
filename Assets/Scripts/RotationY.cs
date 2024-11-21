using UnityEngine;

public class RotationY : MonoBehaviour
{
    [SerializeField] private float rotationSpeed; // Скорость вращения

    void Update()
    {
        // Вращаем объект по Z
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}