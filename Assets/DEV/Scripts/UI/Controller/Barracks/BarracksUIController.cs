
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BarracksUIController : BaseBuildUIController<BarracksData, BarracksDynamicData>
{
	[Space(2), Header("PRODUCTION REFERENCE")]
	[SerializeField] private ProductionUIElemet _productionPrefab;
	[SerializeField] private RectTransform _productionParent;
	[SerializeField] private HealthDisplay _healthDisplay;

	private float _maxHealth;
	private ObjectPool<ProductionUIElemet> _productionPool;
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
		_maxHealth = data.DynamicData.Health;
		base.Active(data, dynamicData);
		CreateProductionElements(data.BuildingType);
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


	private void CreateProductionElements(BuildingType buildingType)
	{
		if (_commonData != null && _commonData.TryGetBuildingData(buildingType, out BarracksData barracksData))
		{
			ProductionUIElemet currentElement;
			if (barracksData != null && barracksData.Units != null)
			{
				BaseUnitData currentData;
				for (byte i1 = 0; i1 < barracksData.Units.Length; ++i1)
				{
					currentData = barracksData.Units[i1].GetData();
					if (currentData == null) continue;
					currentElement = _productionPool.GetObject();
					currentElement.transform.SetSiblingIndex(i1);
					UnitType unitType = currentData.UnitType;
					currentElement.Initialize(currentData.Name, currentData.Icon, () =>
					{
						_ = _factoryManager?.UnitSpawn(unitType, m_dynamicData.UnitSpawnPosititon);
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
			_activeProduction[i1].Hide();
			_productionPool.ReturnObject(_activeProduction[i1]);
		}
		_activeProduction.Clear();
	}

	protected override void DynamicDataUpdate()
	{
		if (m_dynamicData != null)
		{
			_healthDisplay.UpdateHealth(_maxHealth, m_dynamicData.Health);
		}
	}
}

