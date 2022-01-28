using DevCore.Core;
using UnityEngine;

namespace DevCore.ScriptableVariables {
	[CreateAssetMenu(fileName = "SAVE_", menuName = SVConsts.SV_UTILS_PATH + "Save Wrapper" ,order = SVConsts.ASSET_ORDER_UTILITIES)]
	public class ScriptableVariableSaveWrapper : ScriptableObject {
		#region Settings
		[SerializeField] private ScriptableVariableBase m_Variable = null;
		
		[SerializeField] private string m_FileName = "Save";
		[SerializeField] private string m_Extension = "sav";
		[SerializeField] private string m_SubDirectory = string.Empty;
		#endregion


		#region Data Management
		public bool Load() {
			return false;
		}

		public void Save() {
			var fso = new FileStreamOperation(GetDirectoryPath(), m_FileName, m_Extension);
			SaveUtility.WriteJson(fso, m_Variable.ToJson());
		}
		#endregion


		#region Utility
		public string GetSavePath() {
			string dir = GetDirectoryPath();
			return SaveUtility.ConcatFilePath(GetDirectoryPath(),m_FileName, m_Extension);
		}

		public string GetDirectoryPath() {
			string subDir = string.Empty;
			if (m_SubDirectory.Length > 0) {
				subDir = SaveUtility.FileSeparator + m_SubDirectory;
			}

			return SaveUtility.DefaultSavePath + subDir;
		}
		#endregion
	}
}