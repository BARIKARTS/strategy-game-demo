using System;
using UnityEngine;

/// <summary>
/// A serializable data class for power plant buildings, inheriting from BaseBuildingData.
/// Stores static data specific to power plant buildings.
/// </summary>
[Serializable]
public class PowerPlantData: BaseBuildingData
{
	[field:SerializeField] public BaseBuildingDynamicData DynamicData { get; private set; }
}