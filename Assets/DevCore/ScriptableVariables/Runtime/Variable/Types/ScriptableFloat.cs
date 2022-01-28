using UnityEngine;

namespace DevCore.ScriptableVariables {
	[CreateAssetMenu(fileName = "FLT_", menuName = SVConsts.SMV_PATH + "Float", order = SVConsts.ASSET_ORDER)]

	public class ScriptableFloat : ScriptableVariable<float> { }
}