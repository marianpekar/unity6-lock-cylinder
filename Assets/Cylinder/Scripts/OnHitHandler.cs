using System;
using UnityEngine;

public class OnHitHandler : MonoBehaviour
{
    private Action _callback;

    public void SetCallback(Action callback)
    {
        _callback = callback;
    }
    
    public void Invoke()
    {
        _callback.Invoke();
    }
}
