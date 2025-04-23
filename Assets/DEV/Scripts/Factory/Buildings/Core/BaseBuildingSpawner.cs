using UnityEngine;
public abstract class BaseBuildingSpawner
{
	protected BaseBuildingData m_data { get; private set; }
	public BuildingType BuildingType
	{
		get
		{
			if (m_data != null)
			{
				return m_data.BuildingType;
			}
			return BuildingType.None;
		}
	}
	public virtual void Initialize(BaseBuildingData buildingData)
	{
		m_data = buildingData;
	}
	public abstract GameObject Spawn(Vector2 position, byte team);
}
