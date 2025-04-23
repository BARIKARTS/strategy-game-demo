using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
namespace GameInput
{
	/// <summary>
	/// Defines mouse button types
	/// </summary>
	public enum MouseButton
	{
		Left, Right,
	}
	/// <summary>
	/// Manages mouse input and events
	/// </summary>
	public static class MouseUser
	{
		private static InputActions _inputActions => InputActions.Instance;
		private static Camera _mainCamera;
		public static Vector2 _mousePosition { get; private set; }

		/// <summary>
		/// Mouse position in world space
		/// </summary>
		public static Vector2 MouseInWorldPosition => _mainCamera.ScreenToWorldPoint(_mousePosition);

		/// <summary>
		/// Checks if mouse is over UI
		/// </summary>
		public static bool CanMousePointerUI => IsMousePointerOverUI();

		/// <summary>
		/// Event for left mouse button press
		/// </summary>
		public static Action OnLeftMouseDown = delegate { };
		/// <summary>
		/// Event for left mouse button release
		/// </summary>
		public static Action OnLeftMouseUp = delegate { };
		/// <summary>
		/// Event for right mouse button press
		/// </summary>
		public static Action OnRightMouseDown = delegate { };

		/// <summary>
		/// Event for right mouse button release
		/// </summary>
		public static Action OnRightMouseUp = delegate { };

		private static bool _isLeftMouseButtonPressed;//Tracks left mouse button state
		private static bool _isRightMouseButtonPressed;//racks right mouse button state


		public static void Init()
		{
			_mainCamera = Camera.main;
			Subscribe();
		}

		/// <summary>
		/// Disposes mouse input system
		/// </summary>
		public static void Disponse()
		{
			Unsubscribe();
		}

		/// <summary>
		/// Checks if mouse button is pressed
		/// </summary>
		/// <param name="mouseButton">MouseButton Type</param>
		/// <returns>status</returns>
		public static bool IsMouseButtonPressed(MouseButton mouseButton) => (mouseButton == MouseButton.Left ? _isLeftMouseButtonPressed : _isRightMouseButtonPressed);

		/// <summary>
		/// Checks if mouse is over UI elements
		/// </summary>
		/// <returns>status</returns>
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

		//Subscribes to input events
		private static void Subscribe()
		{
			_inputActions.Game.MousePosition.performed += OnMousePositionPerformed;
			_inputActions.Game.PlatformAction.performed += OnPerformActionPerformed;
			_inputActions.Game.PlatformAction.canceled += OnPerformActionCanceled;
			_inputActions.Game.CancelAction.performed += OnCancelActionPerformed;
			_inputActions.Game.CancelAction.canceled += OnCancelActionCanceled;
		}

		//Unsubscribes from input events
		private static void Unsubscribe()
		{

			_inputActions.Game.MousePosition.performed -= OnMousePositionPerformed;
			_inputActions.Game.PlatformAction.performed -= OnPerformActionPerformed;
			_inputActions.Game.PlatformAction.canceled -= OnPerformActionCanceled;
			_inputActions.Game.CancelAction.performed -= OnCancelActionPerformed;
			_inputActions.Game.CancelAction.canceled -= OnCancelActionCanceled;
		}

		//Handles left mouse button press
		private static void OnPerformActionPerformed(InputAction.CallbackContext context)
		{
			OnLeftMouseDown?.Invoke();
			_isLeftMouseButtonPressed = true;
		}
		//Handles left mouse button release
		private static void OnPerformActionCanceled(InputAction.CallbackContext context)
		{
			OnLeftMouseUp?.Invoke();
			_isLeftMouseButtonPressed = false;
		}

		//Handles right mouse button press
		private static void OnCancelActionPerformed(InputAction.CallbackContext context)
		{
			OnRightMouseDown?.Invoke();
			_isRightMouseButtonPressed = true;
		}

		//Handles right mouse button release
		private static void OnCancelActionCanceled(InputAction.CallbackContext context)
		{

			OnRightMouseUp?.Invoke();
			_isRightMouseButtonPressed = false;
		}
		//Updates mouse position
		private static void OnMousePositionPerformed(InputAction.CallbackContext context)
		{
			_mousePosition = context.ReadValue<Vector2>();
		}

	}
}
