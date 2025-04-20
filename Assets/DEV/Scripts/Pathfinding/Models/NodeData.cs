namespace Pathfinding.Models
{
	/// <summary>
	/// Holds A* state for each node: costs, parent and visitation.
	/// </summary>
	public struct NodeData
	{
		public int GCost;
		public int HCost;
		public int FCost => GCost + HCost;
		public Node Parent;
		public bool Visited;
	}

}