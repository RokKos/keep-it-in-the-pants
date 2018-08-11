using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	[SerializeField] private Camera topCamera;
	[SerializeField] private Camera dickCamera;

	public float dickRadius;
	public float positionSendingInterval;
	public Material skinColor;

    public bool lockLandscape = false;


    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
        if (lockLandscape) {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    public void ChangeCameras (bool isDickActive) {
		topCamera.enabled = !isDickActive;
		dickCamera.enabled = isDickActive;
	}



}
