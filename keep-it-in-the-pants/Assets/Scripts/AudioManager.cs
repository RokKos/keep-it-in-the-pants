using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	[SerializeField] private List<AudioClip> ManSounds;
	[SerializeField] private List<AudioClip> WomanSounds;

	[SerializeField] private AudioSource backgroundSound;
	[SerializeField] private AudioSource sourceForVoice;

	[SerializeField] private float voiceDelay;
	private float timeFromLastSound = 0.0f;

	bool playingVoiceSounds = false;
	

	// Use this for initialization
	void Start () {
		playingVoiceSounds = false;
		timeFromLastSound = voiceDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (!playingVoiceSounds) {
			return;
		}


		if (!sourceForVoice.isPlaying) {
			if (timeFromLastSound < voiceDelay) {
				timeFromLastSound += Time.deltaTime;
				return;
			}

			SelectAndPlayVoice();
			timeFromLastSound = 0.0f;
		}

	}

	public void EndGame () {
		backgroundSound.volume = 0.1f;
		playingVoiceSounds = true;
	}

	private void SelectAndPlayVoice () {
		int WomanMan = Random.Range(0, 2);

		if (WomanMan == 0) {
			AudioClip voice = WomanSounds[Random.Range(0,WomanSounds.Count)];
			sourceForVoice.clip = voice;
			sourceForVoice.Play();
		} else {
			AudioClip voice = ManSounds[Random.Range(0, ManSounds.Count)];
			sourceForVoice.clip = voice;
			sourceForVoice.Play();
		}

	}

}
