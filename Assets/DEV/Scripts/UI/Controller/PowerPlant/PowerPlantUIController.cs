using UnityEngine;
/// <summary>
/// A UI controller for power plant buildings, managing the display of static and dynamic data.
/// Inherits from BaseBuildUIController to provide specific functionality for power plants.
/// </summary>
public class PowerPlantUIController : BaseBuildUIController<PowerPlantData, BaseBuildingDynamicData>
{
	[SerializeField] private HealthDisplay _healthDisplay;
	private float _maxHealth;
	private void Start()
	{
		gameObject.SetActive(false);
	}

	public override void Active(PowerPlantData data, BaseBuildingDynamicData dynamicData)
	{
		_maxHealth = data.DynamicData.Health;
		base.Active(data, dynamicData);
	}
	protected override void DynamicDataUpdate()
	{
		if (m_dynamicData != null)
		{
			
			_healthDisplay.UpdateHealth(_maxHealth,m_dynamicData.Health);
		}
	}

}
