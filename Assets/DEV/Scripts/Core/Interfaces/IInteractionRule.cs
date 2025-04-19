public interface IInteractionRule
{
	bool CanHandle(ISelectable selected, IInteractable clicked);
	void Execute(ISelectable selected, IInteractable clicked);
}
