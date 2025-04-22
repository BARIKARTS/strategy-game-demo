/// <summary>
/// A rule handler that manages the movement of a unit to a specified position on an interactable ground or tile map.
/// </summary>
public class MoveUnitToGroundRule : IRuleHandler
{
	public bool CanHandle(InteractionContext interactionContext)
	{
		return interactionContext?.FirstSelected is BaseUnitController && interactionContext?.SecondSelected is InteractableTileMap;
	}

	public void Execute(InteractionContext interactionContext)
	{
		var unit = interactionContext.FirstSelected as BaseUnitController;
		unit.MoveTo(interactionContext.ClickedPosition.Value); // ground scripti pozisyon döndürmeli
	}
}
