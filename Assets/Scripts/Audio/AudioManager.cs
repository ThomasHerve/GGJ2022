using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	public AudioMixer globalMixer = null;
	public float menuLowPassFilterAmount = 0.5f;
	
	private void OnEnable() {
		GameManager.onGameStart += OnGameStart;
		GameManager.onGameEnd += OnGameEnd;
	}

	private void Start() {
		SetLowPassFilterAmount(menuLowPassFilterAmount);
	}

	private void OnGameStart() {
		SetLowPassFilterAmount(1f);
	}

	private void OnGameEnd() {
		SetLowPassFilterAmount(menuLowPassFilterAmount);
	}

	private void SetLowPassFilterAmount(float ratio) {
		globalMixer.SetFloat("MusicLowPass", Mathf.Lerp(10f, 22000f, ratio));
	}
	
	public void SetVolume(float volume) {
		
	}
	
	private void OnDisable() {
		GameManager.onGameStart += OnGameStart;
		GameManager.onGameEnd += OnGameEnd;
	}
}