using System;
using UnityEngine;

/// <summary>
/// Base ScriptableObject for building data
/// </summary>
[Serializable]
public abstract class BaseBuildingSO : ScriptableObject
{
	public abstract BuildingType BuildingType { get; }
	public abstract BaseBuildingData GetData();

}
