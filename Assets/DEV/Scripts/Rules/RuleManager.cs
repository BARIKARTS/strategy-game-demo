/// <summary>: Manages and executes interaction rules based on context
public class RuleManager
{
	// Array of interaction rule handlers
	private IRuleHandler[] rules => new IRuleHandler[]
	{
		new MoveUnitToGroundRule(),
		new UnitAttackEnemyRule(),
		new SetBarracksSpawnPositionRule(),
	};

	/// <summary>: Executes first applicable rule for the context</summary>>
	/// <param name="context">The interaction context containing the selected objects.</param>
	public void Evaluate(InteractionContext context)
	{
		if (context.FirstSelected != null)
		{
			foreach (var rule in rules)
			{
				if (rule.CanHandle(context))
				{
					rule.Execute(context);
					break;
				}
			}
		}
	}
}
