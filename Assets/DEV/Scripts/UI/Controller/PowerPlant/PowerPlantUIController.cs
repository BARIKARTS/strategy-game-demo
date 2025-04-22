/// <summary>
/// A UI controller for power plant buildings, managing the display of static and dynamic data.
/// Inherits from BaseBuildUIController to provide specific functionality for power plants.
/// </summary>
public class PowerPlantUIController : BaseBuildUIController<PowerPlantData, BaseBuildingDynamicData>
{

	private void Start()
	{
		gameObject.SetActive(false);
	}
	public override void Deactive()
	{
		gameObject.SetActive(false);
		Unsubscribe();
	}

	protected override void DynamicDataUpdate()
	{
		if (m_dynamicData != null)
		{
			_healtText.text= $"{m_dynamicData.Healt}";
		}
	}

}
