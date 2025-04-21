using System;
using UnityEngine;


public abstract class BuildingBaseController<T2> : SelectableBase, IDamageable where T2 : BaseBuildingDynamicData
{
	protected BuildingType m_buildingType;
	protected T2 m_dynamicData;

	public abstract float Health { get; }

	public virtual void Initialize(BuildingType buildingType, BaseBuildingDynamicData dynamicData)
	{
		m_buildingType = buildingType;
		BaseBuildingDynamicData createData = (BaseBuildingDynamicData)Activator.CreateInstance(typeof(T2), args: dynamicData);
		m_dynamicData = (T2)createData;
	}
	public abstract void Destroy();
	public abstract void TakeDamage(float damage);

	public Vector2 GetPosition() => transform.position;
}