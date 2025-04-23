using System;
using UnityEngine;

[Serializable]
public class BaseUnitDynamicData
{
	[SerializeField] private float _health;
	[SerializeField] private float _damage;
	[SerializeField] private float _speed;
	[SerializeField] private float _attackDistance;
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
	public float Health
	{
		get
		{
			return _health;
		}
		set
		{
			_health = value;
			DataChange();
		}
	}
	public float AttackDistance
	{
		get { return _attackDistance; }
		set
		{
			_attackDistance = value;
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

	public float Speed
	{
		get { return _speed; }
		set
		{
			_speed = value;
			DataChange();
		}
	}

	public Action OnDataChange;
	public Action OnDestroy;

	public BaseUnitDynamicData(BaseUnitDynamicData baseUnitDynamicData)
	{
		_health = baseUnitDynamicData.Health;
		_damage = baseUnitDynamicData.Damage;
		_attackDistance = baseUnitDynamicData.AttackDistance;
		_attackCooldown = baseUnitDynamicData.AttackCooldown;
		_speed = baseUnitDynamicData.Speed;
		OnDataChange = delegate { };
		OnDestroy = delegate { };

	}
	protected void DataChange()
	{
		OnDataChange?.Invoke();
	}

}