using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

		public static bool CanMousePointerUI => IsMousePointerOverUI();
		public static Action OnLeftMouseDown = delegate { };
		public static Action OnLeftMouseUp = delegate { };
		public static Action OnRightMouseDown = delegate { };
		public static Action OnRightMouseUp = delegate { };

		private static bool _isLeftMouseButtonPressed;
		private static bool _isRightMouseButtonPressed;


		public static void Init()
		{
			_mainCamera = Camera.main;
			Subscribe();
		}
		public static void Disponse()
		{
			Unsubscribe();
		}
		public static bool IsMouseButtonPressed(MouseButton mouseButton) => (mouseButton == MouseButton.Left ? _isLeftMouseButtonPressed : _isRightMouseButtonPressed);
		private static bool IsMousePointerOverUI()
		{
			// Fare pozisyonunu al
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
			{
				position = _mousePosition
			};
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEventData, results);
			return results.Count > 0;
		}
		private static void Subscribe()
		{
			_inputActions.Game.MousePosition.performed += OnMousePositionPerformed;
			_inputActions.Game.PlatformAction.performed += OnPerformActionPerformed;
			_inputActions.Game.PlatformAction.canceled += OnPerformActionCanceled;
			_inputActions.Game.CancelAction.performed += OnCancelActionPerformed;
			_inputActions.Game.CancelAction.canceled += OnCancelActionCanceled;
		}
		private static void Unsubscribe()
		{

			_inputActions.Game.MousePosition.performed -= OnMousePositionPerformed;
			_inputActions.Game.PlatformAction.performed -= OnPerformActionPerformed;
			_inputActions.Game.PlatformAction.canceled -= OnPerformActionCanceled;
			_inputActions.Game.CancelAction.performed -= OnCancelActionPerformed;
			_inputActions.Game.CancelAction.canceled -= OnCancelActionCanceled;
		}
		private static void OnPerformActionPerformed(InputAction.CallbackContext context)
		{
			OnLeftMouseDown?.Invoke();
			_isLeftMouseButtonPressed = true;
		}
		private static void OnPerformActionCanceled(InputAction.CallbackContext context)
		{
			OnLeftMouseUp?.Invoke();
			_isLeftMouseButtonPressed = false;
		}
		private static void OnCancelActionPerformed(InputAction.CallbackContext context)
		{
			OnRightMouseDown?.Invoke();
			_isRightMouseButtonPressed = true;
		}

		private static void OnCancelActionCanceled(InputAction.CallbackContext context)
		{

			OnRightMouseUp?.Invoke();
			_isRightMouseButtonPressed = false;
		}
		private static void OnMousePositionPerformed(InputAction.CallbackContext context)
		{
			_mousePosition = context.ReadValue<Vector2>();
		}

	}
}
