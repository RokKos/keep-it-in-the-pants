using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterModelController : MonoBehaviour {

	[SerializeField] private GameManager gameManager;
	[SerializeField] private MeshRenderer bodyMesh;

	// Use this for initialization
	void Start () {
		bodyMesh.material = gameManager.skinColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
