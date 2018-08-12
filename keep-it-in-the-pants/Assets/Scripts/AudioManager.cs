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
	private List<AudioClip> usedWomanSound = new List<AudioClip>();
	private List<AudioClip> usedManSound = new List<AudioClip>();

	// Use this for initialization
	void Start () {
		playingVoiceSounds = false;
		timeFromLastSound = 0.0f;
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

			bool playedSound = SelectAndPlayVoice();
			if (playedSound) {
				timeFromLastSound = 0.0f;
			}
		}

	}

	public void EndGame () {
		backgroundSound.volume = 0.1f;
		playingVoiceSounds = true;
		usedWomanSound.Clear();
		usedManSound.Clear();
	}

	private bool SelectAndPlayVoice () {
		int WomanMan = Random.Range(0, 2);

		if (WomanMan == 0) {
			AudioClip voice = WomanSounds[Random.Range(0,WomanSounds.Count)];
			if (usedWomanSound.Count < WomanSounds.Count) {
				while (usedWomanSound.Contains(voice)) {
					voice = WomanSounds[Random.Range(0, WomanSounds.Count)];
				}
			} else {
				return false;
			}

			sourceForVoice.clip = voice;
			usedWomanSound.Add(voice);
			sourceForVoice.Play();
			return true;
		} else {
			AudioClip voice = ManSounds[Random.Range(0, ManSounds.Count)];

			if (usedManSound.Count < ManSounds.Count) {
				while (usedManSound.Contains(voice)) {
					voice = ManSounds[Random.Range(0, ManSounds.Count)];
				}
			} else {
				return false;
			}

			sourceForVoice.clip = voice;
			usedManSound.Add(voice);
			sourceForVoice.Play();
			return true;
		}

	}

}
