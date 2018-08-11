using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawningScript : MonoBehaviour {

    public Vector3[] testPoints;
    public int numberOfVertices = 5;
    public float grith = 5f;
    public Material snakeMaterial;

    private float piValue;

    private List<Vector3> meshVertiecesList = new List<Vector3>();
    private List<int> meshTriangles = new List<int>();

	// Use this for initialization
	void Start () {
        if (this.numberOfVertices % 2 != 0) this.numberOfVertices += 1;

        piValue = Mathf.PI * 2 / this.numberOfVertices;
        this.meshVertiecesList.Add(this.testPoints[0]);
        this.GenerateNewVertices();
        this.GenerateTriangles();
        this.PrintList();
        Debug.Log("Count " + this.meshTriangles.Count);
        this.AddMeshToGO();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void AddMeshToGO() {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
 
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.name = "ThickBlackSnake";
        meshFilter.mesh.vertices = this.meshVertiecesList.ToArray();
        meshFilter.mesh.triangles = this.meshTriangles.ToArray();
        meshFilter.mesh.RecalculateNormals();

        meshRenderer.material = this.snakeMaterial;

    }

    void PrintList() {
        foreach(Vector3 e in this.meshVertiecesList) {
            Debug.Log(e);
        }
        foreach(int e in this.meshTriangles) {
            Debug.Log(e);
        }
    }

    void GenerateNewVertices() {
        foreach(Vector3 loc in testPoints) {
            for(int i = 0; i < this.numberOfVertices; i++) {
                Vector3 newVertex = loc;

                newVertex += Mathf.Sin(this.piValue * i) * gameObject.transform.up;
                newVertex += Mathf.Cos(this.piValue * i) * gameObject.transform.right;
                meshVertiecesList.Add(newVertex);
            }
        }
        this.meshVertiecesList.Add(this.testPoints[this.testPoints.Length - 1]);
    }

    void GenerateTriangles() {
        this.GenerateFace(true);
        for(int i = 0; i < this.testPoints.Length - 1; i++) {
            this.GenerateSide(i * this.numberOfVertices + 1);
        }
        this.GenerateFace(false);
    }
    void GenerateSide(int startIndex) {
        Debug.Log("side is being generated with start index of: " + startIndex);
        for(int i = 0; i < this.numberOfVertices; i += 2) {
            int v1 = startIndex + i;
            int v2 = startIndex + i + this.numberOfVertices;
            int v3 = startIndex + i + 1;
            int v4 = startIndex + i + 1 + this.numberOfVertices;
            int v5 = i != this.numberOfVertices - 2 ? startIndex + i + 2 : startIndex;
            int v6 = i != this.numberOfVertices - 2 ? startIndex + i + 2 + this.numberOfVertices: startIndex + this.numberOfVertices;


            this.meshTriangles.Add(v1);
            this.meshTriangles.Add(v4);
            this.meshTriangles.Add(v2);

            this.meshTriangles.Add(v4);
            this.meshTriangles.Add(v1);
            this.meshTriangles.Add(v3);


            this.meshTriangles.Add(v3);
            this.meshTriangles.Add(v6);
            this.meshTriangles.Add(v4);

            this.meshTriangles.Add(v6);
            this.meshTriangles.Add(v3);
            this.meshTriangles.Add(v5);
        }
    }

    void GenerateFace(bool first) {
        if(first) {
            for (int i = 0; i < this.numberOfVertices; i++) {
                this.meshTriangles.Add(1 + i);
                this.meshTriangles.Add(0);
                this.meshTriangles.Add(i == this.numberOfVertices - 1 ? 1 :  i + 2);
            }
            return;
        }
        for (int i = 0; i < this.numberOfVertices; i++) {
            this.meshTriangles.Add(i == this.numberOfVertices - 1 ? this.numberOfVertices * (this.testPoints.Length - 1) + 1 : this.numberOfVertices * (this.testPoints.Length - 1) + 2 + i);
            this.meshTriangles.Add(this.meshVertiecesList.Count - 1);
            this.meshTriangles.Add(this.numberOfVertices * (this.testPoints.Length - 1) + 1 + i);
        }
    }
}
