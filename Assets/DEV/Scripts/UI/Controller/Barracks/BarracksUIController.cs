
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BarracksUIController : BasePanelController<BarracksData, BaseBuildingDynamicData>
{
	[Header("REFERENCES")]
	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private TextMeshProUGUI _description;
	[SerializeField] private TextMeshProUGUI _healtText;
	[SerializeField] private Image _previewIcon;


	[Space(2), Header("PRODUCTION REFERENCE")]
	[SerializeField] private UnitProductionUIElemet _productionPrefab;
	[SerializeField] private RectTransform _productionParent;

	ObjectPool<UnitProductionUIElemet> _productionPool;
	private List<UnitProductionUIElemet> _activeProduction = new List<UnitProductionUIElemet>();

	public override void Active(BarracksData data, BaseBuildingDynamicData dynamicData)
	{
		CreatePool();
		_name.text = data.Name;
		_description.text = data.Description;
		_previewIcon.sprite = data.Icon;
		_dynamicData = dynamicData;
		//Subscribe();
		Debug.Log(data);
		gameObject.SetActive(true);
		CreateProductionElements(data.Units);
	}
	public override void Deactive()
	{
		gameObject.SetActive(false);
		//Unsubscribe();
		HideProductionElements();
	}

	private void CreatePool()
	{
		if (_productionPool != null) return;
		_productionPool = new ObjectPool<UnitProductionUIElemet>(_productionPrefab, 10, _productionParent);
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
	private void CreateProductionElements(GameObject[] units)
	{
		UnitProductionUIElemet currentElement;
		for (byte i1 = 0; i1 < units.Length; ++i1)
		{
			//CreatePructionElement();
			currentElement = _productionPool.GetObject();
			currentElement.Initialize("TEst", null, null);
			_activeProduction.Add(currentElement);
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

