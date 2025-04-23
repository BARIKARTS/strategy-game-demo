using UnityEngine;

/// <summary>
/// A singleton manager for controlling UI panels in the game.
/// Manages the activation and deactivation of barracks, unit, and power plant UI panels.
/// </summary>
public class UIManager : SingletonMonoBehaviour<UIManager>
{
	[SerializeField] private BarracksUIController _barracksUIController;
	[SerializeField] private StandartUnitUIController _unitUIController;
	[SerializeField] private PowerPlantUIController _powerPlantController;

	private CommonData _commonData => CommonData.Instance;

	private BasePanelController _basePanelController;

	/// <summary>
	/// Opens the barracks UI panel for the specified building type with its dynamic data.
	/// Hides the currently active panel, if any, before activating the barracks panel.
	/// </summary>
	/// <param name="buildingType">The type of the building to display in the UI.</param>
	/// <param name="dynamicData">The dynamic data associated with the barracks.</param>
	public void OpenBarracks(BuildingType buildingType, BarracksDynamicData dynamicData)
	{
		HideController();
		if (_commonData.TryGetBuildingData(buildingType, out BarracksData barracksData))
		{
			_basePanelController = _barracksUIController;
			_barracksUIController?.Active(barracksData, dynamicData);
		}
	}

	/// <summary>
	/// Opens the unit UI panel for the specified unit type with its dynamic data.
	/// Hides the currently active panel, if any, before activating the unit panel.
	/// </summary>
	/// <param name="unitType">The type of the unit to display in the UI.</param>
	/// <param name="dynamicData">The dynamic data associated with the unit.</param>
	public void OpenUnitPanel(UnitType unitType, BaseUnitDynamicData dynamicData)
	{
		HideController();
		if (_commonData.TryGetUnitData(unitType, out BaseUnitData baseUnitData))
		{
			_unitUIController?.Active(baseUnitData, dynamicData);
			_basePanelController = _unitUIController;
		}
	}
	/// <summary>
	/// Deactivates the currently active UI panel, if any.
	/// </summary>
	
	public void OpenPowerPlantPanel(BuildingType buildingType, BaseBuildingDynamicData dynamicData)
	{
		HideController();

		if (_commonData.TryGetBuildingData(buildingType, out PowerPlantData powerPlantData))
		{
			_basePanelController = _powerPlantController;
			_powerPlantController?.Active(powerPlantData, dynamicData);
		}
	}
	public void HideController()
	{
		_basePanelController?.Deactive();
	}
}
