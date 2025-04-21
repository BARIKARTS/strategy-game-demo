using System;
using UnityEngine;

[Serializable]
public class BaseUnitDynamicData
{
	[SerializeField] private float _healt;
	[SerializeField] private float _damage;

	public float Damage
	{
		get
		{
			return _damage;
		}
		set
		{
			_damage = value;
			OnDamageCahnge?.Invoke(_damage);
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
			OnHealtChange?.Invoke(_healt);
		}
	}

	public Action<float> OnHealtChange;
	public Action<float> OnDamageCahnge;
	public Action OnDestroy;

	public BaseUnitDynamicData(BaseUnitDynamicData baseUnitDynamicData)
	{
		_healt = baseUnitDynamicData.Healt;
		_damage = baseUnitDynamicData.Damage;
		OnHealtChange = delegate { };
		OnDamageCahnge = delegate { };
		OnDestroy = delegate { };

	}

}