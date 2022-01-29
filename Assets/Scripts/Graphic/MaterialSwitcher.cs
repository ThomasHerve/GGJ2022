using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : ColorSwitchListener {
	#region Settings
	[SerializeField] private MaterialSwitcherData[] _switchers = { };
	[SerializeField] private Renderer _targetRenderer = null;
	#endregion


	#region Current
	private Material[] _currentMaterials = null;
	#endregion
	
	#region Classes
	[System.Serializable]
	public class MaterialSwitcherData {
		public Material _offMaterial = null;
		public Material _onMaterial = null;
		public int[] _targetSubmeshes = { };

		public void ApplyMaterial(bool coloured, Material[] materials) {
			for (int i = 0; i < _targetSubmeshes.Length; i++) {
				materials[_targetSubmeshes[i]] = coloured ? _onMaterial : _offMaterial;
			}//
		}
	}
	#endregion
	
	
	#region Callbacks
	protected override void OnEnable() {
		_currentMaterials = _targetRenderer.sharedMaterials;
		base.OnEnable();
	}

	public override void OnColorSwitch() {
#if UNITY_EDITOR
		if (!Application.isPlaying) {
			_currentMaterials = _targetRenderer.sharedMaterials;
		}
#endif
		
		for (int i = 0; i < _switchers.Length; i++) {
			_switchers[i].ApplyMaterial(PlayerAttribute.color == Phase.BLUE, _currentMaterials);
		}

		_targetRenderer.sharedMaterials = _currentMaterials;
	}
	#endregion
}