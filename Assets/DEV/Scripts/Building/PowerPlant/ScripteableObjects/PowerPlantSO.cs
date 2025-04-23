using UnityEngine;
[CreateAssetMenu(menuName = "Building/New PowerPlant Item", fileName = "New PowerPlant Item")]

/// <summary>
/// A ScriptableObject representing a power plant building, storing its static data.
/// Provides access to power plant data and building type for use in building initialization.
/// </summary>
public class PowerPlantSO : BaseBuildingSO
{
	[field: SerializeField] public PowerPlantData PowerPlantData { get; private set; }
	public override BuildingType BuildingType => PowerPlantData.BuildingType;

	public override BaseBuildingData GetData() => PowerPlantData;

}
