using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public static EventManager Instance = null;

    public UnityEvent<Vector3> OnDirectionInputChanged;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
    }
}
