using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : SingletonMonoBehaviour<CommonData>
{
	[SerializeField] private GeneralDataScriptableData _scripteableData;

	public bool TryGetBuildingData<T>(BuildingType buildingType, out T baseBuildingData) where T : BaseBuildingData
	{
		bool status = _scripteableData.TryGetBuildingData(buildingType, out baseBuildingData);
		return status;
	}

	public bool TryGetBuildingData(BuildingType buildingType, out BaseBuildingData baseBuildingData)
	{
		bool status = _scripteableData.TryGetBuildingData(buildingType, out baseBuildingData);
		return status;
	}

	public bool TryGetUnitData(UnitType unitType, out BaseUnitData baseUnitData)
	{
		bool status = _scripteableData.TryGetUnitData(unitType, out baseUnitData);
		return status;
	}

	public BaseBuildingData[] GetAllBuildingsData() => _scripteableData.GetBuildingsData();

	public BaseUnitData[] GetBarracksUnitData(BuildingType buildingType)
	{
		if(TryGetBuildingData(buildingType, out BarracksData data))
		{
		}
		return null;
	}
}
