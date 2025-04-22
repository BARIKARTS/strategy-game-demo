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
		Debug.Log($"update : {_dynamicData.AttackCooldown}");
		if (target != null && target.Health > 0 && Time.time - lastAttackTime >= _dynamicData.AttackCooldown)
		{
			target.TakeDamage(_dynamicData.Damage);
			lastAttackTime = Time.time;
		}
	}

	public override bool IsFinished()
	{
		return (target == null || target.Health <= 0);
	}
}