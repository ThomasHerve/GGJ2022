using UnityEngine;

namespace DevCore.ScriptableVariables {
	
	[CreateAssetMenu(fileName = "INT_", menuName = SVConsts.SMV_PATH + "Int", order = SVConsts.ASSET_ORDER)]
	public class ScriptableInt : ScriptableVariable<int> { }
}