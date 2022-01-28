using System;
using Newtonsoft.Json;

namespace DevCore.ScriptableVariables {
	public static class ScriptableVariableSerialization {
		#region Serialization
		/// <summary>
		/// Convert the input scriptable variable to <see cref="KeyedValue"/> and write it into json string
		/// </summary>
		/// <param name="variable"></param>
		/// <param name="prettyPrint"></param>
		/// <returns></returns>
		public static string ToJson(this ScriptableVariableBase variable, bool prettyPrint = true) {
			object data;
			if (variable is IValueTableConvertible convertible) {
				data = convertible.GetValueTable();
			} else {
				data = variable.wrappedValue;
			}

			return JsonConvert.SerializeObject(data,
				prettyPrint ? Formatting.Indented : Formatting.None);
		}

		public static string FromJson(this ScriptableVariableBase variable) {
			throw new NotImplementedException();
		}
		#endregion
	}
}