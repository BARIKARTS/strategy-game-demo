namespace Pathfinding.Models
{
	/// <summary>
	/// A struct representing data for a node in a pathfinding algorithm.
	/// Stores cost values, parent node reference, and visitation status.
	/// </summary>
	public struct NodeData
	{
		/// <summary>
		/// The cost of the path from the start node to this node.
		/// </summary>
		public int GCost;

		/// <summary>
		/// The estimated cost from this node to the target node (heuristic cost).
		/// </summary>
		public int HCost;

		/// <summary>
		/// The total cost of the node, calculated as the sum of GCost and HCost.
		/// </summary>
		public int FCost => GCost + HCost;

		/// <summary>
		/// The parent node in the pathfinding tree, used to reconstruct the path.
		/// </summary>
		public Node Parent;

		/// <summary>
		/// Indicates whether the node has been visited during pathfinding.
		/// </summary>
		public bool Visited;
	}

}