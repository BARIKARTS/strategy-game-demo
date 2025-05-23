using System;
using UnityEngine;

[Serializable]
public class BaseBuildingData
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField, TextArea] public string Description { get; private set; }
	[field: SerializeField] public Sprite Icon { get; private set; }
	[field: SerializeField] public Sprite PreviewSprite { get; private set; }
	[field: SerializeField] public GameObject Prefab { get; private set; }
	[field: SerializeField] public BuildingType BuildingType { get; private set; }
	

}


