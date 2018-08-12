using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JostickController : MonoBehaviour {

    private Vector3 lastTouchPosition;
    [Range(0, 100f)]
    [SerializeField] private float joystickRadiusProcentage;
    [SerializeField] private float joystickThresholdX;
    [SerializeField] private float joystickThresholdY;
    [SerializeField] private bool notNormalControls = true;

    private float joystickRadius;

    private bool controlsEnabled;

    void Start () {
        lastTouchPosition = Vector3.zero;
        joystickRadius = Screen.width * (joystickRadiusProcentage / 100);
        controlsEnabled = true;

        EventManager.Instance.OnChangeControlAvailaility.AddListener(SetControls);
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            SceneManager.LoadScene(2);
        }
        if (!controlsEnabled) {
            lastTouchPosition = Vector3.zero;
            return;
        }
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
            if (notNormalControls) y *= -1;
            EventManager.Instance.OnDirectionInputChanged.Invoke(x, y);
        }
    }

    void SetControls(bool enabled) {
        controlsEnabled = enabled;
        if (enabled) {
            if (Input.GetMouseButton(0)) {
                lastTouchPosition = Input.mousePosition;
            }
        }
    }
}
