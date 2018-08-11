using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSpawning : MonoBehaviour {

	[SerializeField] private GameObject colliderPrefab;
    [SerializeField] private Transform Transform;
    private Vector3 lastSpawnPosition;
    private float radius;

    public void Start() {
        lastSpawnPosition = Transform.position;
        radius = GameManager.Instance.dickRadius;
    }
	
	void Update () {
        if (CheckForDistance()) {
            GameObject coll = Instantiate(colliderPrefab, Transform.position, Quaternion.identity);
            coll.GetComponent<ColliderController>().SetRadius(radius);
        }
	}

    private bool CheckForDistance() {
        return (Transform.position - lastSpawnPosition).magnitude > 0.1f;
    }
}
