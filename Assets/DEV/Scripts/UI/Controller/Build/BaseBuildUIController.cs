using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// An abstract base UI controller for managing building-related UI panels.
/// Handles activation, deactivation, and dynamic data updates for building data and dynamic data.
/// </summary>
/// <typeparam name="T1">The type of static building data, derived from BaseBuildingData.</typeparam>
/// <typeparam name="T2">The type of dynamic building data, derived from BaseBuildingDynamicData.</typeparam>
public abstract class BaseBuildUIController<T1, T2> : PanelController<T1, T2> where T1 : BaseBuildingData where T2 : BaseBuildingDynamicData
{
	[Header("MAIN REFERENCES")]
	[SerializeField] protected TextMeshProUGUI _name;
	[SerializeField] protected TextMeshProUGUI _description;
	[SerializeField] protected Image _previewIcon;
	protected BuildingType _currentType;
	/// <summary>
	/// Activates the UI panel with the specified static and dynamic building data.
	/// Sets the name, description, icon, and subscribes to dynamic data events.
	/// </summary>
	/// <param name="data">The static building data to display.</param>
	/// <param name="dynamicData">The dynamic building data to track.</param>
	public override void Active(T1 data, T2 dynamicData)
	{
		_currentType = data.BuildingType;
		_name.text = data.Name;
		_description.text = data.Description;
		_previewIcon.sprite = data.Icon;
		m_dynamicData = dynamicData;
		Subscribe();
		DynamicDataUpdate();
		gameObject.SetActive(true);
	}


	
	public override void Deactive()
	{
		gameObject.SetActive(false);
		Unsubscribe();
	}
	protected override void Subscribe()
	{
		if(m_dynamicData != null)
		{

		m_dynamicData.OnDataChange += DynamicDataUpdate;
		m_dynamicData.OnDestroy += DestroyBuilding;
		}
	}

	protected override void Unsubscribe()
	{
		if (m_dynamicData != null)
		{
			m_dynamicData.OnDataChange -= DynamicDataUpdate;
			m_dynamicData.OnDestroy -= DestroyBuilding;
		}
	}

	protected abstract void DynamicDataUpdate();
	protected virtual void DestroyBuilding()
	{
		Deactive();
	}

}
