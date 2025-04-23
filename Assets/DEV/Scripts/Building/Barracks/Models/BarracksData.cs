
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BarracksData : BaseBuildingData
{
	[field: SerializeField] public BarracksDynamicData DynamicData { get; private set; }
	[field: SerializeField] public BaseUnitSO[] Units;

	public BaseUnitData[] GetAllUnitsData() => Units.Select(u=>u.GetData()).ToArray();

}
