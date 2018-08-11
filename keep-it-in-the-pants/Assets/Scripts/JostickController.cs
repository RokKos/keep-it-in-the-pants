using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JostickController : MonoBehaviour {

    private Vector3 lastTouchPosition;
    [Range(0, 100f)]
    [SerializeField] private float joystickRadiusProcentage;
    [SerializeField] private float joystickThresholdX;
    [SerializeField] private float joystickThresholdY;
    [SerializeField] private bool normalControls = true;

    private float joystickRadius;

    void Start () {
        lastTouchPosition = Vector3.zero;
        joystickRadius = Screen.width * (joystickRadiusProcentage / 100);
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) {
            Vector3 newTouchPosition = Input.mousePosition;
            Vector3 inputDiff = newTouchPosition - lastTouchPosition;
            var x = 0.0f;
            if(Mathf.Abs(inputDiff.x) > joystickThresholdX) {
                x = inputDiff.x / joystickRadius;
                if(x > 1) {
                    x = 1;
                } else if(x < -1) {
                    x = -1;
                }

            }
            var y = 0.0f;
            if(Mathf.Abs(inputDiff.y) > joystickThresholdY) {
                y = inputDiff.y / joystickRadius;
                if (y > 1) {
                    y = 1;
                } else if (y < -1) {
                    y = -1;
                }
            }
            if (normalControl) y *= -1;
            EventManager.Instance.OnDirectionInputChanged.Invoke(x, y);
        }
    }
}
