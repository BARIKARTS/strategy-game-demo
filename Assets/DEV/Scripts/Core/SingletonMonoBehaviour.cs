using UnityEngine;


/// <summary>
/// A generic Singleton base class for MonoBehaviour-derived classes.
/// Ensures that only one instance of the component exists in the scene.
/// 
/// Usage:
/// Inherit from this class instead of MonoBehaviour to make a singleton manager.
/// Example: public class GameManager : SingletonMonoBehaviour<GameManager> {}
/// 
/// If another instance is created, it will be automatically destroyed.
/// </summary>

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }

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