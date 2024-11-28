using Cube;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private bool _canClick = true;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (_canClick)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 50f)) return;

            if (hit.transform.CompareTag(GlobalConstants.CUBE_TAG))
            {
                var cube = hit.transform.GetComponent<IMover>();
                if (cube.IsMoving)
                {
                    return;
                }
                
                cube.TryMove();
            }
        }
    }

    public void ClickEnabled(bool onOff)
    {
        _canClick = onOff;
    }
    

}