using DevCore.Core;
using UnityEngine;
using UnityEngine.Assertions;

public class GateCrossFeedbackPlayer : MonoBehaviour {
	[SerializeField] private FeedbackAsset _offFeedback = null;
	[SerializeField] private FeedbackAsset _onFeedback = null;
	
	private void OnEnable() {
		ObstacleTimer.InObstacle += OnGateCrossed;
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
			Assert.IsNotNull(_offFeedback);
			_offFeedback.Play(transform);
		} else {
			Assert.IsNotNull(_onFeedback);
			_onFeedback.Play(transform);
		}
	}

	private void FailGate(Phase phase) {
		
	}
	
	private void OnDisable() {
		ObstacleTimer.InObstacle -= OnGateCrossed;
	}
}