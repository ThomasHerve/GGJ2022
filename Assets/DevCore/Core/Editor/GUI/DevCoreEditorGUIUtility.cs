using UnityEditor;
using UnityEngine;

namespace DevCore.Core.Editor {
	public static class DevCoreEditorGUIUtility {
		#region Styles
		public static class Styles {
			public static readonly GUIStyle italicLabel = new GUIStyle(EditorStyles.label){fontStyle = FontStyle.Italic};
		}
		#endregion
		
		#region Project View
		public static void SelectObject(Object target,bool ping = true) {
			Selection.objects = new[] {target};
			if (ping) {
				EditorGUIUtility.PingObject(target);
			}
		}
		#endregion
	}
}