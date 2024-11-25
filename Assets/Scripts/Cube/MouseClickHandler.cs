using System;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    public event Action CubeWasTaped;
    
    [SerializeField] private Camera _camera;

    private bool _canClick = true;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (_canClick)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 50f)) return;

            if (hit.transform.CompareTag("Cube"))
            {
                var cubeMover = hit.transform.GetComponent<Cube.Cube>();
                if (cubeMover.IsMoving) return;
                
                cubeMover.TryMove();
                CubeWasTaped?.Invoke();
            }
        }
    }

    public void ClickEnabled(bool onOff)
    {
        _canClick = onOff;
    }
    

}