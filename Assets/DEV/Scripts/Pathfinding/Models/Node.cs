namespace Pathfinding.Models
{
	 /// <summary>
    /// Represents a cell in the grid with coordinates and walkability.
    /// </summary>
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
