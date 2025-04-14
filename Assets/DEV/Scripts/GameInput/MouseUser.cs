using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
namespace GameInput
{
	public enum MouseButton
	{
		Left, Right,
	}
	public static class MouseUser
	{
		private static InputActions _inputActions => InputActions.Instance;
		private static Camera _mainCamera;
		public static Vector2 _mousePosition { get; private set; }
		public static Vector2 MouseInWorldPosition => _mainCamera.ScreenToWorldPoint(_mousePosition);

		private static bool _isLeftMouseButtonPressed;
		private static bool _isRightMouseButtonPressed;

		public static void Init()
		{
			_mainCamera = Camera.main;
			Subscribe();
		}
		private static void Disponse()
		{
			Unsubscribe();
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.ExitingEditMode || state == PlayModeStateChange.ExitingPlayMode)
			{
				Disponse();
			}
		}
		private static void Subscribe()
		{
			Debug.Log("2");
			_inputActions.Game.MousePosition.performed += OnMousePositionPerformed;
			_inputActions.Game.PlatformAction.performed += OnPerformActionPerformed;
			_inputActions.Game.PlatformAction.canceled += OnPerformActionCanceled;
			_inputActions.Game.CancelAction.performed += OnCancelActionPerformed;
			_inputActions.Game.CancelAction.canceled += OnCancelActionCanceled;
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void Unsubscribe()
		{

			Debug.Log("3");
			_inputActions.Game.MousePosition.performed -= OnMousePositionPerformed;
			_inputActions.Game.PlatformAction.performed -= OnPerformActionPerformed;
			_inputActions.Game.PlatformAction.canceled -= OnPerformActionCanceled;
			_inputActions.Game.CancelAction.performed -= OnCancelActionPerformed;
			_inputActions.Game.CancelAction.canceled -= OnCancelActionCanceled;
			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		}



		private static void OnPerformActionPerformed(InputAction.CallbackContext context)
		{
			Debug.Log("sad");
			_isLeftMouseButtonPressed = true;
		}
		private static void OnPerformActionCanceled(InputAction.CallbackContext context)
		{
			Debug.Log("dasd");

			_isLeftMouseButtonPressed = false;
		}
		private static void OnCancelActionPerformed(InputAction.CallbackContext context)
		{
			Debug.Log("sadasdasdad");

			_isRightMouseButtonPressed = true;
		}

		private static void OnCancelActionCanceled(InputAction.CallbackContext context)
		{
			Debug.Log("saaaad");

			_isRightMouseButtonPressed = false;
		}
		private static void OnMousePositionPerformed(InputAction.CallbackContext context)
		{
			_mousePosition = context.ReadValue<Vector2>();
			Debug.Log(_mousePosition);
		}
		public static bool IsMouseButtonPressed(MouseButton mouseButton) => (mouseButton == MouseButton.Left ? _isLeftMouseButtonPressed : _isRightMouseButtonPressed);

	}
}
