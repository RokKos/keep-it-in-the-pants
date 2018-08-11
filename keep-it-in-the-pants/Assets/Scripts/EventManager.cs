using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public static EventManager Instance = null;

    [System.Serializable] public class DoubleFloatEvent : UnityEvent<float, float> { }
    [System.Serializable] public class TransformEvent : UnityEvent<Transform> { }
    [System.Serializable] public class BoolEvent : UnityEvent<bool> { }

    public UnityEvent<float, float> OnDirectionInputChanged;
    public UnityEvent<Transform> OnPlayerPositionChanged;
    public UnityEvent<bool> OnChangeControlAvailaility;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
        OnDirectionInputChanged = new DoubleFloatEvent();
        OnPlayerPositionChanged = new TransformEvent();
        OnChangeControlAvailaility = new BoolEvent();
    }
}
