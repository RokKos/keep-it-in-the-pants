using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawningScript : MonoBehaviour {

    public Vector3[] testPoints;
    public int numberOfVertices = 5;
    public float grith = 5f;
    public Material snakeMaterial;

    private float piValue;

    private List<Vector3> playerLocations = new List<Vector3>();
    private List<Vector3> meshVertiecesList = new List<Vector3>();
    private List<int> meshTriangles = new List<int>();

    private MeshFilter snakeMeshFilter = null;
    private MeshRenderer snakeMeshRenderer = null;

    // Use this for initialization
    void Start() {
        snakeMeshFilter = gameObject.AddComponent<MeshFilter>();
        snakeMeshRenderer = gameObject.AddComponent<MeshRenderer>();

        EventManager.Instance.OnPlayerPositionChanged.AddListener(PositionChanged);
        piValue = Mathf.PI * 2 / this.numberOfVertices;
        if (this.numberOfVertices % 2 != 0) this.numberOfVertices += 1;

    }

    void PositionChanged(Vector3 pos) {
        this.playerLocations.Add(pos);
        this.GenerateNewVertices();
        this.GenerateTriangles();
        this.AddMeshToGO();
    }

    void AddMeshToGO() {
        snakeMeshFilter = gameObject.GetComponent<MeshFilter>();
        snakeMeshRenderer = gameObject.GetComponent<MeshRenderer>();

        snakeMeshFilter.mesh = new Mesh();
        snakeMeshFilter.mesh.name = "ThickBlackSnake";
        snakeMeshFilter.mesh.vertices = this.meshVertiecesList.ToArray();
        snakeMeshFilter.mesh.triangles = this.meshTriangles.ToArray();
        snakeMeshFilter.mesh.RecalculateNormals();

        snakeMeshRenderer.material = this.snakeMaterial;

    }

    void PrintList() {
        foreach (Vector3 e in this.meshVertiecesList) {
            Debug.Log(e);
        }
        foreach (int e in this.meshTriangles) {
            Debug.Log(e);
        }
    }

    void GenerateNewVertices() {
        if (this.playerLocations.Count == 1) this.meshVertiecesList.Add(this.playerLocations[0]);
        for (int i = 0; i < this.numberOfVertices; i++) {
            Vector3 newVertex = this.playerLocations[this.playerLocations.Count - 1];

            newVertex += Mathf.Sin(this.piValue * i) * gameObject.transform.up;
            newVertex += Mathf.Cos(this.piValue * i) * gameObject.transform.right;
            meshVertiecesList.Add(newVertex);
        }
    }

    void GenerateTriangles() {
        if (this.playerLocations.Count == 1) {
            this.GenerateFace(true);
            return;
        }
        this.GenerateSide((this.playerLocations.Count - 2) * this.numberOfVertices + 1);
        //this.GenerateFace(false);
    }
    void GenerateSide(int startIndex) {
        Debug.Log("side is being generated with start index of: " + startIndex);
        for (int i = 0; i < this.numberOfVertices; i += 2) {
            int v1 = startIndex + i;
            int v2 = startIndex + i + this.numberOfVertices;
            int v3 = startIndex + i + 1;
            int v4 = startIndex + i + 1 + this.numberOfVertices;
            int v5 = i != this.numberOfVertices - 2 ? startIndex + i + 2 : startIndex;
            int v6 = i != this.numberOfVertices - 2 ? startIndex + i + 2 + this.numberOfVertices : startIndex + this.numberOfVertices;


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
        if (first) {
            for (int i = 0; i < this.numberOfVertices; i++) {
                this.meshTriangles.Add(1 + i);
                this.meshTriangles.Add(0);
                this.meshTriangles.Add(i == this.numberOfVertices - 1 ? 1 : i + 2);
            }
            return;
        }
        for (int i = 0; i < this.numberOfVertices; i++) {
            this.meshTriangles.Add(i == this.numberOfVertices - 1 ? this.numberOfVertices * (this.playerLocations.Count - 1) + 1 : this.numberOfVertices * (this.playerLocations.Count - 1) + 2 + i);
            this.meshTriangles.Add(this.meshVertiecesList.Count - 1);
            this.meshTriangles.Add(this.numberOfVertices * (this.playerLocations.Count - 1) + 1 + i);
        }
    }
}
