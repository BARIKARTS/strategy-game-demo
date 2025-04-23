using Pathfinding;
using UnityEngine;

/// <summary>: Abstract base class for unit controllers, handling behavior and damage
[RequireComponent(typeof(UnitPathfinding))]
public abstract class BaseUnitController : SelectableBase, IDamageable
{
	public byte Team { get; protected set; }// Unit's team ID
	public abstract float Health { get; }//Unit  current health
	public abstract Vector2 Position { get; }//Unit's current position
	public UnitType UnitType { get; protected set; } //Unit's type

	private UnitPathfinding _unitPathfinding; //UnitPathfinding component for movement
	public UnitPathfinding UnitPathfinding => _unitPathfinding; //Access to UnitPathfinding component

	//Initializes  components
	protected virtual void Start()
	{
		_unitPathfinding = GetComponent<UnitPathfinding>();
	}

	/// <summary>: Attacks a target
	public abstract void Attack(IDamageable target);
	/// 
	/// <summary>: Handles receiving damage
	public abstract void TakeDamage(float damage);

	/// <summary>: Moves unit to target position
	public abstract void MoveTo(Vector2 targetPosition);

	/// <summary>: Changes unit team
	public void ChangeTeam(byte team)=>Team = team;
}
