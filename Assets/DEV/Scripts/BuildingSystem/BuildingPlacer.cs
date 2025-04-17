using GameInput;
using UnityEngine;
using BuildingSystem.Models;
using UnityEngine.Tilemaps;
using System.Collections;
using Pathfinding;

namespace BuildingSystem
{
	public class BuildingPlacer : MonoBehaviour
	{
		[field: SerializeField] public BaseBuildableItem ActiveBuildable { get; private set; }
		[SerializeField] private Tilemap _baseTileMap;
		[SerializeField] private PreviewLayer _previewLayer;

		private void Start()
		{
			_previewLayer.ActivePreview(ActiveBuildable.PreviewSprite);
		}


		private void Update()
		{
			Vector2 mouseWorldPos = MouseUser.MouseInWorldPosition;
			_previewLayer.UpdatePreview(mouseWorldPos);
			//Vector2 newPreviewPos = GetNewPreviewPosition(mouseWorldPos);
			//bool isEmpty = _constructionLayer.IsEmpty(newPreviewPos);
			if (MouseUser.IsMouseButtonPressed(MouseButton.Left) && ActiveBuildable != null && _previewLayer.CanBuild)
			{
				GameObject createObj = Instantiate(ActiveBuildable.Prefab, mouseWorldPos, Quaternion.identity);
				AstarPathfindingManager.Instance.PlaceStructure(createObj);
			}
		}

		private Vector2 GetNewPreviewPosition(Vector2 mouseWorldPos)
		{
			Vector3Int coords = _baseTileMap.WorldToCell(mouseWorldPos);
			Vector2 newPos = _baseTileMap.CellToWorld(coords) + _baseTileMap.cellSize / 2;
			return newPos;
		}
	}

}
