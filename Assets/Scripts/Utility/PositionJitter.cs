using UnityEngine;

public class PositionJitter : MonoBehaviour {
	[SerializeField] private Vector3 m_Axes = Vector3.one;
	[SerializeField] private float m_Amplitude = 0.1f;
	[SerializeField] private float m_Frequency = 2f;

	void Update() {
		Vector3 noisedPos = new Vector3(
			RemappedNoise(0f),
			RemappedNoise(5f),
			RemappedNoise(10f)
			);

		noisedPos.Scale(m_Axes);
		noisedPos *= m_Amplitude;
		transform.localPosition = noisedPos;
	}

	private float RemappedNoise(float seed) {
		return Mathf.PerlinNoise(Time.time * m_Frequency, seed) * 2f - 1f;
	}
}