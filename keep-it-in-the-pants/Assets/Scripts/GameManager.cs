using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	[SerializeField] private MeshSpawningScript meshSpawningScript;
	[SerializeField] private Camera topCamera;
	[SerializeField] private Camera dickCamera;

	[SerializeField] private  List<Material> dickMaterials;


	private enum DickType { kAsian, kWhite, kBlack, kLast };

	private struct DickInfo {
		public float thickness;
		public int material_ind;

		public DickInfo (float _thinkness, int _material_ind) {
			thickness = _thinkness;
			material_ind = _material_ind;
		}
	}
	

	Dictionary<DickType, DickInfo> dickSettings = new Dictionary<DickType, DickInfo>(){
		{ DickType.kAsian, new DickInfo(2.5f, 0) },
		{ DickType.kWhite, new DickInfo(5.0f, 1) },
		{ DickType.kBlack, new DickInfo(10.0f, 2) }
	};




	public float dickRadius;
	public float positionSendingInterval;
	public Material skinColor;



	private void Awake () {
		if (!Instance) {
			Instance = this;
		}

		DickType dickType = (DickType)PlayerPrefs.GetInt("racistDifficulty", 0);
		DickInfo info = dickSettings[dickType];
		skinColor = dickMaterials[info.material_ind];
	}

	public void ChangeCameras (bool isDickActive) {
		topCamera.enabled = !isDickActive;
		dickCamera.enabled = isDickActive;
	}
}
