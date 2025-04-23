using Pathfinding;

/// <summary>
/// Controls power plant building behavior
/// </summary>
public class PowerPlantController : BuildingController<PowerPlantDynamicData>
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
