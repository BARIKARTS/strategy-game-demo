
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSpawner
{
	private List<BaseUnitSpawner> _baseUnitSpawners = new List<BaseUnitSpawner>();
	private CommonData _commonData => CommonData.Instance;
	private Dictionary<UnitType, Type> _unitTypes => new Dictionary<UnitType, Type>
	{
		{UnitType.Solider1,typeof(StandartSoldierFactory) }
	};

	public UnitSpawner()
	{
		Initialize();
	}
	private void Initialize()
	{
		Array enumsValue = Enum.GetValues(typeof(UnitType));
		foreach (UnitType value in enumsValue)
		{
			CreateSpawner(value);
		}
	}
	private void CreateSpawner(UnitType unitType)
	{
		if (_unitTypes.TryGetValue(unitType, out Type type))
		{
			if (_commonData.TryGetUnitData(unitType, out BaseUnitData data))
			{
				BaseUnitSpawner spawner = (BaseUnitSpawner)Activator.CreateInstance(type);
				spawner.Initialize(data);
				_baseUnitSpawners.Add(spawner);
			}
		}
	}
	public void Spawn(UnitType unitType, Vector2 position)
	{
		BaseUnitSpawner spawner = _baseUnitSpawners.FirstOrDefault(s => s.UnitType == unitType);
		if (spawner != null)
		{
			spawner.Spawn(position);
		}
		else
		{
			Debug.LogWarning($"{unitType} type spawner not found");
		}
	}
}
