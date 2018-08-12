using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSpawning : MonoBehaviour {

	[SerializeField] private GameObject colliderPrefab;
    [SerializeField] private Transform Transform;
    private Vector3 lastSpawnPosition;
    private float radius;
    [SerializeField] private float offset;

    public void Start() {
        lastSpawnPosition = Transform.position;
        radius = GameManager.Instance.dickRadius;
        EventManager.Instance.OnPlayerPositionChanged.AddListener(SpawnCollider);
		offset = radius * 1.5f;
    }

    void SpawnCollider(Transform t) {
        if (CheckForDistance(t.position)) {
            lastSpawnPosition = t.position;
            GameObject coll = Instantiate(colliderPrefab, t.position, Quaternion.identity, GameManager.Instance.transform);
            coll.GetComponent<ColliderController>().SetRadius(radius);
        }
    }

    private bool CheckForDistance(Vector3 pos) {
        Debug.Assert(radius > 0);
        return (pos - lastSpawnPosition).magnitude > 2 * radius + offset;
    }
}
