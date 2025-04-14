using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Building/New Buildable Item", fileName = "New Buildable Item")]
public class BuildableItem : ScriptableObject
{
	[field: SerializeField]
	public string Name { get; private set; }

	[field: SerializeField]
	public TileBase Tile { get; private set; }
}
