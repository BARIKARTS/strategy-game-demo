using UnityEngine;

/// <summary>: Spawns standard soldier units
public class SoldierSpawner : BaseUnitSpawner
{
	private StandartUnitData _data;

	/// <summary>: Initializes spawner with unit data
	public override void Initialize(BaseUnitData unitData)
	{
		base.Initialize(unitData);
		if(unitData != null && unitData is StandartUnitData)
		{
			_data = (StandartUnitData)unitData;
		}else
		{
			Debug.LogWarning($"  failed to initialize {nameof(SoldierSpawner)}");
		}
	}
	/// <summary>: Spawns a soldier unit at a position
	public override GameObject Spawn(Vector2 position, byte team)
	{
		if (m_data != null && m_data.Prefab != null)
		{
			GameObject createObj = GameObject.Instantiate(m_data.Prefab);
			createObj.transform.position = position;
			createObj.GetComponent<StandartUnitController>().Initialize(m_data.UnitType, team, m_data.DynamicData);
			return createObj;
		}
		return null;
	}
}
