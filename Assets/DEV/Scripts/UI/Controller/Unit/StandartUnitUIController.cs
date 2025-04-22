/// <summary>
/// A UI controller for standard units, managing the display of unit data and dynamic data.
/// Inherits from BaseUnitUIController to provide specific functionality for standard units.
/// </summary>
public class StandartUnitUIController : BaseUnitUIController<BaseUnitData, BaseUnitDynamicData>
{
	private void Start()
	{
		gameObject.SetActive(false);
	}
	

}
