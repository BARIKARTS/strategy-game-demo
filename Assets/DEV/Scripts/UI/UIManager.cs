using GameInput;
using System;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
	[SerializeField] private BarracksUIController _barracksUIController;
	[SerializeField] private PowerPlantUIController _powerPlantController;

	private UnityEvent _endPanelHideAction = new UnityEvent();
	private CommonData _commonData => CommonData.Instance;

	private ISelectable _endSelectable;
	private void OnEnable()
	{
		MouseUser.OnLeftMouseDown += OnMouseClick;
	}
	private void OnDisable()
	{
		MouseUser.OnLeftMouseDown -= OnMouseClick;

	}

	private void OnMouseClick()
	{
		RaycastHit2D hit = Physics2D.Raycast(MouseUser.MouseInWorldPosition, Vector2.zero);
		if (hit.collider != null)
		{
			Debug.Log("1");
			if (hit.collider.TryGetComponent(out ISelectable selectable))
			{
				Debug.Log("2");
				_endSelectable?.OnDeselected();
				selectable.OnSelected();
				_endSelectable = selectable;
			}
		}
		Debug.Log("3");

	}

	public void OpenBarracks(BuildingType buildingType, BarracksDynamicData dynamicData)
	{
		HideControllers();
		if (_commonData.TryGetBuildingData(buildingType, out BarracksData barracksData))
		{
			_endPanelHideAction.AddListener(_barracksUIController.Deactive);
			Debug.LogWarning(barracksData.Name);
			_barracksUIController?.Active(barracksData, dynamicData);
		}
	}

	public void HideControllers()
	{
		_endPanelHideAction?.Invoke();
		_endPanelHideAction.RemoveAllListeners();
	}
}
