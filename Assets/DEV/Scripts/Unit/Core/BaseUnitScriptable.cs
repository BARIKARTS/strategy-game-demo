using System;
using UnityEngine;

[Serializable]
public abstract class BaseUnitScriptableObject : ScriptableObject
{
	public abstract UnitType UnitType { get; }
	public abstract BaseUnitData GetData();

}
