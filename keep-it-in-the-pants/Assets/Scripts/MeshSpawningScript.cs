﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawningScript : MonoBehaviour {

    public int chunkSize = 10;
    public int numberOfVertices = 5;
    public int bufferSize = 10;
    private float piValue;

    private List<Vector3> playerLocations = new List<Vector3>();
    private List<Vector3> meshVertiecesList = new List<Vector3>();
    private List<int> meshTriangles = new List<int>();
    private Material snakeMaterial;
    private GameObject currentSection;
    private int chunkCounter = 0;
    private int buffer = 0;
    private float grith;
    private Vector3[] posBuffer;
    private Vector3[] upBuffer;
    private Vector3[] rightBuffer;
    private Transform[] transofrmBuffer;

    private int trianglesCreated = 0;


    private MeshFilter snakeMeshFilter = null;
    private MeshRenderer snakeMeshRenderer = null;

    private bool generateFaceBool = true;

    // Use this for initialization
    void Start() {
        snakeMaterial = GameManager.Instance.skinColor;
        grith = GameManager.Instance.dickRadius;
        currentSection = new GameObject();
        currentSection.transform.parent = gameObject.transform;
        snakeMeshFilter = currentSection.AddComponent<MeshFilter>();
        snakeMeshRenderer = currentSection.AddComponent<MeshRenderer>();
        posBuffer = new Vector3[bufferSize+1];
        upBuffer = new Vector3[bufferSize+1];
        rightBuffer = new Vector3[bufferSize+1];

        EventManager.Instance.OnPlayerDeath.AddListener(SpawnBufferContent);
        EventManager.Instance.OnPlayerPositionChanged.AddListener(PositionChanged);

        piValue = Mathf.PI * 2 / this.numberOfVertices;

    }

    void SpawnBufferContent() {
        Debug.Log("Death chunk should have been spawned!");
        generateface();
        AddMeshToGO(true);
        //for(int i = 0; i < bufferSize; i++) {
        //    if (posBuffer[i] != Vector3.zero) break;
        //    ProcessNewPositions(posBuffer[i], upBuffer[i], rightBuffer[i]);
        //}
        //AddMeshToGO(true);
    }
    private int bCounter = 0;
    void PositionChanged(Transform snakeTransform) {
        ProcessNewPositions(snakeTransform.position, snakeTransform.up, snakeTransform.right);
        if (bCounter >= bufferSize) {
            AddMeshToGO(false);
            bCounter = 0;
            return;
        }
        bCounter++;
        //if(buffer < bufferSize) {
        //    posBuffer[buffer] = snakeTransform.position;
        //    upBuffer[buffer] = snakeTransform.up;
        //    rightBuffer[buffer] = snakeTransform.right;
        //    buffer++;
        //    return;
        //}
        //posBuffer[buffer] = snakeTransform.position;
        //upBuffer[buffer] = snakeTransform.up;
        //rightBuffer[buffer] = snakeTransform.right;
        //for (int i = 0; i <= this.bufferSize; i++) {
        //    ProcessNewPositions(posBuffer[i], upBuffer[i], rightBuffer[i]);
        //}
        //buffer = 0;
        //posBuffer = new Vector3[bufferSize+1];
        //upBuffer = new Vector3[bufferSize+1];
        //rightBuffer = new Vector3[bufferSize+1];
        //this.AddMeshToGO(false);

    }
    void ProcessNewPositions(Vector3 pos, Vector3 up, Vector3 right) {
        this.playerLocations.Add(pos);
        this.GenerateNewVertices(pos, up, right);
        this.GenerateTriangles();
       // this.AddMeshToGO();
        //if (this.chunkCounter == this.chunkSize) {
        //    this.chunkCounter = 0;

        //    //this.meshVertiecesList.Add(pos);
        //    //this.GenerateFace(false);
        //    this.AddMeshToGO(false);

        //    this.meshVertiecesList = new List<Vector3>();
        //    this.meshTriangles = new List<int>();
        //    this.playerLocations = new List<Vector3>();

        //    this.playerLocations.Add(pos);
        //    this.GenerateNewVertices(pos, up, right);
        //    this.GenerateTriangles();

        //    trianglesCreated = 0;

        //    currentSection = new GameObject();
        //    currentSection.transform.parent = gameObject.transform;
        //    snakeMeshFilter = currentSection.AddComponent<MeshFilter>();
        //    snakeMeshRenderer = currentSection.AddComponent<MeshRenderer>();
        //}
        this.chunkCounter++;
    }

    void AddMeshToGO(bool last) {
        snakeMeshFilter = currentSection.GetComponent<MeshFilter>();
        snakeMeshRenderer = currentSection.GetComponent<MeshRenderer>();


        snakeMeshFilter.mesh = new Mesh();
        snakeMeshFilter.mesh.name = "ThickBlackSnake";
        snakeMeshFilter.mesh.vertices = this.meshVertiecesList.ToArray();
        snakeMeshFilter.mesh.triangles = this.meshTriangles.ToArray();
        snakeMeshFilter.mesh.uv = generateUVs(last);
        snakeMeshFilter.mesh.RecalculateNormals();

        snakeMeshRenderer.material = this.snakeMaterial;

    }

    private bool first = true;

    Vector2[] generateUVs(bool last) {

        Vector2[] generatedUV = new Vector2[this.meshVertiecesList.Count];

        int size = last ? this.meshVertiecesList.Count - 1 : this.meshVertiecesList.Count;

        for (int i = 0; i < size; i += this.numberOfVertices + 1) {
            for(int j = 0; j < this.numberOfVertices + 1; j++) {
                float u = j == 0 ? 0 : (float)j / ((float)numberOfVertices - 1);
                float v = i == 0 ? 0 : (float)i / (float)(size - 1 - this.numberOfVertices - 1);
                generatedUV[i + j] = new Vector2(u, v);
            }
        }
        if (last) generatedUV[meshVertiecesList.Count - 1] = new Vector2(0.5f, 0.5f);
        return generatedUV;
    }

    void PrintList() {
        foreach (Vector3 e in this.meshVertiecesList) {
            Debug.Log(e);
        }
        foreach (int e in this.meshTriangles) {
            Debug.Log(e);
        }
    }

    void GenerateNewVertices(Vector3 pos, Vector3 up, Vector3 right) {
        //if (this.playerLocations.Count == 1) this.meshVertiecesList.Add(pos);
        for (int i = 0; i < this.numberOfVertices; i++) {
            Vector3 newVertex = this.playerLocations[this.playerLocations.Count - 1];

            newVertex += Mathf.Sin(this.piValue * i) * up * grith;
            newVertex += Mathf.Cos(this.piValue * i) * right * grith;
            meshVertiecesList.Add(newVertex);
        }
        Vector3 addOnVertex = this.playerLocations[this.playerLocations.Count - 1];
        addOnVertex += right * grith;
        meshVertiecesList.Add(addOnVertex);

    }

    void GenerateTriangles() {
        if (this.playerLocations.Count == 1) {
           // this.GenerateFace(true);
            this.generateFaceBool = false;
            return;
        }
        this.GenerateSide(trianglesCreated * (this.numberOfVertices + 1));
        trianglesCreated++;
        //this.GenerateFace(false);
    }
    void GenerateSide(int startIndex) {
        //Debug.Log("side is being generated with start index of: " + startIndex);
        for (int i = 0; i < this.numberOfVertices; i++) {
            int v1 = startIndex + i;
            int v2 = startIndex + i + 1;
            int v3 = startIndex + i + this.numberOfVertices + 1;
            int v4 = startIndex + i + this.numberOfVertices + 2;


            this.meshTriangles.Add(v1);
            this.meshTriangles.Add(v4);
            this.meshTriangles.Add(v3);

            this.meshTriangles.Add(v4);
            this.meshTriangles.Add(v1);
            this.meshTriangles.Add(v2);
        }
        //for (int i = 0; i < this.numberOfVertices; i += 2) {
        //    int v1 = startIndex + i;
        //    int v2 = startIndex + i + this.numberOfVertices;
        //    int v3 = startIndex + i + 1;
        //    int v4 = startIndex + i + 1 + this.numberOfVertices;
        //    int v5 = i != this.numberOfVertices - 2 ? startIndex + i + 2 : startIndex;
        //    int v6 = i != this.numberOfVertices - 2 ? startIndex + i + 2 + this.numberOfVertices : startIndex + this.numberOfVertices;


        //    this.meshTriangles.Add(v1);
        //    this.meshTriangles.Add(v4);
        //    this.meshTriangles.Add(v2);

        //    this.meshTriangles.Add(v4);
        //    this.meshTriangles.Add(v1);
        //    this.meshTriangles.Add(v3);


        //    this.meshTriangles.Add(v3);
        //    this.meshTriangles.Add(v6);
        //    this.meshTriangles.Add(v4);

        //    this.meshTriangles.Add(v6);
        //    this.meshTriangles.Add(v3);
        //    this.meshTriangles.Add(v5);
        //}
    }

    void generateface() {
        meshVertiecesList.Add(playerLocations[playerLocations.Count - 1]);
        int start = meshVertiecesList.Count - numberOfVertices - 2;
        for (int i = 0; i < numberOfVertices+1; i++) {
            meshTriangles.Add(start + i + 1);
            meshTriangles.Add(meshVertiecesList.Count - 1);
            meshTriangles.Add(start + i);
        }
    }
}
