using UnityEngine;

public abstract class BaseUnitSpawner<T> where T : BaseUnitData
{
	protected T m_data;
	protected BaseUnitSpawner(T data)
	{
		m_data = data;
	}
	public virtual T SpawnUnit(Vector2 position) 
	{
		if (m_data != null && m_data.Prefab != null)
		{
			GameObject spawnObj = GameObject.Instantiate(m_data.Prefab);
			spawnObj.transform.position = position;
			return spawnObj.GetComponent<T>();
		}
		return null;

	}
}