using UnityEngine;

public class Raycaster : MonoBehaviour
{
    Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;
        
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) 
            return;
        
        var onHitHandler = hit.collider.gameObject.GetComponent<OnHitHandler>();
        if (!onHitHandler)
            return;
        
        onHitHandler.Invoke();
    }
}