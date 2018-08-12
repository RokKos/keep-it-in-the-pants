using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	[SerializeField] private MeshSpawningScript meshSpawningScript;
	[SerializeField] private Camera topCamera;
	[SerializeField] private Camera dickCamera;
    string nameKey = "name_";
    string scoreKey = "score_";

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

    private struct HighscoreInfo {
        public string name;
        public float score;

        public HighscoreInfo(string name_, float score_) {
            name = name_;
            score = score_;
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

    public void UpdateHighscore(float score) {
        List<HighscoreInfo> players = new List<HighscoreInfo>(4);
        players.Add(new HighscoreInfo(PlayerPrefs.GetString("player_name", "Lil Dicky"), score));
        
        for (int i = 0; i < 3; i++) {
            string currentNameKey = nameKey + i.ToString();
            string currentScoreKey = scoreKey + i.ToString();
            players.Add(new HighscoreInfo(PlayerPrefs.GetString("currentNameKey", "Lil Dicky"), PlayerPrefs.GetFloat(currentScoreKey, 0f)));
        }

        SortHighscore(players);

        for (int i = 0; i < 3; i++) {
            string currentNameKey = nameKey + i.ToString();
            string currentScoreKey = scoreKey + i.ToString();
            PlayerPrefs.SetString(currentNameKey, players[i].name);
            PlayerPrefs.SetFloat(currentScoreKey, players[i].score);
        }
    }

    void SortHighscore(List<HighscoreInfo> players) {
        for (int i = 0; i < players.Count - 1; i++) 
            for (int j = 0; j < players.Count - i - 1; j++) {
                if (players[j].score < players[j + 1].score) {
                    HighscoreInfo info = players[j];
                    players[j] = players[j + 1];
                    players[j + 1] = info;
                }
            }
    }
}
