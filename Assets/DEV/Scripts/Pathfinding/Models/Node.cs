namespace Pathfinding.Models
{
	/// <summary>
	/// Represents a node in a pathfinding system with coordinates and walkability status.
	/// </summary>

	public class Node
    {
		/// <summary>
		/// Gets the X coordinate of the node.
		/// </summary>

		public int X { get; private set; }

		/// <summary>
		/// Gets the Y coordinate of the node.
		/// </summary>

		public int Y { get; private set; }

		/// <summary>
		/// Determines whether the node is walkable.
		/// </summary>

		public bool IsWalkable { get; set; }

		/// <summary>
		/// Initializes a new instance of the Node class with the specified coordinates and walkability status.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		/// <param name="isWalkable">Indicates whether the node is walkable.</param>

		public Node(int x, int y, bool isWalkable)
        {
            X = x;
            Y = y;
            IsWalkable = isWalkable;
        }
    }

}
