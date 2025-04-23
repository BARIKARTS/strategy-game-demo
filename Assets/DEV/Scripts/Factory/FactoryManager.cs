using Pathfinding;
using UnityEngine;

/// <summary>
/// Manages unit and building spawning
/// </summary>
public class FactoryManager : SingletonMonoBehaviour<FactoryManager>
{
	private UnitSpawner _unitSpawner;
	private BuildingSpawner _buildingSpawner;

	private AstarPathfindingManager _pathfindingManager => AstarPathfindingManager.Instance;
	public void Initialize()
	{
		CreateSpawners();
	}

	private void CreateSpawners()
	{
		_unitSpawner = new UnitSpawner();
		_buildingSpawner = new BuildingSpawner();

	}

	public GameObject BuildingSpawn(BuildingType buildingType, Vector2 position)
	{
		GameObject building = _buildingSpawner.Spawn(buildingType, position);
		if (building != null) _pathfindingManager.PlaceStructure(building);
		return building;
	}

	public GameObject UnitSpawn(UnitType unitType, Vector2 position)
	{
		GameObject unit = _unitSpawner.Spawn(unitType, position);
		return unit;
	}

}
