/// <summary>: Handles setting spawn position for barracks
public class SetBarracksSpawnPositionRule : IRuleHandler
{
	public bool CanHandle(InteractionContext interactionContext)
	{
		return interactionContext?.FirstSelected is BarracksController && interactionContext?.SecondSelected is InteractableTileMap;

		//Teams are checked with consideration!
		//return interactionContext?.FirstSelected is BarracksController controller && controller.Team == interactionContext.Team
		//	&& interactionContext?.SecondSelected is InteractableTileMap;
	}

	public void Execute(InteractionContext interactionContext)
	{
		BarracksController controller = interactionContext.FirstSelected as BarracksController;
		controller?.SetSpawnPosition(interactionContext.ClickedPosition.Value);
	}
}
