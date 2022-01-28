using System.IO;
using DevCore.Core;
using DevCore.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace DevCore.ScriptableVariables.Editor {
	[CustomEditor(typeof(ScriptableVariableSaveWrapper))]
	public class ScriptableVariableSaveWrapperInspector : UnityEditor.Editor {
		#region Currents
		private ScriptableVariableSaveWrapper m_Target = null;
		#endregion


		#region Serialized Properties
		private SerializedProperty m_VariableProp = null;
		private SerializedProperty m_FileNameProp = null;
		private SerializedProperty m_ExtensionProp = null;
		private SerializedProperty m_SubDirectoryProp = null;
		#endregion


		#region Callbacks
		private void OnEnable() {
			m_Target = target as ScriptableVariableSaveWrapper;
			
			m_VariableProp = serializedObject.FindProperty("m_Variable");
			m_FileNameProp = serializedObject.FindProperty("m_FileName");
			m_ExtensionProp = serializedObject.FindProperty("m_Extension");
			m_SubDirectoryProp = serializedObject.FindProperty("m_SubDirectory");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();
			m_VariableProp.objectReferenceValue =
				EditorGUILayout.ObjectField("Variable", m_VariableProp.objectReferenceValue,
					typeof(ScriptableVariableBase)
					, false);

			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Path", EditorStyles.boldLabel);
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(m_FileNameProp);
			EditorGUILayout.LabelField(".", GUILayout.Width(4f));
			EditorGUILayout.PropertyField(m_ExtensionProp, GUIContent.none, GUILayout.Width(60f));
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.PropertyField(m_SubDirectoryProp);
			
			//Draw full path
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Target path: ", GUILayout.Width(EditorGUIUtility.labelWidth));
			EditorGUILayout.LabelField(m_Target.GetSavePath(), DevCoreEditorGUIUtility.Styles.italicLabel);
			
			if (GUILayout.Button("../", GUILayout.Width(40f))) {
				EditorUtility.RevealInFinder(Application.persistentDataPath);
			}
			EditorGUILayout.EndHorizontal();

			GUILayout.Space(12f);
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Save")) {
				m_Target.Save();
			}

			if (GUILayout.Button("Load")) {
				if (!m_Target.Load()) {
					Debug.LogError($"The file you want to load at {m_Target.GetSavePath()} doesn't exist");
				}
			}

			EditorGUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();
		}
		#endregion
	}
}