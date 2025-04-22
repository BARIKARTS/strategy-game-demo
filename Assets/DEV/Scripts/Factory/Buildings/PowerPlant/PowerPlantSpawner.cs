using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantSpawner : BaseBuildingSpawner
{
	private PowerPlantData _data;
	public override BuildingType BuildingType => _data.BuildingType;
	public override void Initialize(BaseBuildingData unitData)
	{
		_data = (PowerPlantData)unitData;
	}

	public override GameObject Spawn(Vector2 position, byte team)
	{
		if (_data != null && _data.Prefab != null)
		{
			GameObject createObj = GameObject.Instantiate(_data.Prefab);
			createObj.transform.position = position;
			if (createObj.TryGetComponent(out PowerPlantController powerPlantController))
			{

				powerPlantController.Initialize(_data.BuildingType, team, _data.DynamicData);
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
