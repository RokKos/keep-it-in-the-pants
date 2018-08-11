using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseNormals : MonoBehaviour {

	[SerializeField]
	private MeshFilter meshFilter;


	[SerializeField]
	private SpawningController spawningController;

	void Start () {
		
		if (meshFilter != null) {
			Mesh mesh = meshFilter.mesh;

			Vector3[] normals = mesh.normals;
			for (int i = 0; i < normals.Length; i++) {
				normals[i] = -normals[i];
			}
			mesh.normals = normals;

			for (int m = 0; m < mesh.subMeshCount; m++) {
				int[] triangles = mesh.GetTriangles(m);
				for (int i = 0; i < triangles.Length; i += 3) {
					int temp = triangles[i];
					triangles[i] = triangles[i + 1];
					triangles[i + 1] = temp;
				}
				mesh.SetTriangles(triangles, m);
			}
		}

		float radius = spawningController.GetPlayingRadius() * 2;
		transform.localScale = new Vector3(radius, radius, radius);
	}

	// Update is called once per frame
	void Update () {
		
	}


}
