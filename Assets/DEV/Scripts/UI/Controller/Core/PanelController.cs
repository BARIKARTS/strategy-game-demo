/// <summary>
/// An abstract base class for controlling UI panels with generic static and dynamic data.
/// Provides a foundation for activating panels with specific data types.
/// </summary>
/// <typeparam name="T1">The type of static data for the panel.</typeparam>
/// <typeparam name="T2">The type of dynamic data for the panel.</typeparam>
public abstract class PanelController<T1, T2> : BasePanelController
{
	protected T2 m_dynamicData;

	/// <summary>
	/// Activates the UI panel with the specified static and dynamic data.
	/// Must be implemented by derived classes to define specific activation logic.
	/// </summary>
	/// <param name="baseData">The static data to initialize the panel.</param>
	/// <param name="dynamicData">The dynamic data to track in the panel.</param>
	public abstract void Active(T1 baseData, T2 dynamicData);

}
