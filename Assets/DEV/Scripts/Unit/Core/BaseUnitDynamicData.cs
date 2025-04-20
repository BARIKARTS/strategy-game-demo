using System;
using UnityEngine;

[Serializable]
public class BaseUnitDynamicData
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
			OnHealtChange?.Invoke(_healt);
		}
	}

	public Action<float> OnHealtChange = delegate { };
	public Action OnDestroy = delegate { };

}