using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevCore.ScriptableVariables {
	[CreateAssetMenu(fileName = "VEC_", menuName = SVConsts.SMV_PATH + "Vector 3", order = SVConsts.ASSET_ORDER)]
	public class ScriptableVector3 : ScriptableVariable<Vector3> { }
}