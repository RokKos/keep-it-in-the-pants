using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationController : MonoBehaviour {

    private enum State { idle, zoomingOut, rotating};

    [SerializeField] private Camera camera;
    [SerializeField] private Transform dickCameraTransform;
    [SerializeField] private Renderer humanRenderer;
    [SerializeField] private GameObject snakeParent;
    [SerializeField] private float frostumHeightMultiplier;
    [SerializeField] private GameObject wallsParent;

    [Header("Zoom out stuff")]
    [SerializeField] private float zoomOutDuration;
    [SerializeField] private float rotateSpeed = 20.0f;
    private float progress;
    [SerializeField] private AnimationCurve curve;
    private Vector3 targetPosition;
    private Bounds bounds;
    private State state;

	void Start () {
        state = State.idle;
        EventManager.Instance.OnPlayerDeath.AddListener(HandlePlayerDeath);
	}

    void LateUpdate() {
        if (state == State.zoomingOut) {
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, curve.Evaluate(progress));
            camera.transform.LookAt(bounds.center);
            if (progress < 1) {
                progress += Time.deltaTime / zoomOutDuration;
            } else {
                state = State.rotating;
            }
        }
        else if(state == State.rotating) {
            camera.transform.RotateAround(bounds.center, Vector3.up, rotateSpeed * Time.deltaTime);
            camera.transform.LookAt(bounds.center);
        }
	}

    void HandlePlayerDeath() {
        bounds = GetBoundsOfRenderers();
        var distance = GetDistanceBasedOnFrostumHeigth(bounds.size.y);
        wallsParent.SetActive(false);

        camera.transform.position = dickCameraTransform.position;
        targetPosition = bounds.center + distance * Vector3.right;
        progress = 0.0f;
        state = State.zoomingOut;
        GameManager.Instance.ChangeCameras(false);
    }

    Bounds GetBoundsOfRenderers() {
        var boundsEn = humanRenderer.bounds;
        var renderers = snakeParent.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers) {
            boundsEn.Encapsulate(renderer.bounds);
        }

        return boundsEn;
    }
    
    float GetDistanceBasedOnFrostumHeigth(float height) {
        return height * frostumHeightMultiplier * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }
}
