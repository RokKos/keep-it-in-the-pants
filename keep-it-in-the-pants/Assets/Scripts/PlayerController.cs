using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Transform Transform;
	[SerializeField] private ParticleSystem psJizz;
	[SerializeField] private Text txtLenghtDick;

	[SerializeField] private float speedMultiplier;
    [SerializeField] private float angleMultiplier;
    [SerializeField] private float rotationSpeedMultiplier;
    [SerializeField] private GameObject dick;

    private Vector3 targetRotationEuler;
    private Quaternion targetRotation;
    private float lastTimePositionChanged;

	private float lenghtDick = 0.0f;
	private const float kBodyRatioToUnits = 1.85f / 42.0f; // in meters
	private bool dickMoving = true;


    void Start () {
        SkinnedMeshRenderer mr = dick.GetComponent<SkinnedMeshRenderer>();
        mr.material = GameManager.Instance.skinColor;
        EventManager.Instance.OnDirectionInputChanged.AddListener(HandleDirectionInputChange);
        targetRotationEuler = Transform.rotation.eulerAngles;
        targetRotation = Transform.rotation;
		lenghtDick = 0.0f;
		if(txtLenghtDick != null) txtLenghtDick.enabled = false;
    }
	
	void FixedUpdate () {
		if (!dickMoving) {
			return;
		}

        Transform.rotation = Quaternion.Slerp(Transform.rotation, targetRotation, rotationSpeedMultiplier * Time.deltaTime);
		Vector3 movemntDir = Transform.forward * speedMultiplier * Time.deltaTime;
		lenghtDick += movemntDir.magnitude;
		Transform.position += movemntDir;

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
		txtLenghtDick.text = "Your dick length was: " + (lenghtDick * kBodyRatioToUnits * 100).ToString() + "cm";
		txtLenghtDick.enabled = true;
		dickMoving = false;


	}
}
