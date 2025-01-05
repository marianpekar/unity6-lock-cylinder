using UnityEngine;
using UnityEngine.Events;

public class Lock : MonoBehaviour
{
    public int triggerValue = 4096;
    public int value = 256;

    public UnityEvent onTriggerValueEnter;
    public UnityEvent onTriggerValueExit;
    private bool _wasTriggerValueEntered;

    public void Awake()
    {
        var materialsSwitcher = GetComponent<MaterialsSwitcher>();
        
        Cylinder[] cylinders = GetComponentsInChildren<Cylinder>();

        for (int i = 0; i < cylinders.Length; i++)
        {
            int digit = (value / (int)Mathf.Pow(10, i)) % 10;
            cylinders[i].SetParentLock(this, i, digit);
            cylinders[i].AddRendererTo(materialsSwitcher);
        }
    }

    public void SetDigit(int digit, int order)
    {
        int divisor = (int)Mathf.Pow(10, order);
        int rightPart = value % divisor;
        int leftPart = value / (divisor * 10);

        value = leftPart * (divisor * 10) + digit * divisor + rightPart;

        if (value == triggerValue)
        {
            _wasTriggerValueEntered = true;
            onTriggerValueEnter?.Invoke();
        }
        else if (_wasTriggerValueEntered && value != triggerValue)
        {
            _wasTriggerValueEntered = false;
            onTriggerValueExit?.Invoke();
        }
    }
}