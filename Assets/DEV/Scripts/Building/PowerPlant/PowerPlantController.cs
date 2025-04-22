

using Pathfinding;

public class PowerPlantController : BuildingBaseController<PowerPlantDynamicData>
{

	private AstarPathfindingManager _pathfindingManager => AstarPathfindingManager.Instance;
	public override void OnSelected()
	{
		base.OnSelected();
		UIManager.Instance.OpenPowerPlantPanel(BuildingType.PowerPlant, m_dynamicData);
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
