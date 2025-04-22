using BuildingSystem;
using UnityEngine;

public class BuildingProductionUIController : MonoBehaviour
{
	[SerializeField] private ProductionUIElemet _productionPref;
	[SerializeField] private RectTransform _productionGroup;
	private CommonData _commonData => CommonData.Instance;
	private BuildingPlacer _buildingPlacer => BuildingPlacer.Instance;
	private void Start()
	{
		CreateProduction();
	}
	public void CreateProduction()
	{
		BaseBuildingData[] baseBuildingsData = _commonData.GetAllBuildingsData();

		for (byte i1 = 0; i1 < baseBuildingsData.Length; ++i1)
		{
			CreateProductionElement(baseBuildingsData[i1]);
		}
	}


	private void CreateProductionElement(BaseBuildingData data)
	{
		if (data == null)
		{
			Debug.LogError($"building data is null");
			return;
		}
		ProductionUIElemet element = Instantiate(_productionPref);
		element.transform.SetParent(_productionGroup);
		BuildingType buildingType = data.BuildingType;
		element.Initialize(data.Name, data.Icon, ()=>_buildingPlacer.Active(buildingType));
	}
}
