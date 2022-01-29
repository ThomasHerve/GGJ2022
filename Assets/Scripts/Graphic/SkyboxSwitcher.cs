using UnityEngine;

public class SkyboxSwitcher : ColorSwitchListener {
	[SerializeField] private Material _offMaterial = null;
	[SerializeField] private Material _onMaterial = null;
	
	public override void OnColorSwitch() {
		bool on = PlayerAttribute.color == Phase.ORANGE;
		RenderSettings.skybox = on ? _onMaterial : _offMaterial;
	}
}