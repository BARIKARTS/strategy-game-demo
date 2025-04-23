using UnityEngine;


/// <summary>
/// A UI controller for standard units, managing the display of unit data and dynamic data.
/// Inherits from BaseUnitUIController to provide specific functionality for standard units.
/// </summary>
public class StandartUnitUIController : BaseUnitUIController<BaseUnitData, BaseUnitDynamicData>
{
	//summary: Reference to the HealthDisplay component for updating unit health UI
	[SerializeField] private HealthDisplay _healthDisplay;
	private float _maxHalth;
	private void Start()
	{
		gameObject.SetActive(false);
	}
	//summary: Activates the UI controller, setting the maximum health and initializing base class functionality
	public override void Active(BaseUnitData data, BaseUnitDynamicData dynamicData)
	{
		_maxHalth = data.DynamicData.Health;
		base.Active(data, dynamicData);

	}

	//summary: Updates the health display based on the current dynamic data
	protected override void DynamicDataUpdate()
	{
		if (m_dynamicData != null)
		{
			_healthDisplay?.UpdateHealth(_maxHalth, m_dynamicData.Health);
		}
	}
}
