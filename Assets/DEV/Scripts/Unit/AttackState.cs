using UnityEngine;

public class AttackState : BaseUnitState
{
	private IDamageable target;
	private BaseUnitDynamicData _dynamicData;
	private float lastAttackTime;
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
				target.TakeDamage(_dynamicData.Damage);
				lastAttackTime = Time.time;
			}
			else
			{
				m_controller.Attack(target);
			}
		}
	}

	public override bool IsFinished()
	{
		return (target == null || target.Health <= 0);
	}
}