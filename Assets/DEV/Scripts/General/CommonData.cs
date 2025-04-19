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
}
