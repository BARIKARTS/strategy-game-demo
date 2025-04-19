using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantUIController : BasePanelController<PowerPlantData, BaseBuildingDynamicData>
{
	[Header("REFERENCES")]
	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private TextMeshProUGUI _description;
	[SerializeField] private TextMeshProUGUI _healtText;
	[SerializeField] private Image _previewIcon;




	public override void Active(PowerPlantData data, BaseBuildingDynamicData dynamicData)
	{

		_name.text = data.Name;
		_description.text = data.Description;
		_previewIcon.sprite = data.Icon;
		_dynamicData = dynamicData;
		Subscribe();
		Debug.Log(_dynamicData);
		gameObject.SetActive(true);
	}
	public override void Deactive()
	{
		gameObject.SetActive(false);
		Unsubscribe();
	}


	public override void Subscribe()
	{
		HealtChange(_dynamicData.Healt);
		_dynamicData.OnHealtChange += HealtChange;

	}

	public override void Unsubscribe()
	{
		_dynamicData.OnHealtChange -= HealtChange;
	}

	private void HealtChange(float healt)
	{
		_healtText.text = $"{healt}";
	}
}
