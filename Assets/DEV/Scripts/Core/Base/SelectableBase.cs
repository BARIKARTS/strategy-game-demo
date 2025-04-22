using UnityEngine;

/// <summary>
/// An abstract base class for selectable and interactable game objects.
/// Provides functionality for visual selection feedback using a sprite renderer.
/// Implements ISelectable and IInteractable interfaces.
/// </summary>
public abstract class SelectableBase : MonoBehaviour, ISelectable, IInteractable
{
	[SerializeField] protected SpriteRenderer selectionRenderer;
	[SerializeField] protected Color defaultColor = Color.white;
	[SerializeField] protected Color selectedColor = Color.green;

	public virtual void OnSelected()
	{
		if (selectionRenderer != null)
			selectionRenderer.color = selectedColor;
	}

	public virtual void OnDeselected()
	{
		if (selectionRenderer != null)
		selectionRenderer.color = defaultColor;
	}

}