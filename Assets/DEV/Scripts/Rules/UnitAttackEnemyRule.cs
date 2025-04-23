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

		//Teams are checked with consideration!
		//return interactionContext?.FirstSelected is BaseUnitController controller && controller.Team == interactionContext.Team
		//	&& interactionContext?.SecondSelected is IDamageable damageable && damageable.Team != interactionContext.Team;
	}

	public void Execute(InteractionContext interactionContext)
	{
		var unit = interactionContext.FirstSelected as BaseUnitController;
		IDamageable damageable = interactionContext.SecondSelected as IDamageable;
		unit.Attack(damageable);
	}
}
