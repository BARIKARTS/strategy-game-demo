using UnityEngine;

public class BarracksSpawner : BaseBuildingSpawner
{
	private BarracksData _data;
	public override void Initialize(BaseBuildingData buildingData)
	{
		base.Initialize(buildingData);
		if (buildingData != null && buildingData is BarracksData)
		{
			_data = (BarracksData)buildingData;
		}
		else
		{
			Debug.LogWarning($"  failed to initialize {nameof(BarracksSpawner)}");
		}
	}

	public override GameObject Spawn(Vector2 position, byte team)
	{
		if (m_data != null && _data != null && m_data.Prefab != null)
		{
			GameObject createObj = GameObject.Instantiate(m_data.Prefab);
			createObj.transform.position = position;
			if (createObj.TryGetComponent(out BarracksController barracksController))
			{

				barracksController.Initialize(m_data.BuildingType, team, _data.DynamicData);
			}
			else
			{
				Debug.LogError($"{nameof(barracksController)} not found");
			}
			return createObj;
		}
		Debug.Log($"{BuildingType} not found");
		return null;
	}
}
