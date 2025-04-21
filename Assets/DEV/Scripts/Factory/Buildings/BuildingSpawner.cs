using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingSpawner 
{
	private List<BaseBuildingSpawner> _baseUnitSpawners = new List<BaseBuildingSpawner>();
	private CommonData _commonData => CommonData.Instance;
	private Dictionary<BuildingType, Type> _unitTypes => new Dictionary<BuildingType, Type>
	{
		{BuildingType.Barracks,typeof(BarracksSpawner) }
	};

	public BuildingSpawner()
	{
		Initialize();
	}
	public void Initialize()
	{
		Array enumsValue = Enum.GetValues(typeof(BuildingType));
		foreach (BuildingType value in enumsValue)
		{
			CreateSpawner(value);
		}
	}
	private void CreateSpawner(BuildingType unitType)
	{
		Debug.Log(1);
		if (_unitTypes.TryGetValue(unitType, out Type type))
		{
		Debug.Log(5);
			if (_commonData.TryGetBuildingData(unitType, out BaseBuildingData data))
			{
		Debug.Log(0	);
				BaseBuildingSpawner spawner = (BaseBuildingSpawner)Activator.CreateInstance(type);
				spawner.Initialize(data);
				_baseUnitSpawners.Add(spawner);
			}
		}
	}
	public GameObject Spawn(BuildingType buildingType, Vector2 position)
	{
		BaseBuildingSpawner spawner = _baseUnitSpawners.FirstOrDefault(s => s.BuildingType == buildingType);
		Debug.Log(2);
		if (spawner != null)
		{
			return spawner.Spawn(position);
		}
		else
		{
			Debug.LogWarning($"{buildingType} type spawner not found");
			return null;
		}
	}
}
