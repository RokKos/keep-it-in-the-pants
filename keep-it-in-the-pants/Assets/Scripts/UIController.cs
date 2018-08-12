using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	[SerializeField] private Slider racistSlider;


	public void OnPlayGame () {
		int newValue = (int)racistSlider.value;

		Debug.Log("Racist Difficulty changed from: "+ PlayerPrefs.GetInt("racistDifficulty", -1).ToString() + " to: " + newValue.ToString());
		PlayerPrefs.SetInt("racistDifficulty", (int)newValue);

		SceneManager.LoadSceneAsync("SpawningScene");
	}
}
