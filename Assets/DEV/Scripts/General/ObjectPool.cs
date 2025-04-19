using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
	private Queue<T> pool = new Queue<T>();
	private T prefab;
	private Transform parent;
	public MonoBehaviour CoroutineRunner;

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

	public void ReturnObject(T obj, float time)
	{
		CoroutineRunner.StartCoroutine(ReturnObjectAfterTime(obj, time));
	}

	public void ReturnObject(T obj)
	{
		obj.gameObject.SetActive(false);
		pool.Enqueue(obj);
	}

	private IEnumerator ReturnObjectAfterTime(T obj, float time)
	{
		yield return new WaitForSeconds(time);
		ReturnObject(obj);
	}
}