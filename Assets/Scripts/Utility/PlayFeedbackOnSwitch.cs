using DevCore.Core;

public class PlayFeedbackOnSwitch : ColorSwitchListener {
	public FeedbackAsset _offFeedback = null;
	public FeedbackAsset _onFeedback = null;
	
	public override void OnColorSwitch() {
		if (PlayerAttribute.color == Phase.ORANGE) {
			_offFeedback.Play(transform);
		} else {
			_onFeedback.Play(transform);
		}
	}
}