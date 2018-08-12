using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	[SerializeField] private Slider racistSlider;
	[SerializeField] private Toggle invertControlsToggle;
    [SerializeField] private InputField field;


    private void Start() {
        field.onEndEdit.AddListener(ChangeName);
    }

    private void ChangeName(string name) {
        PlayerPrefs.SetString("player_name", name);
    }

    public void Restart () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GoToMainMenu () {
		SceneManager.LoadScene("MainUIScene");
	}

	public void GoToHighScore () {
		SceneManager.LoadScene("Highscore");
	}

	public void OnPlayGame () {
		int newValue = (int)racistSlider.value;
		Debug.Log("Racist Difficulty changed from: "+ PlayerPrefs.GetInt("racistDifficulty", -1).ToString() + " to: " + newValue.ToString());
		PlayerPrefs.SetInt("racistDifficulty", (int)newValue);

		int invert = invertControlsToggle.isOn ? 1 : 0;
		Debug.Log("Invert Controlls changed from: " + PlayerPrefs.GetInt("invertControls", -1).ToString() + " to: " + invert.ToString());
		PlayerPrefs.SetInt("invertControls", invert); 

		SceneManager.LoadSceneAsync("SpawningScene");
	}
}
