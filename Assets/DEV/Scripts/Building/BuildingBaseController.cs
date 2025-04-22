using System;
using UnityEngine;


public abstract class BuildingBaseController<T2> : SelectableBase, IDamageable where T2 : BaseBuildingDynamicData
{
	public byte Team { get; private set; }
	protected BuildingType m_buildingType;
	protected T2 m_dynamicData;

	public  float Health => m_dynamicData.Healt;

	public Vector2 Position => transform.position;

	public virtual void Initialize(BuildingType buildingType, byte team, BaseBuildingDynamicData dynamicData)
	{
		Team = team;
		m_buildingType = buildingType;
		BaseBuildingDynamicData createData = (BaseBuildingDynamicData)Activator.CreateInstance(typeof(T2), args: dynamicData);
		m_dynamicData = (T2)createData;
	}
	public abstract void Destroy();
	public  void TakeDamage(float damage)
	{
		m_dynamicData.Healt -= damage;
		if (m_dynamicData.Healt <= 0)
		{
			Destroy();
		}
	}

}