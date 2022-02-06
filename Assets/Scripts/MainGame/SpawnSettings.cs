using System;
using UnityEngine;

[Serializable]
public struct SpawnSettings {
	#region Settings
	public Vector2 randomSpawnDistance;
	public Vector2Int randomPatternCount;
	public float patternSpacing;
	#endregion


	#region Construction
	public SpawnSettings(Vector2 randomSpawnDistance, Vector2Int randomPatternCount, float patternSpacing) {
		this.randomSpawnDistance = randomSpawnDistance;
		this.randomPatternCount = randomPatternCount;
		this.patternSpacing = patternSpacing;
	}
	#endregion
	

	#region Interpolation
	public static SpawnSettings Lerp(SpawnSettings a, SpawnSettings b, float t) {
		t = Mathf.Clamp01(t);
		Vector2 randomSpawnTime = Vector2.LerpUnclamped(a.randomSpawnDistance, b.randomSpawnDistance, t);
		Vector2Int randomPatternCount =
			Vector2Int.RoundToInt(Vector2.LerpUnclamped(a.randomPatternCount, b.randomPatternCount, t));
		float patternSpacing = Mathf.LerpUnclamped(a.patternSpacing, b.patternSpacing, t);
		return new SpawnSettings(randomSpawnTime, randomPatternCount, patternSpacing);
	}
	#endregion
}