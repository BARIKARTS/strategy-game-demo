using Pathfinding;
using UnityEngine;

public class FactoryManager : SingletonMonoBehaviour<FactoryManager>
{
	private UnitSpawner _unitSpawner;
	private BuildingSpawner _buildingSpawner;

	private AstarPathfindingManager _pathfindingManager => AstarPathfindingManager.Instance;
	private void Start()
	{
		CreateSpawners();
	}

	private void CreateSpawners()
	{
		_unitSpawner = new UnitSpawner();
		_buildingSpawner = new BuildingSpawner();

	}

	public void BuildingSpawn(BuildingType buildingType, Vector2 position)
	{
		GameObject building = _buildingSpawner.Spawn(buildingType, position);
		if (building != null) _pathfindingManager.PlaceStructure(building);
	}

	public void UnitSpawn(UnitType unitType, Vector2 position)
	{
		_unitSpawner.Spawn(unitType, position);
	}

	[ContextMenu("TestUnitSpawn")]
	private void TestUnitSpawn()
	{
	}
	[ContextMenu("TestBuildingSpawn")]
	private void TestBuildingSpawn()
	{
		_buildingSpawner.Spawn(BuildingType.Barracks, Vector2.zero);
	}

}
