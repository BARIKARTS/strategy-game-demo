using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksController : BuildingBaseController<BarracksDynamicData>
{


	[SerializeField] private Vector2 _testSpawnPos;
	private AstarPathfindingManager _pathfindingManager => AstarPathfindingManager.Instance;
	public override void OnSelected()
	{
		base.OnSelected();
		UIManager.Instance.OpenBarracks(BuildingType.Barracks, m_dynamicData);
	}

	public override void OnDeselected()
	{
		base.OnDeselected();
		UIManager.Instance.HideController();

	}

	public override void Destroy()
	{
		_pathfindingManager.DestroyStructure(gameObject);
		Destroy(gameObject);
	}

	

}
