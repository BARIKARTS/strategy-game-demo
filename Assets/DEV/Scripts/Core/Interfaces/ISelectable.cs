/// <summary>
/// Defines methods for selectable objects
/// </summary>
public interface ISelectable
{
	/// <summary>
	/// Called when object is selected
	/// </summary>
	void OnSelected();

	/// <summary>
	/// Called when object is deselected
	/// </summary>
	void OnDeselected();
}
