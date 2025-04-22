using System;
using UnityEngine;

[Serializable]
public class BaseUnitDynamicData
{
	[SerializeField] private float _healt;
	[SerializeField] private float _damage;
	[SerializeField] private float _attackCooldown;
	public float Damage
	{
		get
		{
			return _damage;
		}
		set
		{
			_damage = value;
			DataChange();
		}
	}

	public float Healt
	{
		get
		{
			return _healt;
		}
		set
		{
			_healt = value;
			DataChange();
		}
	}

	public float AttackCooldown
	{
		get
		{
			return _attackCooldown;
		}
		set
		{
			_attackCooldown = value;
			DataChange();
		}
	}
	public Action OnDataChange;
	public Action OnDestroy;

	public BaseUnitDynamicData(BaseUnitDynamicData baseUnitDynamicData)
	{
		_healt = baseUnitDynamicData.Healt;
		_damage = baseUnitDynamicData.Damage;
		_attackCooldown = baseUnitDynamicData.AttackCooldown;
		OnDataChange = delegate { };
		OnDestroy = delegate { };

	}
	protected void DataChange()
	{
		OnDataChange?.Invoke();
	}

}