
/// <summary>
/// Defines a contract for handling interaction rules based on a given context.
/// Implementations validate and execute specific interactions between objects.
/// </summary>
public interface IRuleHandler
{
	/// <summary>
	/// Determines if the rule can handle the given interaction context.
	/// </summary>
	/// <param name="interactionContext">The context containing the selected objects.</param>
	/// <returns>True if the rule can handle the interaction, false otherwise.</returns>
	bool CanHandle(InteractionContext interactionContext);

	/// <summary>
	/// Executes the interaction defined by the given context.
	/// </summary>
	/// <param name="interactionContext">The context containing the selected objects.</param>
	void Execute(InteractionContext interactionContext);
}
