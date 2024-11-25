using Cube;
using UnityEngine;

public class RocketTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    private SoundsManager _soundsManager; // Звук

  

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, срабатывает ли триггер на объекте, который движется
        if (other.gameObject.CompareTag("Cube"))
        {
            // Определяем точку касания с триггером
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            
            // Инстанциируем объект в точке касания
            Instantiate(_gameObject, hitPoint, Quaternion.identity);
            
            Debug.Log("WasAttacked");

            DestroyObject(other.gameObject);
            Debug.Log("Попал в триггер");
            
            _soundsManager.PlayExplosion(); // звук взрыва
        }
    }

    // Метод для разрушения объекта
    private void DestroyObject(GameObject obj)
    {
        Destroy(obj);
        Destroy(gameObject);
    }
}