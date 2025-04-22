using UnityEngine;

/// <summary>
/// A generic singleton base class for MonoBehaviour-derived classes.
/// Ensures only one instance of the specified type exists in the scene.
/// </summary>
/// <typeparam name="T">The type of the MonoBehaviour to enforce as a singleton.</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	/// <summary>
	/// Gets the single instance of the specified MonoBehaviour type.
	/// </summary>
	public static T Instance { get; private set; }

	/// <summary>
	/// Initializes the singleton instance during the Awake phase.
	/// Destroys duplicate instances to ensure only one instance exists.
	/// </summary>
	protected virtual void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
			return;
		}

		Instance = this as T;
	}
}