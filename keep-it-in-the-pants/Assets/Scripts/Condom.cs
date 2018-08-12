using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condom : MonoBehaviour {

	[SerializeField] private SpawningController spawningController;


	private void OnCollisionEnter (Collision collision) {
		spawningController.RepositionCondom();
	}
}
