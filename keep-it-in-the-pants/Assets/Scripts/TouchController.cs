using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    private Vector3 lastTouchPosition;

    [SerializeField] private float dragThreshold;
    [SerializeField] private float dragThresholdX;
    [SerializeField] private float dragThresholdY;

	void Start () {
        lastTouchPosition = Vector3.zero;
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            lastTouchPosition = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {
            Vector3 newTouchPosition = Input.mousePosition;
            Vector3 inputDiff = newTouchPosition - lastTouchPosition;

            if(inputDiff.magnitude > dragThreshold) {
                lastTouchPosition = newTouchPosition;
                float x = Mathf.Abs(inputDiff.x) > dragThresholdX ? inputDiff.x : 0.0f;
                float y = Mathf.Abs(inputDiff.y) > dragThresholdY ? inputDiff.y : 0.0f;

                EventManager.Instance.OnDirectionInputChanged.Invoke(x, y);
            }
        }
	}
}
