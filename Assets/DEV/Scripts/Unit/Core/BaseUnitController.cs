using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(UnitPathfinding))]
public abstract class BaseUnitController<T2> : SelectableBase, IDamageable where T2 : BaseUnitDynamicData
{
	public UnitType UnitType { get; private set; }
	public float Health
	{
		get
		{
			if (m_dynamicData != null)
			{
				return m_dynamicData.Healt;
			}
			Debug.LogError($"{nameof(m_dynamicData)} is null");
			return 0;
		}
	}
	protected T2 m_dynamicData;

	public abstract void TakeDamage(float damage);

	private UnitPathfinding _unitPathfinding;
	public UnitPathfinding UnitPathfinding => _unitPathfinding;

	private Queue<UnitCommand> commandQueue = new Queue<UnitCommand>();
	private UnitCommand _currentCommand;


	protected virtual void Start()
	{
		_unitPathfinding = GetComponent<UnitPathfinding>();
	}
	protected virtual void AddCommand(UnitCommand command)
	{
		commandQueue.Enqueue(command);
	}
	public Vector2 GetPosition() => transform.position;
	public virtual void Initialize(UnitType unitType, BaseUnitDynamicData defaultData)
	{
		if (defaultData == null) Debug.LogError($"data is null");
		BaseUnitDynamicData data = (BaseUnitDynamicData)Activator.CreateInstance(typeof(T2), args: defaultData);
		m_dynamicData = data as T2;
		UnitType = unitType;

	}

	protected virtual void ProcessCommand()
	{
		if (_currentCommand == null && commandQueue.Count > 0)
		{
			_currentCommand = commandQueue.Dequeue();
		}
		if (_currentCommand != null)
		{
			_currentCommand.Execute();
			if (_currentCommand.IsFinished())
			{
				_currentCommand = null;
			}
		}
	}



}
