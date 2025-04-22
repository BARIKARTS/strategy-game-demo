
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BarracksUIController : BaseBuildUIController<BarracksData, BarracksDynamicData>
{
	[Space(2), Header("PRODUCTION REFERENCE")]
	[SerializeField] private ProductionUIElemet _productionPrefab;
	[SerializeField] private RectTransform _productionParent;

	ObjectPool<ProductionUIElemet> _productionPool;
	private List<ProductionUIElemet> _activeProduction = new List<ProductionUIElemet>();
	private CommonData _commonData => CommonData.Instance;
	private FactoryManager _factoryManager => FactoryManager.Instance;

	private void Start()
	{
		CreatePool();
		gameObject.SetActive(false);
	}
	public override void Active(BarracksData data, BarracksDynamicData dynamicData)
	{
		base.Active(data, dynamicData);
		CreateProductionElements();
	}
	public override void Deactive()
	{
		base.Deactive();
		HideProductionElements();
	}

	private void CreatePool()
	{
		if (_productionPool != null) return;
		_productionPool = new ObjectPool<ProductionUIElemet>(_productionPrefab, 10, _productionParent);
	}


	private void CreateProductionElements()
	{
		if (_commonData != null && _commonData.TryGetBuildingData(BuildingType.Barracks, out BarracksData barracksData))
		{
			ProductionUIElemet currentElement;
			if (barracksData != null && barracksData.Units != null)
			{
				BaseUnitData currentData;
				for (byte i1 = 0; i1 < barracksData.Units.Length; ++i1)
				{
					currentData = barracksData.Units[i1].GetData();
					if (currentData == null) continue;
					//CreatePructionElement();
					currentElement = _productionPool.GetObject();
					currentElement.Initialize(currentData.Name, currentData.Icon, () =>
					{
						_factoryManager?.UnitSpawn(currentData.UnitType, m_dynamicData.UnitSpawnPosititon);
					});
					_activeProduction.Add(currentElement);
				}

			}
		}
	}
	private void HideProductionElements()
	{
		for (byte i1 = 0; i1 < _activeProduction.Count; ++i1)
		{
			_productionPool.ReturnObject(_activeProduction[i1]);
		}
		_activeProduction.Clear();
	}

}

