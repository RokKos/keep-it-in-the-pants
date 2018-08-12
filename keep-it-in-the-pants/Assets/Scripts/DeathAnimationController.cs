using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationController : MonoBehaviour {

    [SerializeField] private Camera camera;
    [SerializeField] private Renderer humanRenderer;
    [SerializeField] private GameObject snakeParent;
    [SerializeField] private float frostumHeightMultiplier;
    [SerializeField] private GameObject wallsParent;

	void Start () {
        EventManager.Instance.OnPlayerDeath.AddListener(HandlePlayerDeath);
	}
	
	void Update () {
		
	}

    void HandlePlayerDeath() {
        var bounds = GetBoundsOfRenderers();
        var distance = GetDistanceBasedOnFrostumHeigth(bounds.size.y);
        wallsParent.SetActive(false);

        camera.transform.position = bounds.center + distance * Vector3.right;
        camera.transform.LookAt(bounds.center);
        GameManager.Instance.ChangeCameras(false);
    }

    Bounds GetBoundsOfRenderers() {
        var bounds = humanRenderer.bounds;
        var renderers = snakeParent.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers) {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
    
    float GetDistanceBasedOnFrostumHeigth(float height) {
        return height * frostumHeightMultiplier * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }
}
