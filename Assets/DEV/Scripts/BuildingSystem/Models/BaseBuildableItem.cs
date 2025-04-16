using UnityEngine;

namespace BuildingSystem.Models
{
	[CreateAssetMenu(menuName = "Building/New Buildable Item", fileName = "New Buildable Item")]
	public class BaseBuildableItem : ScriptableObject 
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField, TextArea] public string Description { get; private set; }
		[field: SerializeField] public Sprite PreviewSprite { get; private set; }
		[field: SerializeField] public GameObject Prefab { get; private set; }
		[field: SerializeField] public StructureType StructureType { get; private set; }
	}
}