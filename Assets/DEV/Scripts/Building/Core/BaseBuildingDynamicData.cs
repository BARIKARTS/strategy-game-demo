using System;
using UnityEngine;

[Serializable]
public class BaseBuildingDynamicData
{
	[SerializeField] private float _healt = 0;

	public float Healt
	{
		get
		{
			return _healt;
		}
		set
		{
			_healt = value;
			if (_healt <= 0) OnDestroy?.Invoke();
			DataChange();
		}
	}

	public event Action OnDataChange;
	public event Action OnDestroy;
	public BaseBuildingDynamicData(BaseBuildingDynamicData defaultData)
	{
		_healt = defaultData.Healt;
		OnDataChange = delegate { };
		OnDestroy = delegate { };
	}

	protected void DataChange()
	{
		OnDataChange?.Invoke();
	}

}