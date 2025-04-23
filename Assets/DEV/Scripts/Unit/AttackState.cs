using DG.Tweening;
using UnityEngine;

public class AttackState : BaseUnitState
{
	private IDamageable target;
	private BaseUnitDynamicData _dynamicData;
	private float lastAttackTime;
	private Transform transform => m_controller.transform;
	public AttackState(StandartUnitController controller, IDamageable target) : base(controller)
	{
		this.target = target;
		_dynamicData = controller.DynamicData;
	}

	public override void Update()
	{
		if (target != null && target.Health > 0 && Time.time - lastAttackTime >= _dynamicData.AttackCooldown)
		{
			if (Vector2.Distance(target.Position, m_controller.transform.position) <= _dynamicData.AttackDistance)
			{
				Attack();
			}
			else
			{
				m_controller.Attack(target);
			}
		}
	}
	private void Attack()
	{
		if (m_controller == null) return;
		Sequence attackSequence = DOTween.Sequence();
		attackSequence.Append(transform.DOLocalMoveX(transform.position.x + 0.5f, 0.2f))
					  .Append(transform.DOLocalMoveX(transform.position.x, 0.2f))
					  .OnComplete(() =>
					  {
						  if (target != null)
						  {
							  target.TakeDamage(_dynamicData.Damage);
						  }
					  });
		lastAttackTime = Time.time;
	}
	public override bool IsFinished()
	{
		return (target == null || target.Health <= 0);
	}
}