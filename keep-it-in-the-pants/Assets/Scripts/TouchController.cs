using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    private Vector3 lastTouchPosition;

	void Start () {
        lastTouchPosition = Vector3.zero;
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            lastTouchPosition = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {
            Vector3 newTouchPosition = Input.mousePosition;
            Vector3 inputDiff = newTouchPosition - lastTouchPosition;
            lastTouchPosition = newTouchPosition;

            EventManager.Instance.OnDirectionInputChanged.Invoke(inputDiff);
        }
	}
}
