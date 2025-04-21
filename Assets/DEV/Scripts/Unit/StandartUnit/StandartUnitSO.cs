using UnityEngine;
[CreateAssetMenu(menuName = "Units/New Standart Unit", fileName = "new StandartUnit")]
public class StandartUnitSO : BaseUnitScriptableObject
{
	[field: SerializeField] public StandartUnitData UnitData { get; private set; }
	public override UnitType UnitType => UnitData.UnitType;

	public override BaseUnitData GetData() => UnitData;


}
