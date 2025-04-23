using System;
using UnityEngine;

[Serializable]
public class BaseBuildingDynamicData
{
	[SerializeField] private float _health = 0;

	public float Health
	{
		get
		{
			return _health;
		}
		set
		{
			_health = value;
			if (_health <= 0) OnDestroy?.Invoke();
			DataChange();
		}
	}

	public event Action OnDataChange;
	public event Action OnDestroy;
	public BaseBuildingDynamicData(BaseBuildingDynamicData defaultData)
	{
		_health = defaultData.Health;
		OnDataChange = delegate { };
		OnDestroy = delegate { };
	}

	protected void DataChange()
	{
		OnDataChange?.Invoke();
	}

}