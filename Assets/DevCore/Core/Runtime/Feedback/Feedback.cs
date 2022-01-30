namespace DevCore.Core {
	using UnityEngine;

	public class Feedback : MonoBehaviour {
		#region References
		[SerializeField] internal AudioSource audioSource = null;
		#endregion


		#region Currents
		internal new ParticleSystem particleSystem = null;
		private FeedbackAsset owner = null;
		private Timer timer = null;
		#endregion


		#region Behaviour
		internal void Setup(FeedbackAsset feedback) {
			owner = feedback;
			float duration = 0f;
			if (feedback.ParticleSystem) {
				particleSystem = Instantiate(feedback.ParticleSystem, transform);
				particleSystem.transform.localPosition = Vector3.zero;
				duration = owner.ParticleSystem.main.duration;
			}

			var sfxArr = feedback.SoundEffects;
			if (sfxArr.Length > 0) {
				var sfx = sfxArr[Random.Range(0, sfxArr.Length)];
				audioSource.clip = sfx;
				audioSource.spatialBlend = feedback.SpatialBlend;

				duration = Mathf.Max(duration, sfx.length);
			}

			timer = new Timer(duration + 0.1f, OnComplete);
		}

		internal void Play() {
			if (particleSystem != null) {
				particleSystem.Play();
			}

			audioSource.pitch = Random.Range(owner.PitchRange.x, owner.PitchRange.y);
			audioSource.volume = Random.Range(owner.VolumeRange.x, owner.VolumeRange.y);
			audioSource.Play();
			timer.Play();
		}

		internal void OnComplete() {
			owner.pool.Add(this);
		}
		#endregion
	}
}