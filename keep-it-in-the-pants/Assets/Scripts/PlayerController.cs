using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Transform Transform;
	[SerializeField] private ParticleSystem psJizz;
	[SerializeField] private Text txtLenghtDick;
	[SerializeField] GameManager gameManager;

	[SerializeField] private float speedMultiplier;
    [SerializeField] private float angleMultiplier;
    [SerializeField] private float rotationSpeedMultiplier;
    private Vector3 targetRotationEuler;
    private Quaternion targetRotation;
    private float lastTimePositionChanged;

	private float lenghtDick = 0.0f;
	private const float kBodyRatioToUnits = 1.85f / 42.0f; // in meters
	private bool dickMoving = true;

    [Header("Flipping the dick")]
    [SerializeField] private float maxXAngle;
    [SerializeField] private float flipDuration;
    private float lerpProgress;
    private bool dickFlipping;

    void Start () {
        EventManager.Instance.OnDirectionInputChanged.AddListener(HandleDirectionInputChange);
        targetRotationEuler = Transform.rotation.eulerAngles;
        targetRotation = Transform.rotation;
		lenghtDick = 0.0f;
		txtLenghtDick.enabled = false;
		gameManager.ChangeCameras(true);
        dickFlipping = false;
    }
	
	void FixedUpdate () {
        
        if (!dickMoving) {
            return;
        }

        if (dickFlipping) {
            Transform.rotation = Quaternion.Slerp(Transform.rotation, targetRotation, lerpProgress);
            if (lerpProgress < 1) {
                lerpProgress += Time.deltaTime / flipDuration;
            }
            else {
                targetRotationEuler = Transform.rotation.eulerAngles;
                targetRotation = Transform.rotation;
                dickFlipping = false;
                EventManager.Instance.OnChangeControlAvailaility.Invoke(true);
            }
        }
        else {
            Transform.rotation = Quaternion.Slerp(Transform.rotation, targetRotation, rotationSpeedMultiplier * Time.deltaTime);
        }
        Vector3 movemntDir = Transform.forward * speedMultiplier * Time.deltaTime;
        lenghtDick += movemntDir.magnitude;
        Transform.position += movemntDir;

        if (Time.time > lastTimePositionChanged + GameManager.Instance.positionSendingInterval) {
            EventManager.Instance.OnPlayerPositionChanged.Invoke(Transform);
            lastTimePositionChanged = Time.time;
        }
        //Debug.Log(Mathf.Abs(Transform.rotation.eulerAngles.x));
        if (CheckForAngleFlip()) {
            HandleDickFlipping();
        }
    }

    private void HandleDirectionInputChange(float x, float y) {
        Vector3 diff = new Vector3(y * angleMultiplier, x * angleMultiplier, 0.0f);
        targetRotationEuler += diff;
        targetRotation = Quaternion.Euler(targetRotationEuler);
    }

	private void OnCollisionEnter (Collision collision) {
		Debug.Log("Hit");

		psJizz.Play();
		txtLenghtDick.text = "Your dick length was: " + (lenghtDick * kBodyRatioToUnits * 100).ToString() + "cm";
		txtLenghtDick.enabled = true;
		dickMoving = false;
	}

	gameManager.ChangeCameras(false);
    private void HandleDickFlipping() {
        if (dickFlipping) return;

        Debug.Log(transform.rotation.eulerAngles);
        dickFlipping = true;
        var z = IsFlipped() ? 0.0f : Transform.rotation.eulerAngles.z;
        var yOffset = IsFlipped() ? 0.0f : -180.0f;
        targetRotationEuler = new Vector3(0.0f, Transform.rotation.eulerAngles.y + yOffset, z);
        targetRotation = Quaternion.Euler(targetRotationEuler);
        lerpProgress = 0;
        EventManager.Instance.OnChangeControlAvailaility.Invoke(false);
    }

    private bool CheckForAngleFlip() {
        float dotProduct = Vector3.Dot(Vector3.up, Transform.forward);
        float angle = Mathf.Acos(dotProduct);
        return angle < (Mathf.Deg2Rad * maxXAngle);
    }

    private bool IsFlipped() {
        float dotProduct = Vector3.Dot(Vector3.up, Transform.up);
        return dotProduct < 0.0f;
    }
}
