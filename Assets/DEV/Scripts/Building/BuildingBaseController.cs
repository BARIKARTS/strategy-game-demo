using System;
using UnityEngine;


public abstract class BuildingBaseController<T1, T2> : SelectableBase, IDamageable where T1 : BaseBuildingData where T2 : BaseBuildingDynamicData
{
	protected BuildingType m_buildingType;
	protected T2 m_dynamicData;

	public abstract float Health { get; }

	public virtual void Initialize(T1 data)
	{
		if (data == null) Debug.LogError($"data is null");
		m_dynamicData = data.DynamicData as T2;
		m_buildingType = data.BuildingType;
	}
	public abstract void Destroy();
	public abstract void TakeDamage(float damage);

	public Vector2 GetPosition() => transform.position;
}