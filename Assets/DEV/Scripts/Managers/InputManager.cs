using GameInput;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages user input and interactions
/// </summary>
public class InputManager : MonoBehaviour
{
	private RuleManager _ruleManager;
	private InteractionContext currentContext;
	private CommonData _commonData => CommonData.Instance;
	private void Start()
	{
		_ruleManager = new RuleManager();
		currentContext = new InteractionContext()
		{
			Team = _commonData.Team
		};
	}
	private void OnEnable()
	{
		MouseUser.OnLeftMouseDown += SelectObject;
		MouseUser.OnRightMouseDown += InteracteableObject;
	}
	private void OnDisable()
	{
		MouseUser.OnLeftMouseDown -= SelectObject;
		MouseUser.OnRightMouseDown -= InteracteableObject;
	}

	/// <summary>
	/// Handles object selection with left mouse
	/// </summary>
	private void SelectObject()
	{
		if (MouseUser.CanMousePointerUI) return;
		GameObject[] clickedObjects = GetClickedGameObject();
		currentContext.ClickedPosition = MouseUser.MouseInWorldPosition;
		for (byte i1 = 0; i1 < clickedObjects.Length; ++i1)
		{
			if (clickedObjects[i1].TryGetComponent(out ISelectable selectable))
			{
				currentContext.FirstSelected?.OnDeselected();
				currentContext.FirstSelected = selectable;
				selectable.OnSelected();
				return;
			}
		}
		if (currentContext.FirstSelected != null)
			currentContext.FirstSelected?.OnDeselected();
		currentContext.FirstSelected = null;
	}

	/// <summary>
	/// Handles interaction with right mouse
	/// </summary>
	private void InteracteableObject()
	{
		if (MouseUser.CanMousePointerUI) return;
		GameObject[] clickedObjects = GetClickedGameObject();
		currentContext.ClickedPosition = MouseUser.MouseInWorldPosition;
		for (byte i1 = 0; i1 < clickedObjects.Length; ++i1)
		{
			if (clickedObjects[i1].TryGetComponent(out IInteractable interactableObject))
			{
				currentContext.SecondSelected = interactableObject;
				_ruleManager.Evaluate(currentContext);
				return;
			}

		}
		currentContext?.FirstSelected?.OnDeselected();
	}
	void Update()
	{

	}


	/// <summary>
	/// Gets objects at mouse position
	/// </summary>
	/// <returns>Objects</returns>
	private GameObject[] GetClickedGameObject()
	{
		Vector2 mouseWorldPos = MouseUser.MouseInWorldPosition;
		RaycastHit2D[] hit = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);
		GameObject[] result = hit.Where(h => h.collider != null).Select(h => h.collider.gameObject).ToArray();
		return result;
	}
}
