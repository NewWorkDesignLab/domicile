using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnablePassthrough : MonoBehaviour
{
    public event System.Action<GameObject> OnEnableEvent;
    public event System.Action<GameObject> OnDisableEvent;

    void OnEnable()
    {
        OnEnableEvent?.Invoke(this.gameObject);
    }

    void OnDisable()
    {
        OnDisableEvent?.Invoke(this.gameObject);
    }
}
