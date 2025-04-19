using System;
using UnityEngine;

[Serializable]
public abstract class BaseBuildingScriptableObject : ScriptableObject
{
	public abstract BuildingType BuildingType { get; }
	public abstract BaseBuildingData GetData();

}
