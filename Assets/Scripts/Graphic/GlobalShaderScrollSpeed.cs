using DevCore.Core;
using UnityEngine;

public class GlobalShaderScrollSpeed : MonoBehaviour {
	[SerializeField] private float m_SpeedFactor = 1f;

	private static GlobalShaderScrollSpeed m_Instance = null;
	private static readonly int _globalSpeedFactorProp = Shader.PropertyToID("_GlobalSpeedFactor");


	public static float speedFactor {
		get => m_Instance.m_SpeedFactor;
		set {
			m_Instance.m_SpeedFactor = value;
			SetFactorProperty(value);
		}
	}
	
	private void Awake() {
		m_Instance = this;
		SetFactorProperty(m_SpeedFactor);
	}

	private static void SetFactorProperty(float factor) {
		Shader.SetGlobalFloat(_globalSpeedFactorProp, factor);
	}

#if UNITY_EDITOR
	private void OnValidate() {
		SetFactorProperty(m_SpeedFactor);
	}
#endif
}