using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorSwitchListener), true)]
public class ColorSwitchListenerInspector : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		EditorGUILayout.Space();

		if (GUILayout.Button("Toggle State")) {
			if (PlayerAttribute.color == Phase.BLUE) PlayerAttribute.color = Phase.ORANGE;
			else PlayerAttribute.color = Phase.BLUE;
			if (!Application.isPlaying) {
				foreach (var listener in FindObjectsOfType<ColorSwitchListener>(true)) {
					listener.OnColorSwitch();
				}
			}
		}
	}
}