using UnityEngine;

public class InputMediator : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	private ISelectable currentSelected;
	private InteractionResolver resolver;

	void Start()
	{
		resolver = new InteractionResolver();
		RegisterRules();
	}

	void Update()
	{
		if (!Input.GetMouseButtonDown(0)) return;

		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out RaycastHit hit)) return;

		var clicked = hit.collider.GetComponent<IInteractable>();

		if (clicked != null)
		{
			resolver.Resolve(currentSelected, clicked);

			if (clicked is ISelectable newSelection)
			{
				currentSelected?.OnDeselected();
				currentSelected = newSelection;
				currentSelected.OnSelected();
			}
		}
	}

	void RegisterRules()
	{
		//resolver.RegisterRule(new UnitToEnemyRule());
		//resolver.RegisterRule(new UnitToGroundRule());
		//resolver.RegisterRule(new BuildingToGroundRule());
	}
}
