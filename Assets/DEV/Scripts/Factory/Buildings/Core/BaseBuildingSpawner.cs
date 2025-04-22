
using UnityEngine;

public abstract class BaseBuildingSpawner
{
	public abstract BuildingType BuildingType { get; }


	public abstract void Initialize(BaseBuildingData unitData);

	public abstract GameObject Spawn(Vector2 position, byte team);
}
