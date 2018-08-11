using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public static EventManager Instance = null;

    [System.Serializable] public class DoubleFloatEvent : UnityEvent<float, float> { }

    public UnityEvent<float, float> OnDirectionInputChanged;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
        OnDirectionInputChanged = new DoubleFloatEvent();
    }
}
