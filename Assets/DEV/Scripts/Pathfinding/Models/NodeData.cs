namespace Pathfinding.Models
{
	public struct NodeData
	{
		public int GCost;
		public int HCost;
		public int FCost => GCost + HCost;
		public Node Parent;
		public bool Visited;
	}

}