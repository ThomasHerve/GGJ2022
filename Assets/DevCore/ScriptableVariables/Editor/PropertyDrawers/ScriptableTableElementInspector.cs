using UnityEditor;
using UnityEngine;

namespace DevCore.ScriptableVariables.Editor {
	[CustomPropertyDrawer(typeof(ScriptableTableElement<>))]
	public class ScriptableTableElementInspector : PropertyDrawer {
		private const float SPACING = 2f;
		
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			return base.GetPropertyHeight(property, label) + SPACING;
		}

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
			rect.height -= SPACING;
			
			//Draw name
			property.Next(true);
			SerializedProperty name = property.Copy(); 
			float labelWidth = EditorGUIUtility.labelWidth;
			Rect nameRect = new Rect(rect.position, new Vector2(labelWidth - 5f, rect.size.y));
			EditorGUI.PropertyField(nameRect, name, GUIContent.none);

			//Draw variable field
			property.Next(false);
			SerializedProperty variable = property.Copy();
			Rect fieldRect =
				new Rect(new Vector2(rect.position.x + labelWidth, rect.position.y),
					new Vector2(rect.width - labelWidth, rect.size.y));
			EditorGUI.PropertyField(fieldRect, variable, GUIContent.none);
		}
	}
}