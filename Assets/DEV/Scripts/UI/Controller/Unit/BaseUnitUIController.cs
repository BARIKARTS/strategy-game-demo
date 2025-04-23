using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>: Abstract base class for unit UI controllers, managing unit data and dynamic data display. Inherits from PanelController to provide generic UI functionality.
public abstract class BaseUnitUIController<T1, T2> : PanelController<T1, T2> where T1 : BaseUnitData where T2 : BaseUnitDynamicData
{
	[Header("MAIN REFERENCES")]
	[SerializeField] protected TextMeshProUGUI _name; //TextMeshProUGUI component for displaying the unit's name
	[SerializeField] protected TextMeshProUGUI _description; //TextMeshProUGUI component for displaying the unit's description
	[SerializeField] protected Image _previewIcon; //image component for displaying the unit's preview icon

	// <summary>: Activates the UI controller, setting up the unit's name, description, icon, and dynamic data, and subscribing to updates
	public override void Active(T1 data, T2 dynamicData)
	{
		_name.text = data.Name;
		_description.text = data.Description;
		_previewIcon.sprite = data.Icon;
		m_dynamicData = dynamicData;
		Subscribe();
		DynamicDataUpdate();
		gameObject.SetActive(true);
	}
	/// <summary>: Deactivates the UI controller, hiding the game object and unsubscribing from updates
	public override void Deactive()
	{
		gameObject.SetActive(false);
		Unsubscribe();
	}
	/// <summary>: Subscribes to dynamic data change and destroy events
	protected override void Subscribe()
	{
		m_dynamicData.OnDataChange += DynamicDataUpdate;
		m_dynamicData.OnDestroy += DestroyBuilding;
	}
	/// <summary>: Unsubscribes from dynamic data change and destroy events
	protected override void Unsubscribe()
	{
		m_dynamicData.OnDataChange -= DynamicDataUpdate;
		m_dynamicData.OnDestroy -= DestroyBuilding;
	}
	/// <summary>:  dynamic data changes
	protected abstract void DynamicDataUpdate();

	//Virtual method to handle the destruction of the unit, deactivating the UI
	protected virtual void DestroyBuilding()
	{
		Deactive();
	}
}
