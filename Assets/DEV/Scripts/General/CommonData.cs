using UnityEngine;

/// <summary>
/// Manages shared game data singleton
/// </summary>
public class CommonData : SingletonMonoBehaviour<CommonData>
{

	[SerializeField] private GeneralSOData _scripteableData;//ScriptableObject for building/unit data
	[field: SerializeField] public byte Team { get; private set; } //Team ID

	/// <summary>
	/// Gets building data by type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="buildingType">Building type</param>
	/// <param name="baseBuildingData">Output building data</param>
	/// <returns></returns>
	public bool TryGetBuildingData<T>(BuildingType buildingType, out T baseBuildingData) where T : BaseBuildingData
	{
		bool status = _scripteableData.TryGetBuildingData(buildingType, out baseBuildingData);
		return status;
	}

	/// <summary>
	/// Gets base building data by type
	/// </summary>
	/// <param name="buildingType">Building type</param>
	/// <param name="baseBuildingData">Output base building data</param>
	/// <returns></returns>
	public bool TryGetBuildingData(BuildingType buildingType, out BaseBuildingData baseBuildingData)
	{
		bool status = _scripteableData.TryGetBuildingData(buildingType, out baseBuildingData);
		return status;
	}
	/// <summary>
	/// Gets unit data by type
	/// </summary>
	/// <param name="unitType">Unit type</param>
	/// <param name="baseUnitData">Output unit data</param>
	/// <returns></returns>
	public bool TryGetUnitData(UnitType unitType, out BaseUnitData baseUnitData)
	{
		bool status = _scripteableData.TryGetUnitData(unitType, out baseUnitData);
		return status;
	}

	/// <summary>
	/// Gets all building data
	/// </summary>
	public BaseBuildingData[] GetAllBuildingsData() => _scripteableData.GetBuildingsData();

}
