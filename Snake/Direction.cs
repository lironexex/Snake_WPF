namespace Snake
{
    public class Direction
    {
        // Predefined directions with row and column offsets for Left, Right, Up, and Down
        public readonly static Direction Left = new Direction(0, -1);   // Move left: no row change, column decreases
        public readonly static Direction Right = new Direction(0, 1);   // Move right: no row change, column increases
        public readonly static Direction Up = new Direction(-1, 0);     // Move up: row decreases, no column change
        public readonly static Direction Down = new Direction(1, 0);    // Move down: row increases, no column change

        // Properties to hold row and column offsets for the direction
        public int RowOffset { get; }
        public int ColOffset { get; }

        // Private constructor to initialize a Direction with specific row and column offsets
        private Direction(int rowOffset, int colOffset)
        {
            RowOffset = rowOffset;
            ColOffset = colOffset;
        }

        // Method to get the opposite direction by inverting row and column offsets
        public Direction Opposite()
        {
            return new Direction(-RowOffset, -ColOffset);
        }

        // Override Equals method to compare directions by row and column offsets
        public override bool Equals(object obj)
        {
            return obj is Direction direction &&
                   RowOffset == direction.RowOffset &&
                   ColOffset == direction.ColOffset;
        }

        // Override GetHashCode to generate a unique hash code for the direction based on row and column offsets
        public override int GetHashCode()
        {
            return HashCode.Combine(RowOffset, ColOffset);
        }

        // Overload the == operator to compare two Direction instances
        public static bool operator ==(Direction left, Direction right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        // Overload the != operator to compare two Direction instances
        public static bool operator !=(Direction left, Direction right)
        {
            return !(left == right);
        }
    }
}
