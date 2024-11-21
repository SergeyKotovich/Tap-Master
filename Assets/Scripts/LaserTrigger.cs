using System;
using Cube;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    private CubesController _cubesController;

    private void Awake()
    {
        _cubesController = Container.Instance.CubesController;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, срабатывает ли триггер на объекте, который движется
        if (other.gameObject.CompareTag("Cube"))
        {
            _cubesController.MarkCubesAsGone(1);
            Debug.Log("WasAttacked");

            DestroyObject(other.gameObject);
            Debug.Log("Попал в триггер");
        }
    }

    // Метод для разрушения объекта
    private void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}