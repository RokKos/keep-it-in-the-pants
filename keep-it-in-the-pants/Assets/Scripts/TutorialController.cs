using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    [SerializeField] private Text tutorialText;
    private string firstText = "Touch and drag to steer your cock";
    private string secondText = "Erect the largest dick without hitting anything";
    [SerializeField] private GameObject joystickGO;
    bool inTutorial = false;
    [SerializeField] RectTransform leftPosition;
    [SerializeField] RectTransform rightPosition;
    [SerializeField] float timeToLeft;
    [SerializeField] float timeToRight;
    private float lengthBetweenPositions;
    private GameObject GameObject;
    private float midScreen;

    public void Init() {
        midScreen = Screen.width / 2.0f;
        joystickGO.SetActive(true);
        GameObject = this.gameObject;
        tutorialText.text = firstText;
        tutorialText.gameObject.SetActive(true);
        lengthBetweenPositions = Mathf.Abs(leftPosition.transform.position.x - rightPosition.transform.position.x);
        EventManager.Instance.OnChangeControlAvailaility.Invoke(false);
        iTween.MoveTo(joystickGO, iTween.Hash("position", leftPosition.transform.position, "time", timeToLeft, "oncomplete", "StartAnimationRight", "oncompletetarget", GameObject));// leftPosition.position, timeToLeft);
        inTutorial = true;
    }

	void Update () {
        if (!inTutorial) return;

        var multiplier = joystickGO.GetComponent<RectTransform>().position.x > midScreen ? 2f : 1.1f;
        Debug.Log(multiplier);
        var x = (joystickGO.GetComponent<RectTransform>().position.x - midScreen) / (lengthBetweenPositions * multiplier);
        Debug.Log(joystickGO.transform.position.x);
        EventManager.Instance.OnDirectionInputChanged.Invoke(x, 0f);
	}

    private void StartAnimationRight() {
        tutorialText.text = secondText;
        iTween.MoveTo(joystickGO, iTween.Hash("position", rightPosition.transform.position, "time", timeToRight, "oncomplete", "OnCompleteTutorial", "oncompletetarget", GameObject));
    }

    private void OnCompleteTutorial() {
        EventManager.Instance.OnChangeControlAvailaility.Invoke(true);
        PlayerPrefs.SetInt("tutorial", 1);
        inTutorial = false;
        tutorialText.gameObject.SetActive(false);
        joystickGO.SetActive(false);
    }
}
