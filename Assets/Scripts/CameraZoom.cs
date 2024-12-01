using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed; 
    [SerializeField] private float swipePanSpeed; 

    public float minFOV; 
    public float maxFOV; 

    private Camera cam;
    private float initialPinchDistance; 

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
       
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView += -scroll * zoomSpeed;
        
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);
        
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
                
                cam.transform.Translate(cam.transform.forward * deltaDistance * swipePanSpeed * Time.deltaTime, Space.World);

                initialPinchDistance = currentPinchDistance;
            }
        }
    }
}