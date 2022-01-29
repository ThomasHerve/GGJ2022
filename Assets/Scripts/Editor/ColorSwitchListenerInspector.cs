using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorSwitchListener), true)]
public class ColorSwitchListenerInspector : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		EditorGUILayout.Space();

		if (GUILayout.Button("Toggle State")) {
			PlayerAttribute.color = !PlayerAttribute.color;
			if (!Application.isPlaying) {
				foreach (var listener in FindObjectsOfType<ColorSwitchListener>(true)) {
					listener.OnColorSwitch();
				}
			}
		}
	}
}