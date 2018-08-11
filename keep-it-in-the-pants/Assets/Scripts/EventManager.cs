using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public static EventManager Instance = null;

    [System.Serializable] public class DoubleFloatEvent : UnityEvent<float, float> { }
    [System.Serializable] public class Vector3Event : UnityEvent<Vector3> { }

    public UnityEvent<float, float> OnDirectionInputChanged;
    public UnityEvent<Vector3> OnPlayerPositionChanged;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
        OnDirectionInputChanged = new DoubleFloatEvent();
        OnPlayerPositionChanged = new Vector3Event();
    }
}
