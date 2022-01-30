using UnityEngine;

public class DisableOnPlatform : MonoBehaviour {
	[SerializeField] private RuntimePlatform m_Platform;
	private void Awake() {
		if (Application.platform == m_Platform) {
			gameObject.SetActive(false);
		}
	}
}