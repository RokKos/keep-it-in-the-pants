using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Transform dickTrans;

    private float lastInput;
    private float cameraDelay;
    private float cameraAxisRotationX;
    private float cameraAxisRotationY;
    private float cameraRotationSpeed;
    private float cameraCorrectionSpeedFactor;
    private Vector3 startingRot;
    private bool fixRotation = false;

	// Use this for initialization
	void Start () {
        EventManager.Instance.OnDirectionInputChanged.AddListener(MoveCamera);
        lastInput = Time.time;

        cameraDelay = GameManager.Instance.cameraDelay;
        cameraAxisRotationX = GameManager.Instance.cameraAxisRotationX;
        cameraAxisRotationY = GameManager.Instance.cameraAxisRotationY;
        cameraRotationSpeed = GameManager.Instance.cameraMoveSpeed;
        cameraCorrectionSpeedFactor = GameManager.Instance.cameraCorrectionSpeedFactor;

        startingRot = transform.localRotation.eulerAngles;
        //startingRot = Vector3.zero;
	}
    private void Update() {
        if(Time.time - lastInput > cameraDelay && VectorDif()) {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(startingRot), cameraRotationSpeed * cameraCorrectionSpeedFactor * Time.deltaTime);

        }
    }

    bool VectorDif() {
        Vector3 dif = transform.localEulerAngles - startingRot;
        if (dif.sqrMagnitude < 0.1f) return false; else return true;
    }

    void MoveCamera(float x, float y) {
        if (dickTrans.rotation.eulerAngles.x < -90) {
            Debug.Log("multiplaying with -1");
            x *= -1;
        }
        else Debug.Log(dickTrans.rotation.eulerAngles);
        Vector3 targetRotation = transform.localRotation.eulerAngles;
        if (targetRotation.x > 180) targetRotation.x -= 360;
        if (targetRotation.y > 180) targetRotation.y -= 360;

        targetRotation.x += y *  cameraRotationSpeed;
        targetRotation.y += x *  cameraRotationSpeed;

        if (Mathf.Abs(targetRotation.x - startingRot.x) > cameraAxisRotationX) {
            targetRotation.x = targetRotation.x > startingRot.x ? startingRot.x + cameraAxisRotationX : startingRot.x - cameraAxisRotationX;


        }
        if (Mathf.Abs(targetRotation.y - startingRot.y) > cameraAxisRotationY) {
            targetRotation.y = targetRotation.y > startingRot.y ? startingRot.y + cameraAxisRotationY : startingRot.y - cameraAxisRotationY;

        }
        transform.localEulerAngles = targetRotation; //Quaternion.Euler(targetRotation);
        lastInput = Time.time;
    }
}
