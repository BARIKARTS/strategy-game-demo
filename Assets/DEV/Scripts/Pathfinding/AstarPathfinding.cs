using Pathfinding.Models;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//path bulma islemleri icin zamanin kalirse job entegre et IJobParalel kullan !
//size 1.000.000 grid icin 63ms alindi! 100 coklu objede ayni anda path islemi yapildiginda 524 ms alindi ParalelJob veya Task ayrilarak optimize edilebilir !
//ideal ayarlarda 0 ms altinda bir gecikme oluyor case icin iyi!
namespace Pathfinding
{
	public class AstarPathfinding
	{
		private CustomGrid grid;


		public AstarPathfinding(CustomGrid grid)
		{
			this.grid = grid;
		}


		///dongu icerisinde cok fazla class yapisindan obje yenileniyor. GC icin sikinti !
		public Queue<Node> FindPath(Vector2 startWorld, Vector2 endWorld)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			Vector2Int startGrid = grid.WorldToGrid(startWorld);
			Vector2Int endGrid = grid.WorldToGrid(endWorld);
			Node startNode = grid.GetNode(startGrid.x, startGrid.y);
			Node endNode = grid.GetNode(endGrid.x, endGrid.y);

			if (startNode == null || endNode == null || !startNode.IsWalkable || !endNode.IsWalkable)
				return null;

			int width = grid.Width;
			int height = grid.Height;
			NodeData[,] nodeData = new NodeData[width, height];

			List<Node> openSet = new List<Node> { startNode };
			HashSet<Node> closedSet = new HashSet<Node>();
			List<Node> neighbors = new List<Node>();

			nodeData[startNode.X, startNode.Y].GCost = 0;
			nodeData[startNode.X, startNode.Y].HCost = GetDistance(startNode, endGrid);
			nodeData[startNode.X, startNode.Y].Visited = true;

			while (openSet.Count > 0)
			{
				Node current = openSet[0];
				NodeData currentData = nodeData[current.X, current.Y];

				for (int i = 1; i < openSet.Count; i++)
				{
					Node testNode = openSet[i];
					ref NodeData testData = ref nodeData[testNode.X, testNode.Y];

					if (testData.FCost < currentData.FCost ||
						(testData.FCost == currentData.FCost && testData.HCost < currentData.HCost))
					{
						current = testNode;
						currentData = testData;
					}
				}

				openSet.Remove(current);
				closedSet.Add(current);

				if (current == endNode)
				{
					stopwatch.Stop();
					UnityEngine.Debug.Log($"Path bulundu, süre: {stopwatch.Elapsed} ");
					return RetracePath(startNode, endNode, nodeData);
				}

				GetNeighbors(current, ref neighbors);
				for (int i = 0; i < neighbors.Count; i++)
				{
					Node neighbor = neighbors[i];
					if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
						continue;

					ref NodeData neighborData = ref nodeData[neighbor.X, neighbor.Y];
					 int tentativeG = currentData.GCost + GetDistance(current, neighbor);

					if (!neighborData.Visited || tentativeG < neighborData.GCost)
					{
						neighborData.GCost = tentativeG;
						neighborData.HCost = GetDistance(neighbor, endGrid);
						neighborData.Parent = current;
						neighborData.Visited = true;

						if (!openSet.Contains(neighbor))
							openSet.Add(neighbor);
					}
				}
			}

			return null;
		}

		//grid icerisine tasi burada durmasi mantiksiz!
		private int GetDistance(Node a, Node b)
		{
			int dstX = Mathf.Abs(a.X - b.X);
			int dstY = Mathf.Abs(a.Y - b.Y);
			return (int)(Mathf.Sqrt(dstX * dstX + dstY * dstY) * 10);
		}


		//grid icerisine tasi burada durmasi mantiksiz!
		private int GetDistance(Node a, Vector2Int b)
		{
			int dstX = Mathf.Abs(a.X - b.x);
			int dstY = Mathf.Abs(a.Y - b.y);
			return dstX + dstY;
		}

		private void GetNeighbors(Node node, ref List<Node> neighbors)
		{
			neighbors?.Clear();
			int x = node.X;
			int y = node.Y;
			Node currentNeighbor;
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i == 0 && j == 0) continue;
					currentNeighbor = grid.GetNode(x + i, y + j);
					if (currentNeighbor != null)
						neighbors.Add(currentNeighbor);
				}
			}
		}

		//stack olarak alip queue donusumu mantiksiz.
		private Queue<Node> RetracePath(Node startNode, Node endNode, NodeData[,] nodeData)
		{
			Stack<Node> stack = new Stack<Node>();
			Node current = endNode;

			while (current != startNode)
			{
				stack.Push(current);
				current = nodeData[current.X, current.Y].Parent;
			}

			stack.Push(startNode);

			Queue<Node> pathQueue = new Queue<Node>();
			while (stack.Count > 0)
			{
				pathQueue.Enqueue(stack.Pop());
			}

			return pathQueue;
		}


	}
}