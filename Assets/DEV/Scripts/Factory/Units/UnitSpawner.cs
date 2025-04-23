using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Managerial class for soldier production
/// </summary>
public class UnitSpawner
{
	private List<BaseUnitSpawner> _baseUnitSpawners = new List<BaseUnitSpawner>();
	private CommonData _commonData => CommonData.Instance;

	/// <summary>
	/// holds the spawner corresponding to the soldier type
	/// </summary>
	private Dictionary<UnitType, Type> _unitTypes => new Dictionary<UnitType, Type>
	{
		{UnitType.Clown,typeof(SoldierSpawner) },
		{UnitType.Crossbone,typeof(SoldierSpawner) },
		{UnitType.Devil,typeof(SoldierSpawner) },
		{UnitType.Ghost,typeof(SoldierSpawner) },
		{UnitType.Goblin,typeof(SoldierSpawner) },
		{UnitType.Ogre,typeof(SoldierSpawner) },
		{UnitType.Skull,typeof(SoldierSpawner) },
		{UnitType.Zombie,typeof(SoldierSpawner) },
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
	/// <summary>
	/// finds and assigns a suitable spawner
	/// </summary>
	/// <param name="unitType">the type of soldier desired to be produced</param>
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

	/// <summary>
	/// makes production
	/// </summary>
	/// <param name="unitType">the type of soldier desired to be produced</param>
	/// <param name="position">spawn position</param>
	public GameObject Spawn(UnitType unitType, Vector2 position)
	{
		BaseUnitSpawner spawner = _baseUnitSpawners.FirstOrDefault(s => s?.UnitType == unitType);
		if (spawner != null)
		{
			return spawner.Spawn(position, _commonData.Team);
		}
		else
		{
			Debug.LogWarning($"{unitType} type spawner not found");
			return null;
		}
	}
}
