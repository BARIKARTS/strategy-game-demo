using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksController : BuildingBaseController<BarracksDynamicData>
{
	public override float Health => m_dynamicData.Healt;

	[SerializeField] private Vector2 _testSpawnPos;

	[ContextMenu("changeSpawnPos")]
	private void ChangeSpawnPos()
	{
		m_dynamicData.UnitSpawnPosititon = _testSpawnPos;
	}

	public override void OnSelected()
	{
		base.OnSelected();
		UIManager.Instance.OpenBarracks(BuildingType.Barracks, m_dynamicData);
	}

	public override void OnDeselected()
	{
		base.OnDeselected();
	}

	public override void InteractWith(ISelectable selected)
	{
		throw new System.NotImplementedException();
	}
	public override void Destroy()
	{
		throw new System.NotImplementedException();
	}

	public override void TakeDamage(float damage)
	{
		m_dynamicData.Healt -= damage;
		if (m_dynamicData.Healt <= 0)
		{
			Destroy();
		}
	}

}
