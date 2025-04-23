using System;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
	[SerializeField] private byte _team = 5;
	[SerializeField] private BuildSpawnSettings[] _buildSpawnSettings;
	[SerializeField] private UnitSpawnSettings[] _unitSpawnSettings;

	private FactoryManager _factoryManager => FactoryManager.Instance;

	public void Initialize()
	{
		CreateBuildings();
		CreateUnits();
	}
	public void CreateBuildings()
	{
		if (_buildSpawnSettings == null) return;
		BuildSpawnSettings currentSettings;
		GameObject createObj;
		BaseBuildingController controller;
		for (int i = 0; i < _buildSpawnSettings.Length; i++)
		{
			currentSettings = _buildSpawnSettings[i];
			createObj = _factoryManager.BuildingSpawn(currentSettings.BuildingType, currentSettings.Position);
			if (createObj.TryGetComponent(out controller))
			{
				controller.ChangeTeam(_team);
			}
		}

	}

	public void CreateUnits()
	{
		if (_buildSpawnSettings == null) return;
		UnitSpawnSettings currentSettings;
		GameObject createObj;
		BaseUnitController controller;
		for (int i = 0; i < _unitSpawnSettings.Length; i++)
		{
			currentSettings = _unitSpawnSettings[i];
			createObj = _factoryManager.UnitSpawn(currentSettings.UnitType, currentSettings.Position);
			if (createObj.TryGetComponent(out controller))
			{
				controller.ChangeTeam(_team);
			}
		}

	}

}

[Serializable]
public class BuildSpawnSettings
{
	[field: SerializeField] public Vector2 Position { get; private set; }
	[field: SerializeField] public BuildingType BuildingType { get; private set; }
}


[Serializable]
public class UnitSpawnSettings
{
	[field: SerializeField] public Vector2 Position { get; private set; }
	[field: SerializeField] public UnitType UnitType { get; private set; }
}

