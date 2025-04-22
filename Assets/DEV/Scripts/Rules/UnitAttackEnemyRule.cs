using UnityEngine;

/// <summary>
/// A rule handler that processes an attack interaction between a unit and a damageable target.
/// Validates if the interaction involves a unit and a damageable entity, then triggers the attack.
/// </summary>
public class UnitAttackEnemyRule : IRuleHandler
{
	public bool CanHandle(InteractionContext interactionContext)
	{
		return interactionContext?.FirstSelected is BaseUnitController && interactionContext?.SecondSelected is IDamageable;
	}

	public void Execute(InteractionContext interactionContext)
	{
		Debug.Log("Execute");
		var unit = interactionContext.FirstSelected as BaseUnitController;
		IDamageable damageable = interactionContext.SecondSelected as IDamageable;
		unit.Attack(damageable);
	}
}
