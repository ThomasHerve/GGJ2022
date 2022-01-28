using UnityEngine;
using System.IO;
using DevCore.Core;

namespace DevCore.Core {
	public static class SaveUtility {
		#region Properties
		public static char FileSeparator => Path.DirectorySeparatorChar;

		public static string DefaultSavePath {
			get { return Application.persistentDataPath; }
		}
		#endregion


		#region Json
		public static void WriteJson(FileStreamOperation op, string content) {
			if (!Directory.Exists(op.path)) {
				Directory.CreateDirectory(op.path);
			}

			string filePath = op.filePath;
			if (!File.Exists(filePath)) {
				File.Create(filePath).Dispose();
			}
			File.WriteAllText(filePath, content);
		}

		public static string ReadJson(FileStreamOperation op) {
			string path = op.filePath;
			if (!File.Exists(path)) {
				throw new FileNotFoundException($"The file you want to read [{path}] doesn't exist");
			}

			return File.ReadAllText(path);
		}
		#endregion


		#region File Utility
		public static string ConcatFilePath(string name, string extension) {
			return ConcatFilePath(DefaultSavePath, name, extension);
		}

		public static string ConcatFilePath(string path, string name, string extension) {
			return path + FileSeparator + name + '.' + extension;
		}
		#endregion
	}
}

public class FileStreamOperation {
	#region Datas
	public string fileName = "GameData";
	public string path = SaveUtility.DefaultSavePath;
	public string extension = "sav";
	#endregion


	#region Properties
	public string filePath => SaveUtility.ConcatFilePath(path, fileName, extension);
	#endregion


	#region Construction
	public FileStreamOperation(string fileName, string extension) {
		path = SaveUtility.DefaultSavePath;
		this.fileName = fileName;
		this.extension = extension;
	}

	public FileStreamOperation(string path, string fileName, string extension) {
		this.path = path;
		this.fileName = fileName;
		this.extension = extension;
	}
	#endregion
}