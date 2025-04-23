using System.Linq;
using UnityEngine;
/// <summary>
/// Stores building and unit data as ScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "Building/BuildingData", fileName = "BuildingData")]
public class GeneralSOData : ScriptableObject
{
	[SerializeField] private BaseBuildingSO[] Buildings;//Array of building data
	[SerializeField] private BaseUnitSO[] Units;//Array of unit data

	#region Building
	/// <summary>
	/// Gets building data by type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="buildingType"></param>
	/// <param name="buildingData"></param>
	/// <returns></returns>
	public bool TryGetBuildingData<T>(BuildingType buildingType, out T buildingData) where T : BaseBuildingData
	{
		buildingData = default;
		if (Buildings == null)
		{
			Debug.LogError($"{nameof(Buildings)} is null");
			return false;
		}
		BaseBuildingSO baseSO = Buildings.FirstOrDefault(b => b?.BuildingType == buildingType);
		if (baseSO != null)
		{
			buildingData = (T)baseSO.GetData();
			return true;
		}
		return false;
	}
	/// <summary>
	/// Gets all building data
	/// </summary>
	/// <returns></returns>
	public BaseBuildingData[] GetBuildingsData() => Buildings.Select(b => b.GetData()).ToArray();
	#endregion

	#region Units
	/// <summary>
	/// Gets unit data by type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="unitType"></param>
	/// <param name="unitData"></param>
	/// <returns></returns>
	public bool TryGetUnitData<T>(UnitType unitType, out T unitData) where T : BaseUnitData
	{
		unitData = default;
		if (Units == null)
		{
			Debug.LogError($"{nameof(Units)} is null");
			return false;
		}
		BaseUnitSO baseUnitSO = Units.FirstOrDefault(b => b.UnitType == unitType);
		if (baseUnitSO != null)
		{
			unitData = (T)baseUnitSO.GetData();
			return true;
		}
		return false;
	}
	#endregion
}