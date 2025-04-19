using UnityEngine;
[CreateAssetMenu(menuName = "Building/New PowerPlant Item", fileName = "New PowerPlant Item")]
public class PowerPlantScriptable : BaseBuildingScriptableObject
{
	[field: SerializeField] public PowerPlantData PowerPlantData { get; private set; }
	public override BuildingType BuildingType => PowerPlantData.BuildingType;

	public override BaseBuildingData GetData() => PowerPlantData;

}
