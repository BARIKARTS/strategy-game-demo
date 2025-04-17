namespace Pathfinding.Models
{
	public class Node
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public bool IsWalkable { get; set; }

		public Node(int x, int y, bool isWalkable)
		{
			X = x;
			Y = y;
			IsWalkable = isWalkable;
		}
	}

}
