using Pathfinding;
using UnityEngine;

/// <summary>
/// A controller for standard units, managing movement, attacks, and UI interactions.
/// Inherits from UnitController to provide specific functionality for standard units.
/// </summary>
[RequireComponent(typeof(UnitPathfinding))]
public class StandartUnitController : UnitController<BaseUnitDynamicData>
{

	private UIManager _uiManager => UIManager.Instance;
	protected override void Start()
	{
		base.Start();
		selectionRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		ProcessCommand();
	}

	public override void Attack(IDamageable target)
	{

		MoveTo(target.Position);
		AddCommand(new AttackState(this, target));
	}

	public override void TakeDamage(float damage)
	{
		Debug.Log(m_dynamicData.Healt);
		m_dynamicData.Healt -= damage;
		Debug.Log(m_dynamicData.Healt);
		if (m_dynamicData.Healt <= 0)
		{
			Destroy(gameObject);
		}
	}
	/// <summary>
	/// Moves the unit to the specified target position.
	/// Clears existing commands and queues a new movement command.
	/// </summary>
	/// <param name="targetPosition">The position to move towards.</param>
	public override void MoveTo(Vector2 targetPosition)
	{
		AllClearStates();
		AddCommand(new MoveState(this, targetPosition));
	}

	public override void OnSelected()
	{
		_uiManager?.OpenUnitPanel(UnitType, m_dynamicData);
	}
	public override void OnDeselected()
	{
		_uiManager?.HideController();
	}
}
