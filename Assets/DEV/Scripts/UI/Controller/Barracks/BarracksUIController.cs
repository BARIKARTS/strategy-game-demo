
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BarracksUIController : BasePanelController<BarracksData, BarracksDynamicData>
{
	[Header("REFERENCES")]
	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private TextMeshProUGUI _description;
	[SerializeField] private TextMeshProUGUI _healtText;
	[SerializeField] private Image _previewIcon;


	[Space(2), Header("PRODUCTION REFERENCE")]
	[SerializeField] private ProductionUIElemet _productionPrefab;
	[SerializeField] private RectTransform _productionParent;

	ObjectPool<ProductionUIElemet> _productionPool;
	private List<ProductionUIElemet> _activeProduction = new List<ProductionUIElemet>();
	private CommonData _commonData => CommonData.Instance;
	private FactoryManager _factoryManager => FactoryManager.Instance;
	public override void Active(BarracksData data, BarracksDynamicData dynamicData)
	{
		CreatePool();
		_name.text = data.Name;
		_description.text = data.Description;
		_previewIcon.sprite = data.Icon;
		Debug.Log($"dynamic data: {dynamicData}");
		_dynamicData = dynamicData;
		Subscribe();
		Debug.Log(data);
		gameObject.SetActive(true);
		CreateProductionElements();
	}
	public override void Deactive()
	{
		gameObject.SetActive(false);
		Unsubscribe();
		HideProductionElements();
	}

	private void CreatePool()
	{
		if (_productionPool != null) return;
		_productionPool = new ObjectPool<ProductionUIElemet>(_productionPrefab, 10, _productionParent);
	}

	public override void Subscribe()
	{
		HealtChange(_dynamicData.Healt);
		_dynamicData._onHealtChange += HealtChange;

	}

	public override void Unsubscribe()
	{
		_dynamicData._onHealtChange -= HealtChange;
	}

	private void HealtChange(float healt)
	{
		_healtText.text = $"{healt}";
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
						Debug.Log("click kral");
						_factoryManager?.UnitSpawn(currentData.UnitType, _dynamicData.UnitSpawnPosititon);
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

