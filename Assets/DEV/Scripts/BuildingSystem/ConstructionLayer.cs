using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
	public class ConstructionLayer : TileMapLayer
	{
		public void Build(Vector3 worldPos, BuildableItem item)
		{
			if (item != null)
			{
				Vector3Int coords = _tilemap.WorldToCell(worldPos);
				_tilemap.SetTile(coords, item.Tile);
			}
		}
	}

}
