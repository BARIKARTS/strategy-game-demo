using UnityEngine;

public class BarracksSpawner : BaseBuildingSpawner
{
	private BarracksData _data;
	public override BuildingType BuildingType => BuildingType.Barracks;
	public override void Initialize(BaseBuildingData unitData)
	{
		_data = (BarracksData)unitData;
	}

	public override GameObject Spawn(Vector2 position, byte team)
	{
		if (_data != null && _data.Prefab != null)
		{
			GameObject createObj = GameObject.Instantiate(_data.Prefab);
			createObj.transform.position = position;
			if (createObj.TryGetComponent(out BarracksController barracksController))
			{

				barracksController.Initialize(_data.BuildingType,team ,_data.DynamicData);
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
