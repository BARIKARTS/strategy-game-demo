using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A generic object pool for managing reusable instances of a specified component type.
/// Reduces instantiation overhead by reusing inactive objects.
/// </summary>
/// <typeparam name="T">The type of component to pool, derived from Component.</typeparam>
public class ObjectPool<T> where T : Component
{
	private Queue<T> pool = new Queue<T>();
	private T prefab;
	private Transform parent;
	public MonoBehaviour CoroutineRunner;

	/// <summary>
	/// Initializes a new object pool with a specified prefab, initial size, and optional parent transform and scale.
	/// Creates and stores the initial set of inactive objects.
	/// </summary>
	/// <param name="prefab">The prefab to instantiate for the pool.</param>
	/// <param name="initialSize">The initial number of objects to create.</param>
	/// <param name="parent">The parent transform for instantiated objects, if any.</param>
	/// <param name="scale">The scale to apply to instantiated objects, defaults to prefab's scale.</param>
	public ObjectPool(T prefab, int initialSize, Transform parent = null, Vector3? scale = null)
	{
		scale = scale ?? prefab.transform.localScale;
		this.prefab = prefab;
		this.parent = parent;

		for (int i1 = 0; i1 < initialSize; ++i1)
		{
			T obj = GameObject.Instantiate(prefab, this.parent);
			obj.gameObject.SetActive(false);
			obj.transform.localScale = (Vector3)scale;
			pool.Enqueue(obj);
		}
	}


	/// <summary>
	/// Retrieves an object from the pool or instantiates a new one if the pool is empty.
	/// Activates the retrieved object and optionally sets its name.
	/// </summary>
	/// <param name="name">The name to assign to the object, if specified.</param>
	/// <returns>The retrieved or instantiated object.</returns>
	public T GetObject(string name = null)
	{
		T obj;
		if (pool.Count > 0)
		{
			obj = pool.Dequeue();
			obj.gameObject.SetActive(true);
		}
		else
		{
			obj = GameObject.Instantiate(prefab, parent);
		}
		name = name ?? obj.name;
		return obj;
	}


	/// <summary>
	/// Returns an object to the pool after a specified delay using a coroutine.
	/// </summary>
	/// <param name="obj">The object to return to the pool.</param>
	/// <param name="time">The delay in seconds before returning the object.</param>
	public void ReturnObject(T obj, float time)
	{
		CoroutineRunner.StartCoroutine(ReturnObjectAfterTime(obj, time));
	}
	/// <summary>
	/// Returns an object to the pool by deactivating it and enqueueing it for reuse.
	/// </summary>
	/// <param name="obj">The object to return to the pool.</param>
	public void ReturnObject(T obj)
	{
		obj.gameObject.SetActive(false);
		pool.Enqueue(obj);
	}
	/// <summary>
	/// Coroutine that delays the return of an object to the pool by the specified time.
	/// </summary>
	/// <param name="obj">The object to return to the pool.</param>
	/// <param name="time">The delay in seconds before returning the object.</param>
	/// <returns>An IEnumerator for coroutine execution.</returns>
	private IEnumerator ReturnObjectAfterTime(T obj, float time)
	{
		yield return new WaitForSeconds(time);
		ReturnObject(obj);
	}
}