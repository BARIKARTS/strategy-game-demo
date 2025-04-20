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
			if (_healt <= 0) _onDestroy?.Invoke();
			_onHealtChange?.Invoke(_healt);
		}
	}

	public event Action<float> _onHealtChange = delegate { };
	public event Action _onDestroy = delegate { };

}