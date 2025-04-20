using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Models
{
	/// <summary>
	/// Grid container managing nodes, world-grid conversions and gizmo drawing.
	/// </summary>
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
			InitializeNodes();
		}

		public void UpdateGrid(int width, int height, Vector2 origin, float cellSize)
		{
			Width = width;
			Height = height;
			Origin = origin;
			CellSize = cellSize;
			nodes = new Node[width, height];
			InitializeNodes();
		}

		private void InitializeNodes()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					nodes[x, y] = new Node(x, y, true);
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
			var node = GetNode(x, y);
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
			return GetWorldPosition(x, y) + new Vector2(CellSize * 0.5f, CellSize * 0.5f);
		}

#if UNITY_EDITOR
		public void DrawGizmos()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var node = nodes[x, y];
					var pos = GetCellCenter(x, y);
					Gizmos.color = node.IsWalkable ? Color.green : Color.red;
					Gizmos.DrawWireCube(pos, new Vector3(CellSize, CellSize, 0f));
				}
			}
		}
#endif

		public int GetManhattanDistance(Node a, Vector2Int b)
		{
			return Mathf.Abs(a.X - b.x) + Mathf.Abs(a.Y - b.y);
		}

		public int GetEuclideanDistance(Node a, Node b)
		{
			int dx = a.X - b.X;
			int dy = a.Y - b.Y;
			return (int)(Mathf.Sqrt(dx * dx + dy * dy) * 10);
		}

		/// <summary>
		/// Retrieves adjacent neighbors (including diagonals).
		/// </summary>
		public void GetNeighbors(Node node, List<Node> neighbors)
		{
			neighbors.Clear();
			for (int dx = -1; dx <= 1; dx++)
			{
				for (int dy = -1; dy <= 1; dy++)
				{
					if (dx == 0 && dy == 0) continue;
					var n = GetNode(node.X + dx, node.Y + dy);
					if (n != null)
						neighbors.Add(n);
				}
			}
		}
	}
}