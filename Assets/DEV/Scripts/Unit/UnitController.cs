using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitPathfinding))]
public class UnitController : BaseUnitController<BaseUnitData, BaseUnitDynamicData>
{

	[SerializeField] private UnitController _testMove;

	protected override void Start()
	{
		base.Start();
		Initialize(new BaseUnitData());
	}

	private void Update()
	{
		ProcessCommand();
	}

	[ContextMenu("TestMove")]
	public void TestMove()
	{
		IDamageable damageable = _testMove;
		UnitController obj = damageable as UnitController;
		Debug.Log(obj);
		if (obj != null)
		{
			Attack(damageable);
		}
	}

	public void MoveTo(Vector2 position)
	{
		Debug.Log("MoveTo");
		AddCommand(new MoveCommand(this, position));
	}

	public void Attack(IDamageable target)
	{

		MoveTo(target.GetPosition());
		AddCommand(new AttackCommand(this, target));

	}

	public override void TakeDamage(float damage)
	{
		m_dynamicData.Healt -= damage;
		Debug.LogError($"healt:{m_dynamicData.Healt}");
		if (m_dynamicData.Healt <= 0)
		{
			Destroy(gameObject);
		}
	}


	public override void InteractWith(ISelectable selected)
	{
	}
}
