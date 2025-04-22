using GameInput;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private RuleManager _ruleManager;
	private InteractionContext currentContext = new();
	private void Start()
	{
		_ruleManager = new RuleManager();
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
		currentContext.FirstSelected?.OnDeselected();
		currentContext.FirstSelected = null;
	}

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
		//Debug.Log("3");
	}
	void Update()
	{

	}



	private GameObject[] GetClickedGameObject()
	{
		Vector2 mouseWorldPos = MouseUser.MouseInWorldPosition;
		RaycastHit2D[] hit = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);
		GameObject[] result = hit.Where(h => h.collider != null).Select(h => h.collider.gameObject).ToArray();
		return result;
	}
}
