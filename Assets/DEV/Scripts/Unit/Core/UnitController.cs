using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// An abstract base class for controlling units with generic dynamic data.
/// Manages unit commands, health, position, and initialization.
/// </summary>
/// <typeparam name="T2">The type of dynamic data, derived from BaseUnitDynamicData.</typeparam>
public abstract class UnitController<T2> : BaseUnitController where T2 : BaseUnitDynamicData
{
	public override float Health
	{
		get
		{
			if (m_dynamicData != null)
			{
				return m_dynamicData.Health;
			}
			Debug.LogError($"{nameof(m_dynamicData)} is null");
			return 0;
		}
	}
	protected T2 m_dynamicData;
	public T2 DynamicData => m_dynamicData;
	private Queue<BaseUnitState> commandQueue = new Queue<BaseUnitState>();
	private BaseUnitState _currentCommand;


	/// <summary>
	/// Adds a new command to the unit's command queue.
	/// </summary>
	/// <param name="command">The command to be added to the queue.</param>
	protected virtual void AddCommand(BaseUnitState command)
	{
		commandQueue.Enqueue(command);
	}
	public override Vector2 Position => transform.position;

	/// <summary>
	/// Initializes the unit with the specified type, team, and dynamic data.
	/// Creates a new instance of the dynamic data based on the provided default data.
	/// </summary>
	/// <param name="unitType">The type of the unit.</param>
	/// <param name="team">The team ID of the unit.</param>
	/// <param name="defaultData">The default dynamic data to initialize the unit.</param>
	public virtual void Initialize(UnitType unitType, byte team, BaseUnitDynamicData defaultData)
	{
		Team = team;
		if (defaultData == null) Debug.LogError($"data is null");
		BaseUnitDynamicData data = (BaseUnitDynamicData)Activator.CreateInstance(typeof(T2), args: defaultData);
		m_dynamicData = data as T2;
		UnitType = unitType;

	}


	/// <summary>
	/// Clears all commands from the command queue and resets the current command.
	/// </summary>
	protected virtual void AllClearStates()
	{
		commandQueue.Clear();
		_currentCommand = null;
	}

	/// <summary>
	/// Processes the current command or dequeues a new one if none is active.
	/// Updates the current command and clears it when finished.
	/// </summary>
	protected virtual void ProcessCommand()
	{
		if (_currentCommand == null && commandQueue.Count > 0)
		{
			_currentCommand = commandQueue.Dequeue();
		}
		if (_currentCommand != null)
		{
			_currentCommand.Update();

			if (_currentCommand.IsFinished())
			{
				_currentCommand = null;
			}
		}
	}

	/// <summary>
	/// Invokes the OnDestroy event of the dynamic data when the unit is destroyed.
	/// </summary>
	protected virtual void OnDestroy()
	{
		m_dynamicData?.OnDestroy?.Invoke();
	}



}
