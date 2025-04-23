using UnityEngine;

public abstract class BaseBuildingController : SelectableBase, IDamageable
{
	public byte Team { get; protected set; }
	protected BuildingType m_buildingType;

	public abstract float Health { get; }

	public Vector2 Position => transform.position;

	public abstract void Initialize(BuildingType buildingType, byte team, BaseBuildingDynamicData dynamicData);
	public abstract void Destroy();
	public abstract void TakeDamage(float damage);
	public void ChangeTeam(byte team)=> Team = team;
}
