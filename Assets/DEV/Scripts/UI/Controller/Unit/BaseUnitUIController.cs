using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUnitUIController<T1, T2> : PanelController<T1, T2> where T1 : BaseUnitData where T2 : BaseUnitDynamicData
{
	[Header("MAIN REFERENCES")]
	[SerializeField] protected TextMeshProUGUI _name;
	[SerializeField] protected TextMeshProUGUI _description;
	[SerializeField] protected TextMeshProUGUI _healtText;
	[SerializeField] protected Image _previewIcon;


	public override void Active(T1 data, T2 dynamicData)
	{
		_name.text = data.Name;
		_description.text = data.Description;
		_previewIcon.sprite = data.Icon;
		m_dynamicData = (T2)dynamicData;
		Subscribe();
		Debug.Log(1);
		DynamicDataUpdate();
		Debug.Log(2);
		gameObject.SetActive(true);
	}



	public override void Deactive()
	{
		gameObject.SetActive(false);
		Unsubscribe();
	}


	protected override void Subscribe()
	{
		m_dynamicData.OnDataChange += DynamicDataUpdate;
		m_dynamicData.OnDestroy += DestroyBuilding;
	}

	protected override void Unsubscribe()
	{
		m_dynamicData.OnDataChange -= DynamicDataUpdate;
		m_dynamicData.OnDestroy -= DestroyBuilding;
	}
	protected virtual void DynamicDataUpdate()
	{
		if (m_dynamicData != null)
		{
			_healtText.text = $"{m_dynamicData.Healt}";
		}
	}
	protected virtual void DestroyBuilding()
	{
		Deactive();
	}
}
