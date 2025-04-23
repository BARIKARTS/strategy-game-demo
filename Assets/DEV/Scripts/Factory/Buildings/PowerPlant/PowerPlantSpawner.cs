using UnityEngine;
/// <summary>
/// Spawns power plant buildings
/// </summary>
public class PowerPlantSpawner : BaseBuildingSpawner
{
	private PowerPlantData _data;//Stores power plant data

	public override void Initialize(BaseBuildingData buildingData)
	{
		base.Initialize(buildingData);
		if (buildingData != null && buildingData is PowerPlantData)
		{
			_data = (PowerPlantData)buildingData;
		}else
		{
			Debug.LogWarning($"failed to initialize {nameof(PowerPlantSpawner)}");
		}
	}
	public override GameObject Spawn(Vector2 position, byte team)
	{
		if (m_data != null && _data != null && m_data.Prefab != null)
		{
			GameObject createObj = GameObject.Instantiate(m_data.Prefab);
			createObj.transform.position = position;
			if (createObj.TryGetComponent(out PowerPlantController powerPlantController))
			{

				powerPlantController.Initialize(m_data.BuildingType, team, _data.DynamicData);
			}
			else
			{
				Debug.LogError($"{nameof(powerPlantController)} not found");
			}
			return createObj;
		}
		Debug.Log($"{BuildingType} not found");
		return null;
	}
}
