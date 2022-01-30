using Cinemachine;
using DevCore.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

public class GateCrossFeedbackPlayer : MonoBehaviour {
	[Header("Cross Gate")] [SerializeField]
	private FOVKickSetting crossGateFOVKick;
	
	[SerializeField]
	private FeedbackAsset offCrossFeedback = null;

	[SerializeField]
	private FeedbackAsset onCrossFeedback = null;

	[Header("Fail Gate")] [SerializeField] private FeedbackAsset _failGateFeedback = null; 
	
	[Header("References")]
	[SerializeField] private CinemachineVirtualCamera _camera = null;

	private Tween m_CurrentCameraTween = null;
	private float m_BaseCameraFOV = 0f;
	
	private void OnEnable() {
		ObstacleTimer.InObstacle += OnGateCrossed;
		m_BaseCameraFOV = CameraGetFOV();
	}

	private void OnGateCrossed(object sender, ObstacleEventArg e) {
		var col = e.obstacleGameObject.GetComponent<ObstacleAttribute>().color;
		if (!PlayerAttribute.invincible) {
			if (PlayerAttribute.color == col) {
				CrossGate(col);
			} else {
				FailGate(col);
			}
		}
	}

	private void CrossGate(Phase phase) {
		if (phase == Phase.BLUE) {
			Assert.IsNotNull(offCrossFeedback);
			offCrossFeedback.Play(transform);
		} else {
			Assert.IsNotNull(onCrossFeedback);
			onCrossFeedback.Play(transform);
		}
		
		DoFovKick(crossGateFOVKick);
	}


	#region Camera
	private void DoFovKick(FOVKickSetting setting) {
		if (m_CurrentCameraTween != null) {
			m_CurrentCameraTween.Kill(true);
		}

		m_CurrentCameraTween = DOTween.To(CameraGetFOV, CameraSetFov, setting.value, setting.duration);
		m_CurrentCameraTween.SetEase(setting.curve);
		m_CurrentCameraTween.OnComplete(ResetCameraFOV);
	}
	
	private float CameraGetFOV() {
		return _camera.m_Lens.FieldOfView;
	}
	
	private void CameraSetFov(float fov) {
		_camera.m_Lens.FieldOfView = fov;
	}

	private void ResetCameraFOV() {
		CameraSetFov(m_BaseCameraFOV);
	}
	#endregion


	private void FailGate(Phase phase) {
		_failGateFeedback.Play(transform);
	}

	private void OnDisable() {
		ObstacleTimer.InObstacle -= OnGateCrossed;
	}
}

[System.Serializable]
public struct FOVKickSetting {
	[Min(0f)]public float duration;
	[Min(0f)]public float value;
	public AnimationCurve curve;
} 