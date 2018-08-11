using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Transform Transform;
	[SerializeField] private ParticleSystem psJizz;

	[SerializeField] private float speedMultiplier;
    [SerializeField] private float angleMultiplier;
    [SerializeField] private float rotationSpeedMultiplier;
    private Vector3 targetRotationEuler;
    private Quaternion targetRotation;
    private float lastTimePositionChanged;

	void Start () {
        EventManager.Instance.OnDirectionInputChanged.AddListener(HandleDirectionInputChange);
        targetRotationEuler = Transform.rotation.eulerAngles;
        targetRotation = Transform.rotation;
    }
	
	void FixedUpdate () {
        Transform.rotation = Quaternion.Slerp(Transform.rotation, targetRotation, rotationSpeedMultiplier * Time.deltaTime);
        Transform.position += Transform.forward * speedMultiplier * Time.deltaTime;

        if(Time.time > lastTimePositionChanged + GameManager.Instance.positionSendingInterval) {
            EventManager.Instance.OnPlayerPositionChanged.Invoke(Transform);
            lastTimePositionChanged = Time.time;
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
	}
}
