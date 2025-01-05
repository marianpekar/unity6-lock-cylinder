using System.Collections;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public GameObject cylinder;
    public OnHitHandler handlerUp;
    public OnHitHandler handlerDown;
    public float scrollTimeInSeconds = 0.33f;
    private bool _isRotating;
    private int _value;
    private Lock _parentLock;
    private int _order;

    public void SetParentLock(Lock parentLock, int order, int value)
    {
        _parentLock = parentLock;
        _order = order;
        _value = value;
        
        cylinder.transform.rotation *= Quaternion.Euler(0, -value * 36, 0);
    }
    
    public void Awake()
    {
        handlerUp.SetCallback(ScrollUp);
        handlerDown.SetCallback(ScrollDown);
    }

    private void ScrollDown()
    {
        if (_isRotating)
            return;

        StartCoroutine(RotateCylinder(36));
    }

    private void ScrollUp()
    {
        if (_isRotating)
            return;

        StartCoroutine(RotateCylinder(-36));
    }

    private IEnumerator RotateCylinder(float angle)
    {
        _isRotating = true;
        float elapsed = 0f;
        Quaternion initialRotation = cylinder.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, angle, 0);

        while (elapsed < scrollTimeInSeconds)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / scrollTimeInSeconds;
            cylinder.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            yield return null;
        }

        // Ensure final position
        cylinder.transform.rotation = targetRotation; 

        _value -= (int)(angle / 36);
        if (_value > 9) _value = 0;
        if (_value < 0) _value = 9;

        _parentLock.SetDigit(_value, _order);
        
        _isRotating = false;
    }

    public void AddRendererTo(MaterialsSwitcher materialsSwitcher)
    {
        var r = cylinder.GetComponent<MeshRenderer>();
        materialsSwitcher.AddRenderer(r);
    }
}