using Pathfinding.Models;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

//path bulma islemleri icin zamanin kalirse job entegre et IJobParalel kullan !
//size 1.000.000 grid icin 63ms alindi! 100 coklu objede ayni anda path islemi yapildiginda 524 ms alindi ParalelJob veya Task ayrilarak optimize edilebilir !
//ideal ayarlarda 0 ms altinda bir gecikme oluyor case icin iyi!
namespace Pathfinding
{
	public class AstarPathfinding
	{
		private readonly CustomGrid grid;
		private readonly List<Node> neighborBuffer = new List<Node>(8);

		public AstarPathfinding(CustomGrid grid) => this.grid = grid;

		public Queue<Vector2> FindPath(Vector2 startWorld, Vector2 endWorld)
		{
			var startGrid = grid.WorldToGrid(startWorld);
			var endGrid = grid.WorldToGrid(endWorld);

			startGrid.x = Mathf.Clamp(startGrid.x, 0, grid.Width - 1);
			startGrid.y = Mathf.Clamp(startGrid.y, 0, grid.Height - 1);
			endGrid.x = Mathf.Clamp(endGrid.x, 0, grid.Width - 1);
			endGrid.y = Mathf.Clamp(endGrid.y, 0, grid.Height - 1);

			var startNode = grid.GetNode(startGrid.x, startGrid.y);
			var endNode = grid.GetNode(endGrid.x, endGrid.y);
			if (startNode == null || endNode == null || !startNode.IsWalkable || !endNode.IsWalkable) return null;

			var data = new NodeData[grid.Width, grid.Height];
			var openSet = new List<Node> { startNode };
			var closed = new HashSet<Node>();

			ref var sd = ref data[startNode.X, startNode.Y];
			sd.GCost = 0; sd.HCost = grid.GetManhattanDistance(startNode, endGrid); sd.Visited = true;

			while (openSet.Count > 0)
			{
				var current = openSet[0]; ref var cd = ref data[current.X, current.Y];
				for (int i = 1; i < openSet.Count; i++)
				{
					var t = openSet[i]; ref var td = ref data[t.X, t.Y];
					if (td.FCost < cd.FCost || (td.FCost == cd.FCost && td.HCost < cd.HCost))
					{
						current = t; cd = ref data[current.X, current.Y];
					}
				}

				openSet.Remove(current); closed.Add(current);
				if (current == endNode)
				{
					var stack = new Stack<Node>(); var c = endNode;
					while (c != startNode) { stack.Push(c); c = data[c.X, c.Y].Parent; }
					stack.Push(startNode);
					var worldQ = new Queue<Vector2>();
					while (stack.Count > 0) { var n = stack.Pop(); worldQ.Enqueue(grid.GetCellCenter(n.X, n.Y)); }
					return worldQ;
				}

				grid.GetNeighbors(current, neighborBuffer);
				foreach (var nb in neighborBuffer)
				{
					if (!nb.IsWalkable || closed.Contains(nb)) continue;
					ref var nd = ref data[nb.X, nb.Y];
					int tg = cd.GCost + grid.GetEuclideanDistance(current, nb);
					if (!nd.Visited || tg < nd.GCost)
					{
						nd.GCost = tg; nd.HCost = grid.GetManhattanDistance(nb, endGrid);
						nd.Parent = current; nd.Visited = true;
						if (!openSet.Contains(nb)) openSet.Add(nb);
					}
				}
			}
			return null;
		}
	}
}