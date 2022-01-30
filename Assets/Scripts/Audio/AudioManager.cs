using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	#region Settings
	public AudioMixer globalMixer = null;
	public float menuLowPassFilterAmount = 0.5f;
	#endregion


	#region Currents
	private float m_CurrentLowPassFilterRatio = 1f;  
	#endregion

	#region Callbacks
	private void OnEnable() {
		GameManager.onGameStart += OnGameStart;
		GameManager.onGameEnd += OnGameEnd;
	}
	private void Start() {
		SetLowPassFilterAmount(menuLowPassFilterAmount);
	}

	private void OnGameStart() {
		DoLowPassFilterFade(1f, 0.6f);
	}

	private void OnGameEnd() {
		DoLowPassFilterFade(menuLowPassFilterAmount, 0.3f);
	}

	private void OnDisable() {
		GameManager.onGameStart += OnGameStart;
		GameManager.onGameEnd += OnGameEnd;
	}
	#endregion

	public void SetVolume(float volume) {
		globalMixer.SetFloat("Volume", Mathf.Lerp(-80f, 0f, volume));
	}

	#region Music
	private void DoLowPassFilterFade(float targetVal, float duration) {
		DOTween.To(GetLowPassFilterAmount, SetLowPassFilterAmount, targetVal, duration);
	}
	
	private void SetLowPassFilterAmount(float ratio) {
		m_CurrentLowPassFilterRatio = ratio;
		globalMixer.SetFloat("LowPass", Mathf.Lerp(10f, 22000f, ratio));
	}

	private float GetLowPassFilterAmount() {
		return m_CurrentLowPassFilterRatio;
	}
	#endregion
}