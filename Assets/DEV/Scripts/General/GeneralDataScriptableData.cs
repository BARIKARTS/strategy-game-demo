using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Building/BuildingData", fileName = "BuildingData")]
public class GeneralDataScriptableData : ScriptableObject
{
	[SerializeField] private List<BaseBuildingScriptableObject> Buildings;





	#region Building
	public bool TryGetBuildingData<T>(BuildingType buildingType, out T buildingData) where T : BaseBuildingData
	{
		buildingData = default;
		if (Buildings == null)
		{
			Debug.LogError($"{Buildings} is null");
			return false;
		}
		 buildingData = (T)Buildings.FirstOrDefault(b => b.BuildingType == buildingType).GetData();
		Debug.Log(buildingData.Name);
		return true;
	}
	#endregion
}