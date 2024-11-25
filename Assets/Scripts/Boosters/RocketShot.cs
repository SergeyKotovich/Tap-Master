using System.Collections;
using UnityEngine;

public class RocketShot : MonoBehaviour
{
    [SerializeField] private GameObject _objectToShoot; // Префаб объекта, который вы будете стрелять
    [SerializeField] private int _timerToDestroy; // Таймер для отключения объекта лазер 7
    [SerializeField] private float _moveSpeed; // Скорость полета снаряда
    [SerializeField] private MouseClickHandler _mouseClickHandler; // Выкл мышки не знаю надо или нет?

    public bool CanShoot; // Флаг, позволяющий выполнять выстрел
    private SoundsManager _soundsManager; // Звук
    
    void Update()
    {
        if (CanShoot)
        {
            GameObject shotObject = Instantiate(_objectToShoot, transform.position, Quaternion.identity);
            
            // Получаем компонент Rigidbody объекта выстрела
            Rigidbody rb = shotObject.GetComponent<Rigidbody>();

            // Двигаем все дочерние объекты вместе с основным объектом
            foreach (Transform child in shotObject.transform)
            {
                Rigidbody childRb = child.GetComponent<Rigidbody>();
                if (childRb != null)
                {
                    childRb.velocity = transform.forward * _moveSpeed;
                }
            }

            if (rb != null)
            {
                // Применяем силу в заданном направлении
                rb.velocity = transform.forward * _moveSpeed;
            }
            
            // Запускаем корутину с задержкой
            StartCoroutine(DestroyObjectAfterDelay(shotObject, _timerToDestroy));
            
            _soundsManager.PlayShotRocket(); // звук выстрела
            
            CanShoot = false;
        }
    }

    IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // Ждем указанное количество секунд

        // Уничтожаем объект
        Destroy(obj);
    }

    public void ActiveShot()
    {
        if (CanShoot)
        {
            return;
        }
        
        CanShoot = true;
    }
}