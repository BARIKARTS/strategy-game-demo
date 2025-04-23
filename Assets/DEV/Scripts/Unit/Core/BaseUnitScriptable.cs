using System;
using UnityEngine;

[Serializable]
public abstract class BaseUnitSO : ScriptableObject
{
	public abstract UnitType UnitType { get; }
	public abstract BaseUnitData GetData();

}
