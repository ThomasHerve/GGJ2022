using UnityEngine;

namespace DevCore.ScriptableVariables {
	public abstract class ScriptableVariableBase : ScriptableObject, IValueWrapper {
		public abstract object wrappedValue { get; set; }
		
		public string wrapperName => name;

		public override string ToString() {
			return wrappedValue.ToString();
		}

		public abstract void ResetValue();

		protected class InvalidWrapperCastException : System.InvalidCastException {
			public override string Message => "Invalid value wrapper type cast";
		}
	}
}