using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(UnitPathfinding))]
public abstract class BaseUnitController : SelectableBase, IDamageable
{
	public byte Team { get; protected set; }
	public UnitType UnitType { get; protected set; }

	private UnitPathfinding _unitPathfinding;
	public UnitPathfinding UnitPathfinding => _unitPathfinding;

	protected virtual void Start()
	{
		_unitPathfinding = GetComponent<UnitPathfinding>();
	}
	public abstract void Attack(IDamageable target);
	public abstract void TakeDamage(float damage);
	public abstract float Health { get; }
	public abstract void MoveTo(Vector2 targetPosition);
	public abstract Vector2 Position { get; }
}
