using UnityEngine;
public class StandartSoldierFactory : BaseUnitSpawner
{
	private BaseUnitData _data;
	public override UnitType UnitType => UnitType.Solider1;


	public override void Initialize(BaseUnitData unitData)
	{
		_data = unitData;
	}
	public override GameObject Spawn(Vector2 position)
	{
		if (_data != null && _data.Prefab != null)
		{
			GameObject createObj = GameObject.Instantiate(_data.Prefab);
			createObj.transform.position = position;
			createObj.GetComponent<StandartUnitController>().Initialize(_data.UnitType, _data.DynamicData);
			return createObj;
		}
		return null;
	}
}
