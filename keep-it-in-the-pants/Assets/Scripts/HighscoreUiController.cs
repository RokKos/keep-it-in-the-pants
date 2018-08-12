using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUiController : MonoBehaviour {

    [SerializeField] private Text[] playerNames;
    [SerializeField] private Text[] scores;
    string nameKey = "name_";
    string scoreKey = "score_";

    void Start () {
		for(int i = 0; i < playerNames.Length; i++) {
            string currentNameKey = nameKey + i.ToString();
            string currentScoreKey = scoreKey + i.ToString();
            playerNames[i].text = PlayerPrefs.GetString(currentNameKey, "???");
            scores[i].text = PlayerPrefs.GetFloat(currentScoreKey, 0f).ToString("F2");
        }
	}
}
