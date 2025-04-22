/// <summary>
/// Manages a collection of interaction rules and evaluates them based on the provided context.
/// Executes the first applicable rule that can handle the interaction.
/// </summary>
public class RuleManager
{

	private IRuleHandler[] rules => new IRuleHandler[]
	{
		new MoveUnitToGroundRule(),
		new UnitAttackEnemyRule(),
	};

	/// <summary>
	/// Evaluates the interaction context against the registered rules.
	/// Executes the first rule that can handle the context, if any.
	/// </summary>
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
