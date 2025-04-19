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
			OnHealtChange(_healt);
		}
	}

	public Action<float> OnHealtChange = delegate { };
	public Action OnDestroy = delegate { };

}