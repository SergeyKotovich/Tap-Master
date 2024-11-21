using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private GameObject _objectToShoot; // Префаб объекта, который вы будете стрелять
    [SerializeField] private float _shootForce; // Сила выстрела 30
    [SerializeField] private int _maxShots; // Максимальное количество выстрелов 1
    [SerializeField] private int _timerToDestroy; // Таймер для отключения объекта лазер 7
    [SerializeField] private MouseClickHandler _mouseClickHandler;
    
    public bool CanShoot; // Флаг, позволяющий выполнять выстрел
    private int _shotsRemaining; // Количество оставшихся выстрелов
    private SoundsManager _soundsManager;
    private ScreensController.ScreensController _screensController;
    
    private void Start()
    {
        _shotsRemaining = _maxShots; // Устанавливаем начальное количество выстрелов
        _soundsManager = Container.Instance.SoundsManager; // звук
        
        // стоит ли брать ScreensController ради того что бы узнавать IsAnyWindowOpened
        _screensController = Container.Instance.ScreensController;
    }

    private void Update()
    {
        if (CanShoot)
        {
            _mouseClickHandler.ClickEnabled(false);
        }
        // Проверяем нажатие кнопки мыши и возможность стрелять
        if (Input.GetMouseButtonDown(0) && CanShoot && _shotsRemaining > 0 && !_screensController.IsAnyWindowOpened)
        {

            // Создаем луч из позиции мыши в мировом пространстве
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, попал ли луч в какой-либо объект
            if (Physics.Raycast(ray, out hit))
            {
                // Если луч попал в объект, создаем выстрел
                GameObject shotObject = Instantiate(_objectToShoot, transform.position, Quaternion.identity);

                // Запускаем корутину с задержкой в 3 секунды
                StartCoroutine(DestroyObjectAfterDelay(shotObject, _timerToDestroy));

                // Определяем направление выстрела в сторону попадания луча
                Vector3 shootDirection = (hit.point - transform.position).normalized;

                // Получаем компонент Rigidbody объекта, чтобы применить к нему силу
                Rigidbody rb = shotObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Применяем силу в заданном направлении
                    rb.AddForce(shootDirection * _shootForce, ForceMode.Impulse);

                    // Уменьшаем количество оставшихся выстрелов
                    _shotsRemaining--;
                    
                    _soundsManager.PlayShotLaser(); // звук выстрела
                }

            }
        }

        // Проверяем, достигнуто ли максимальное количество выстрелов
        if (_shotsRemaining <= 0 && CanShoot)
        {
            _mouseClickHandler.ClickEnabled(true);
            CanShoot = false; // Отключаем возможность выстрелов
        }
    }

    IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // Ждем указанное количество секунд

        // Уничтожаем объект
        Destroy(obj);
    }

    // Активируем кнопкой бустер и восстанавливаем заряды и флаги
    public void ActiveShot()
    {
        if (CanShoot)
        {
            return;
        }
        _mouseClickHandler.ClickEnabled(false);
        CanShoot = true;
        _shotsRemaining = _maxShots;
    }
}