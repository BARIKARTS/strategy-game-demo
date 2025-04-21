using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Building/BuildingData", fileName = "BuildingData")]
public class GeneralDataScriptableData : ScriptableObject
{
	[SerializeField] private BaseBuildingScriptableObject[] Buildings;
	[SerializeField] private BaseUnitScriptableObject[] Units;




	#region Building
	public bool TryGetBuildingData<T>(BuildingType buildingType, out T buildingData) where T : BaseBuildingData
	{
		buildingData = default;
		if (Buildings == null)
		{
			Debug.LogError($"{nameof(Buildings)} is null");
			return false;
		}
		buildingData = (T)Buildings.FirstOrDefault(b => b.BuildingType == buildingType).GetData();
		Debug.Log(buildingData.Name);
		return true;
	}
	public BaseBuildingData[] GetBuildingsData() => Buildings.Select(b => b.GetData()).ToArray();
	#endregion

	#region Units
	public bool TryGetUnitData<T>(UnitType unitType, out T unitData) where T : BaseUnitData
	{
		unitData = default;
		if (Units == null)
		{
			Debug.LogError($"{nameof(Units)} is null");
			return false;
		}
		unitData = (T)Units.FirstOrDefault(b => b.UnitType == unitType).GetData();
		Debug.Log(unitData.Name);
		return true;
	}
	#endregion
}