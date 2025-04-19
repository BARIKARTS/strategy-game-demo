using UnityEngine;

public abstract class SelectableBase : MonoBehaviour, ISelectable, IInteractable
{
	[SerializeField] protected Renderer selectionRenderer;
	[SerializeField] protected Color defaultColor = Color.white;
	[SerializeField] protected Color selectedColor = Color.green;

	public virtual void OnSelected()
	{
		if (selectionRenderer != null)
			selectionRenderer.material.color = selectedColor;
	}

	public virtual void OnDeselected()
	{
		if (selectionRenderer != null)
		selectionRenderer.material.color = defaultColor;
	}

	public abstract void InteractWith(ISelectable selected);
}