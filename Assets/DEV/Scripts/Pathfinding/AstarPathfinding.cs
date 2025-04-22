using Pathfinding.Models;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Pathfinding
{
	public class AstarPathfinding
	{
		private readonly CustomGrid grid;
		private readonly List<Node> neighborBuffer = new List<Node>(8);

		public AstarPathfinding(CustomGrid grid) => this.grid = grid;

		/// <summary>
		/// Start veya end noktasý walkable deðilse,
		/// çevresinden en yakýn walkable node’u bulup pathfinding'i tekrar dener.
		/// </summary>
		public Queue<Vector2> FindPath(Vector2 startWorld, Vector2 endWorld)
		{
			// 1) Dünya koordinatlarýný grid koordinatýna çevir
			var startGrid = grid.WorldToGrid(startWorld);
			var endGrid = grid.WorldToGrid(endWorld);

			// 2) Grid sýnýrlarýna clamp et
			startGrid.x = Mathf.Clamp(startGrid.x, 0, grid.Width - 1);
			startGrid.y = Mathf.Clamp(startGrid.y, 0, grid.Height - 1);
			endGrid.x = Mathf.Clamp(endGrid.x, 0, grid.Width - 1);
			endGrid.y = Mathf.Clamp(endGrid.y, 0, grid.Height - 1);

			// 3) Node referanslarýný al
			var startNode = grid.GetNode(startGrid.x, startGrid.y);
			var endNode = grid.GetNode(endGrid.x, endGrid.y);

			if (startNode == null || endNode == null)
				return null;

			// 4) Eðer walkable deðilse, en yakýn walkable node'u bul
			if (!startNode.IsWalkable)
			{
				startNode = FindClosestWalkable(startNode);
				if (startNode == null) return null;
			}
			if (!endNode.IsWalkable)
			{
				endNode = FindClosestWalkable(endNode);
				if (endNode == null) return null;
			}

			// 5) A* algoritmasý
			var data = new NodeData[grid.Width, grid.Height];
			var openSet = new List<Node> { startNode };
			var closed = new HashSet<Node>();

			ref var sd = ref data[startNode.X, startNode.Y];
			sd.GCost = 0;
			sd.HCost = grid.GetManhattanDistance(startNode, new Vector2Int(endNode.X, endNode.Y));
			sd.Visited = true;

			while (openSet.Count > 0)
			{
				// en düþük FCost'lu node'u seç
				var current = openSet[0];
				ref var cd = ref data[current.X, current.Y];
				for (int i = 1; i < openSet.Count; i++)
				{
					var t = openSet[i];
					ref var td = ref data[t.X, t.Y];
					if (td.FCost < cd.FCost ||
						(td.FCost == cd.FCost && td.HCost < cd.HCost))
					{
						current = t;
						cd = ref data[current.X, current.Y];
					}
				}

				openSet.Remove(current);
				closed.Add(current);

				// hedefe ulaþtýysa yolu geriye doðru stack’le çýkar
				if (current == endNode)
				{
					var stack = new Stack<Node>();
					var c = endNode;
					while (c != startNode)
					{
						stack.Push(c);
						c = data[c.X, c.Y].Parent;
					}
					stack.Push(startNode);

					var worldQ = new Queue<Vector2>();
					while (stack.Count > 0)
					{
						var n = stack.Pop();
						worldQ.Enqueue(grid.GetCellCenter(n.X, n.Y));
					}
					return worldQ;
				}

				// komþulara bak
				grid.GetNeighbors(current, neighborBuffer);
				foreach (var nb in neighborBuffer)
				{
					if (!nb.IsWalkable || closed.Contains(nb))
						continue;

					ref var nd = ref data[nb.X, nb.Y];
					int tg = cd.GCost + grid.GetEuclideanDistance(current, nb);

					if (!nd.Visited || tg < nd.GCost)
					{
						nd.GCost = tg;
						nd.HCost = grid.GetManhattanDistance(nb, endGrid);
						nd.Parent = current;
						nd.Visited = true;

						if (!openSet.Contains(nb))
							openSet.Add(nb);
					}
				}
			}

			// yol bulunamadý
			return null;
		}

		/// <summary>
		/// Verilen node'un çevresinde, artan yarýçaplarla en yakýn walkable node'u bulur.
		/// Bulamazsa null döner.
		/// </summary>
		private Node FindClosestWalkable(Node origin, int maxRadius = 10)
		{
			for (int r = 1; r <= maxRadius; r++)
			{
				for (int dx = -r; dx <= r; dx++)
				{
					for (int dy = -r; dy <= r; dy++)
					{
						// sadece çerçeveyi kontrol et (iç kýsým her seferinde daha önce kontrol edildi)
						if (Mathf.Abs(dx) != r && Mathf.Abs(dy) != r)
							continue;

						int nx = origin.X + dx;
						int ny = origin.Y + dy;
						if (nx < 0 || nx >= grid.Width || ny < 0 || ny >= grid.Height)
							continue;

						var node = grid.GetNode(nx, ny);
						if (node != null && node.IsWalkable)
							return node;
					}
				}
			}
			return null;
		}
	}
}
