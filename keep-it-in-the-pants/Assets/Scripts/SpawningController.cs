using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningController : MonoBehaviour {

	[SerializeField]
	private Vector3 spawnOrigin = Vector3.zero;

	[SerializeField]
	[Range(0, 30)]
	private float spawnRadius = 4.0f;

	[SerializeField]
	[Range(0, 360)]
	private float spawnAngleTheta = 90.0f;

	[SerializeField]
	[Range(0, 180)]
	private float spawnAngleFi = 90.0f;

	[SerializeField]
	[Range(0, 300)]
	private int spawnCount = 50;

	[SerializeField]
	private GameObject prefab;

	[SerializeField]
	private Transform gameTransform;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < spawnCount; ++i) {
			Vector3 pos = SelectRandomCordinate();
			Instantiate(prefab, pos, Quaternion.identity, gameTransform);
		} 

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector3 SelectRandomCordinate () {
		// http://mathworld.wolfram.com/SphericalCoordinates.html
		

		float r = Random.Range(0, spawnRadius);
		float theta = Random.Range(0, spawnAngleTheta);
		float fi = Random.Range(0, spawnAngleFi);

		float x = r * Mathf.Cos(Mathf.Deg2Rad * theta) * Mathf.Sin(Mathf.Deg2Rad * fi);
		float y = r * Mathf.Sin(Mathf.Deg2Rad * theta) * Mathf.Sin(Mathf.Deg2Rad * fi);
		float z = r * Mathf.Cos(Mathf.Deg2Rad * fi);


		return new Vector3(x,y,z);

	}
}
