using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	[SerializeField] private Slider racistSlider;
	[SerializeField] private Toggle invertControlsToggle;
    [SerializeField] private InputField field;
    [SerializeField] private GameObject pausePanel;


    private void Start() {
        if(field)
            field.onEndEdit.AddListener(ChangeName);
    }

	private void Update () {
		if (Input.GetKey("escape")) {
            if(SceneManager.GetActiveScene().name == "MainUIScene") {
			    ExitGame();
            }
            else if(GameManager.Instance.playerController.dickMoving) {
                Time.timeScale = 0.0f;
                pausePanel.SetActive(true);
            }
		}
	}

	private void ChangeName(string name) {
        PlayerPrefs.SetString("player_name", name);
    }

    public void Restart () {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ExitGame () {
		Application.Quit();
	}


	public void GoToMainMenu () {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainUIScene");
	}

	public void GoToHighScore () {
        Time.timeScale = 1f;
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

    public void Resume() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
