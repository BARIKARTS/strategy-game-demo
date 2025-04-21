using System;
using UnityEngine;

[Serializable]
public class BarracksDynamicData : BaseBuildingDynamicData
{
	[field: SerializeField] public Vector2 UnitSpawnPosititon;

	public BarracksDynamicData(BaseBuildingDynamicData defaultData) : base(defaultData)
	{
		BarracksDynamicData defaultBarracksDynamicData = defaultData as BarracksDynamicData;
		UnitSpawnPosititon = defaultBarracksDynamicData.UnitSpawnPosititon;
	}

	



}
