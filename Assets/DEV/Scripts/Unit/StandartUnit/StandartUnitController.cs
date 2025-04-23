using DG.Tweening;
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
		m_dynamicData.Health -= damage;
		PlayDamageAnimation();
		if (m_dynamicData.Health <= 0)
		{
			Destroy(gameObject);
		}
	}
	private void PlayDamageAnimation()
	{
		Sequence damageSequence = DOTween.Sequence();
		damageSequence.Append(transform.DOShakePosition(0.3f, 0.1f)) 
					  .Join(selectionRenderer?.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo)); 
	}

	public override void MoveTo(Vector2 targetPosition)
	{
		AllClearStates();
		AddCommand(new MoveState(this, targetPosition,m_dynamicData.Speed));
	}

	public override void OnSelected()
	{
		base.OnSelected();
		_uiManager?.OpenUnitPanel(UnitType, m_dynamicData);
	}
	public override void OnDeselected()
	{
		base.OnDeselected();
		_uiManager?.HideController();
	}
}
