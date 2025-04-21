
using UnityEngine;

public abstract class BaseUnitSpawner
{
	public abstract UnitType UnitType { get;}


	public abstract void Initialize(BaseUnitData unitData);

	public abstract GameObject Spawn(Vector2 position);
}
