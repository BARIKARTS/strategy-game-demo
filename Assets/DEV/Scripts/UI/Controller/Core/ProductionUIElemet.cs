using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductionUIElemet : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _nameTMP;
	[SerializeField] private Image _iconImage;
	[SerializeField] private Button _button;
	public void Initialize(string name, Sprite icon, UnityAction onClickAction)
	{
		_nameTMP.text = name;
		_iconImage.sprite = icon;
		if (_button != null && onClickAction != null) _button.onClick.AddListener(onClickAction);
		gameObject.SetActive(true);
	}
	public void Hide()
	{
		gameObject.SetActive(false);
		_button.onClick.RemoveAllListeners();
	}
}
