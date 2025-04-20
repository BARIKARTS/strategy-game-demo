using UnityEngine;

public class AttackCommand : UnitCommand
{
	private IDamageable target;
	private float damage;
	private float attackCooldown;
	private float lastAttackTime;

	public AttackCommand(UnitController controller, IDamageable target) : base(controller)
	{
		this.target = target;
		this.damage = 10;
		this.attackCooldown = 1f;
	}

	public override void Execute()
	{
		Debug.Log($" target = {target}");
		if (target != null && target.Health > 0 && Time.time - lastAttackTime >= attackCooldown)
		{
			target.TakeDamage(damage);
			lastAttackTime = Time.time;
		}
	}

	public override bool IsFinished()
	{
		return (target == null || target.Health <= 0);
	}
}