using UnityEditor;
using UnityEngine;

namespace GameInput
{
	/// <summary>
	/// Manages input actions singleton
	/// </summary>
	public partial class InputActions
	{
		private static InputActions _instance;
		public static InputActions Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new InputActions();
					_instance.Enable();
				}
				return _instance;
			}
			private set => _instance = value;
		}


#if UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.ExitingEditMode)
			{
				Clear();
			}
		}

		private static void Clear()
		{
			_instance = null;
			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;

		}
#endif

	}
}
