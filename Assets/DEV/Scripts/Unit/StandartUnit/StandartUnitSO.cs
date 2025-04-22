using UnityEngine;
/// <summary>
/// A ScriptableObject representing a standard unit, storing its static data.
/// Provides access to unit data and type for use in unit initialization.
/// </summary>
[CreateAssetMenu(menuName = "Units/New Standart Unit", fileName = "new StandartUnit")]
public class StandartUnitSO : BaseUnitScriptableObject
{
	[field: SerializeField] public StandartUnitData UnitData { get; private set; }
	public override UnitType UnitType => UnitData.UnitType;

	public override BaseUnitData GetData() => UnitData;


}
