using UnityEngine;
using Pathfinding.Models;
using Unity.VisualScripting;

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

		public void PlaceStructure(GameObject structure)
		{
			Collider2D collider = structure.GetComponent<Collider2D>();
			if (collider == null)
			{
				Debug.LogError("Structure has no Collider2D component.");
				return;
			}

			Bounds bounds = collider.bounds;
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
					grid.UpdateWalkability(x, y, false);
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