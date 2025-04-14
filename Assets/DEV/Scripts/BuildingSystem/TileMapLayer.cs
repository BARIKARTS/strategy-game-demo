using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{

	[RequireComponent(typeof(Tilemap))]
	public class TileMapLayer : MonoBehaviour
	{
		protected Tilemap _tilemap { get; private set; }
		private void Awake()
		{
			_tilemap = GetComponent<Tilemap>();
		}
	}
}
