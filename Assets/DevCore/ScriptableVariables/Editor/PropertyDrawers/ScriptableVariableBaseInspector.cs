using System.Collections;
using DevCore.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace DevCore.ScriptableVariables.Editor {
	
	[CustomPropertyDrawer(typeof(ScriptableVariableBase), true)]
	public class ScriptableVariableBaseInspector : PropertyDrawer {
		#region Currents
		private GUIContent m_LabelContent = new GUIContent();
		#endregion


		#region Contents
		private static readonly GUIContent m_SelectAssetContent = new GUIContent("Select");
		private static readonly GUIContent m_UnbindContent = new GUIContent("Unbind");
		#endregion


		#region Callbacks
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			float defaultLabelWidth = EditorGUIUtility.labelWidth;
			bool hasValue = property.objectReferenceValue != null; 
			
			if (property.objectReferenceValue != null) {
				var obj = property.objectReferenceValue;
				SerializedObject so = new SerializedObject(obj);
				var valueProp = so.FindProperty("m_Value");

				if (valueProp != null) {
					DrawValueField(position, property,valueProp, label);
				}
				else {
					UpdateLabel(hasValue, property.name, string.Empty);
					EditorGUI.PropertyField(position, property, m_LabelContent);
				}
			}
			else {
				DrawEmptyField(position, property, label);
			}

			EditorGUIUtility.labelWidth = defaultLabelWidth;
		}
		#endregion


		#region Draw Modes
		private void DrawValueField(Rect position, SerializedProperty property, SerializedProperty value, GUIContent label) {
			

			//Draw the sub value field
			string ownerName = property.serializedObject.targetObject is IEnumerable
				? string.Empty
				: property.displayName;
			UpdateLabel(true, ownerName, property.objectReferenceValue.name);

			//Draw field
			value.serializedObject.Update();
			var scope = new GUIHorizontalScope(position);
			float miscButtonWidth = 20f;
			float fieldSpace = scope.GetRemainingSpace(miscButtonWidth, 1, false);
			EditorGUI.PropertyField(scope.GetInsertedRect(fieldSpace, true), value, m_LabelContent);
			if (GUI.Button(scope.GetInsertedRect(miscButtonWidth), "...")) {
				DrawOptionsMenu(property);
			}

			value.serializedObject.ApplyModifiedProperties();
		}

		private void DrawEmptyField(Rect position, SerializedProperty property, GUIContent label) {
			UpdateLabel(false, property.displayName, string.Empty);
			EditorGUI.PropertyField(position, property, m_LabelContent);
		}
		#endregion


		#region Label
		private void UpdateLabel(bool hasValue, string ownerName, string valueName) {
			if (!ScriptableTableEditor.isDrawingTab) {
				if (hasValue) {
					if (ownerName.Length > 0) {
						m_LabelContent.text = ownerName + $" ({valueName})";
					}
					else {
						m_LabelContent.text = valueName;
					}
				}
				else {
					m_LabelContent.text = ownerName;
				}
			}
			else {
				if (hasValue) {
					m_LabelContent.text = "...";
					m_LabelContent.tooltip = valueName;
					EditorGUIUtility.labelWidth = 15f;
				}
				else {
					m_LabelContent.text = "";
					m_LabelContent.tooltip = "";
				}
			}
		}
		#endregion


		#region Manage Variable
		private void PushReferenceIntoDragBuffer() {
			
		}
		
		private void DrawOptionsMenu(SerializedProperty property) {
			var menu = new GenericMenu();
			menu.AddItem(m_SelectAssetContent, false,
				() => { DevCoreEditorGUIUtility.SelectObject(property.objectReferenceValue); });

			menu.AddItem(m_UnbindContent, false, () => {
				property.objectReferenceValue = null;
				property.serializedObject.ApplyModifiedProperties();
			});
			menu.ShowAsContext();
		}
		#endregion
	}
}