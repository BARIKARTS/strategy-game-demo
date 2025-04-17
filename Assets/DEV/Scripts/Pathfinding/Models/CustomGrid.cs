using UnityEngine;

namespace Pathfinding.Models
{
	public class CustomGrid
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public Vector2 Origin { get; private set; }
		public float CellSize { get; private set; }
		private Node[,] nodes;

		public static event System.Action<Node> OnWalkabilityChanged;

		public CustomGrid(int width, int height, Vector2 origin, float cellSize)
		{
			Width = width;
			Height = height;
			Origin = origin;
			CellSize = cellSize;
			nodes = new Node[width, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					nodes[x, y] = new Node(x, y,true);
				}
			}
		}

		public void UpdateGrid(int width, int height, Vector2 position, float cellSize)
		{
			Width = width;
			Height = height;
			Origin = position;
			CellSize = cellSize;
			nodes = new Node[width, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					nodes[x, y] = new Node(x, y,true);
				}
			}
		}

		public Node GetNode(int x, int y)
		{
			if (x >= 0 && x < Width && y >= 0 && y < Height)
				return nodes[x, y];
			return null;
		}

		public void UpdateWalkability(int x, int y, bool isWalkable)
		{
			Node node = GetNode(x, y);
			if (node != null)
			{
				node.IsWalkable = isWalkable;
				OnWalkabilityChanged?.Invoke(node);
			}
		}

		public Vector2 GetWorldPosition(int x, int y)
		{
			return new Vector2(x * CellSize, y * CellSize) + Origin;
		}

		public Vector2Int WorldToGrid(Vector2 worldPosition)
		{
			int x = Mathf.FloorToInt((worldPosition.x - Origin.x) / CellSize);
			int y = Mathf.FloorToInt((worldPosition.y - Origin.y) / CellSize);
			return new Vector2Int(x, y);
		}
		public Vector2 GetCellCenter(int x, int y)
		{
			return GetWorldPosition(x, y) + new Vector2(CellSize / 2, CellSize / 2);
		}
		public void DrawGizmos()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Node node = nodes[x, y];
					Vector2 position = GetWorldPosition(x, y) + new Vector2(CellSize / 2, CellSize / 2);
					Gizmos.color = node.IsWalkable ? Color.green : Color.red;
					Gizmos.DrawWireCube(position, new Vector3(CellSize, CellSize, 0));
				}
			}
		}
	}
}