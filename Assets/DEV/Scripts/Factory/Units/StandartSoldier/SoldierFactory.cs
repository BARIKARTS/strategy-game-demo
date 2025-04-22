using UnityEngine;

/// <summary>
/// A factory class for spawning soldier units in the game.
/// Inherits from BaseUnitSpawner to provide specific spawning logic for soldiers.
/// </summary>
public class SoldierFactory : BaseUnitSpawner
{
	public SoldierFactory(BaseUnitData unitData) : base(unitData)
	{

	}

	public override GameObject Spawn(Vector2 position, byte team)
	{
		if (Data != null && Data.Prefab != null)
		{
			GameObject createObj = GameObject.Instantiate(Data.Prefab);
			createObj.transform.position = position;
			createObj.GetComponent<StandartUnitController>().Initialize(Data.UnitType, team, Data.DynamicData);
			return createObj;
		}
		return null;
	}
}
