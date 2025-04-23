using UnityEngine;
/// <summary>
/// Stores data for interaction events
/// </summary>
public class InteractionContext
{
	/// <summary>
	/// Primary selected object
	/// </summary>
	public ISelectable FirstSelected;
	/// <summary>
	/// Secondary interactable object
	/// </summary>
	public IInteractable SecondSelected;
	/// <summary>
	/// Position of interaction click
	/// </summary>
	public Vector2? ClickedPosition;

	/// <summary>
	/// Team ID for interaction
	/// </summary>
	public byte Team;
}
