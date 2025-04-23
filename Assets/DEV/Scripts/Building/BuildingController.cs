using DG.Tweening;
using System;
using UnityEngine;


public abstract class BuildingController<T2> : BaseBuildingController where T2 : BaseBuildingDynamicData
{
	protected T2 m_dynamicData;

	public override float Health => m_dynamicData.Health;


	public override void Initialize(BuildingType buildingType, byte team, BaseBuildingDynamicData dynamicData)
	{
		Team = team;
		m_buildingType = buildingType;
		BaseBuildingDynamicData createData = (BaseBuildingDynamicData)Activator.CreateInstance(typeof(T2), args: dynamicData);
		m_dynamicData = (T2)createData;
	}
	public override void TakeDamage(float damage)
	{
		m_dynamicData.Health -= damage;
		PlayDamageAnimation();
		if (m_dynamicData.Health <= 0)
		{
			Destroy();
		}
	}
	private void PlayDamageAnimation()
	{
		Sequence damageSequence = DOTween.Sequence();
		damageSequence.Append(transform.DOShakePosition(0.3f, 0.1f))
					  .Join(selectionRenderer?.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo));
	}
}