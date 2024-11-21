using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed; // Скорость зума - пока 25
    [SerializeField] private float swipePanSpeed; // Скорость сдвига при свайпе - пока 10

    public float minFOV; // Минимальное поле зрения - 60
    public float maxFOV; // Максимальное поле зрения -110

    private Camera cam;
    private float initialPinchDistance; // Начальное расстояние между пальцами

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // Обработка зума с помощью колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView += -scroll * zoomSpeed;

        // Ограничение диапазона полей зрения
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);

        // Обработка свайпов для сдвига и раздвигания двух пальцев на смартфоне
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                float currentPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                float deltaDistance = currentPinchDistance - initialPinchDistance;

                // Сдвиг камеры при раздвигании и сближении пальцев
                cam.transform.Translate(cam.transform.forward * deltaDistance * swipePanSpeed * Time.deltaTime, Space.World);

                initialPinchDistance = currentPinchDistance;
            }
        }
    }
}