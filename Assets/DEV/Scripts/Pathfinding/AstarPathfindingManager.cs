using UnityEngine;
using Pathfinding.Models;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace Pathfinding
{
	public class AstarPathfindingManager : SingletonMonoBehaviour<AstarPathfindingManager>
	{
		[Header("Grid Settings")]
		[SerializeField] private int gridWidth = 10;
		[SerializeField] private int gridHeight = 10;
		[SerializeField] private Vector2 gridPosition = Vector2.zero;
		[SerializeField] private float cellSize = 1f;

		public CustomGrid grid;
		private AstarPathfinding _astarPathfinding;

		private void Start()
		{
			_astarPathfinding = new AstarPathfinding(grid);
		}


		public Queue<Vector2> FindPath(Vector2 start, Vector2 target)
		{
			if (_astarPathfinding == null) return null;
			return _astarPathfinding.FindPath(start, target);
		}


#if UNITY_EDITOR
		private void OnValidate()
		{
			// Inspector'da deðiþiklik yapýldýðýnda grid'i güncelle
			if (grid != null)
			{
				grid.UpdateGrid(gridWidth, gridHeight, gridPosition, cellSize);
			}
			else
			{
				InitializeGrid();
			}
		}

		private void InitializeGrid()
		{
			grid = new CustomGrid(gridWidth, gridHeight, gridPosition, cellSize);
		}

		private void OnDrawGizmos()
		{
			if (grid != null)
			{
				grid.DrawGizmos();
			}
		}
#endif

		public void DestroyStructure(GameObject structure)
		{
			UpdateGrids(structure, true);
		}
		public void PlaceStructure(GameObject structure)
		{
			UpdateGrids(structure, false);
		}

		private void UpdateGrids(GameObject structure, bool value)
		{
			if (structure.TryGetComponent(out SpriteRenderer spriteRenderer))
			{
				if (spriteRenderer == null)
				{
					Debug.LogError("Structure has no Collider2D component.");
					return;
				}

				Bounds bounds = spriteRenderer.bounds;
				Vector2 minWorld = bounds.min;
				Vector2 maxWorld = bounds.max;

				int minGridX = WorldToGridX(minWorld.x);
				int minGridY = WorldToGridY(minWorld.y);
				int maxGridX = WorldToGridX(maxWorld.x);
				int maxGridY = WorldToGridY(maxWorld.y);

				for (int x = minGridX; x <= maxGridX; x++)
				{
					for (int y = minGridY; y <= maxGridY; y++)
					{
						grid.UpdateWalkability(x, y, value);
					}
				}
			}
		}



		private int WorldToGridX(float worldX)
		{
			return Mathf.FloorToInt((worldX - gridPosition.x) / cellSize);
		}

		private int WorldToGridY(float worldY)
		{
			return Mathf.FloorToInt((worldY - gridPosition.y) / cellSize);
		}
	}
}