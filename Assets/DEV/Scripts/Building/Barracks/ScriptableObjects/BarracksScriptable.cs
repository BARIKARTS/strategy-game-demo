using UnityEngine;


[CreateAssetMenu(menuName = "Building/Barracks", fileName = "New Barracks Item")]
public class BarracksScriptable : BaseBuildingSO
{
	[field: SerializeField] public BarracksData BarracksData { get; private set; }

	public override BuildingType BuildingType => BarracksData.BuildingType;

	public override BaseBuildingData GetData() => BarracksData;
}
