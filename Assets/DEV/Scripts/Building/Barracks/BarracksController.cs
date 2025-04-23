using Pathfinding;
using UnityEngine;

public class BarracksController : BuildingController<BarracksDynamicData>
{
	[SerializeField] private GameObject _flagObject;
	private AstarPathfindingManager _pathfindingManager => AstarPathfindingManager.Instance;

	public override void Initialize(BuildingType buildingType, byte team, BaseBuildingDynamicData dynamicData)
	{
		base.Initialize(buildingType, team, dynamicData);
		_flagObject?.SetActive(false);
		m_dynamicData.UnitSpawnPosititon = _flagObject.transform.position;
	}

	public override void OnSelected()
	{
		base.OnSelected();
		_flagObject?.SetActive(true);
		UIManager.Instance.OpenBarracks(m_buildingType, m_dynamicData);
	}

	public override void OnDeselected()
	{
		base.OnDeselected();
		if (_flagObject != null) _flagObject.SetActive(false);
		UIManager.Instance.HideController();

	}

	public override void Destroy()
	{
		_pathfindingManager.DestroyStructure(gameObject);
		Destroy(gameObject);
	}

	public void SetSpawnPosition(Vector2 newPosition)
	{
		m_dynamicData.UnitSpawnPosititon = newPosition;
		if (_flagObject != null) _flagObject.transform.position = newPosition;
	}


}
