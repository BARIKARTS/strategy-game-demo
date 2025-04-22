
using UnityEngine;
/// <summary>
/// An abstract base class for spawning units in the game.
/// Provides a foundation for unit factories by storing unit data and defining a spawn method.
/// </summary>
public abstract class BaseUnitSpawner
{
	/// <summary>
	/// Gets the unit data associated with the spawner.
	/// </summary>
	public BaseUnitData Data { get; private set; }

	/// <summary>
	/// Initializes a new instance of the BaseUnitSpawner with the specified unit data.
	/// </summary>
	/// <param name="unitData">The unit data used for spawning units.</param>
	protected BaseUnitSpawner(BaseUnitData unitData)
	{
		Data = unitData;
	}
	/// <summary>
	/// Spawns a unit at the specified position with the given team.
	/// Must be implemented by derived classes to define specific spawning logic.
	/// </summary>
	/// <param name="position">The position where the unit will be spawned.</param>
	/// <param name="team">The team ID for the spawned unit.</param>
	/// <returns>The spawned GameObject.</returns>
	public abstract GameObject Spawn(Vector2 position, byte team);
}
