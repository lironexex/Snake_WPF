namespace Snake
{
    // Represents a position in the grid with row and column coordinates
    public class Position
    {
        // Row index of the position
        public int Row { get; }

        // Column index of the position
        public int Col { get; }

        // Constructor to initialize a position with specific row and column values
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        // Translates the current position in the direction specified by the Direction object
        public Position Translate(Direction dir)
        {
            return new Position(Row + dir.RowOffset, Col + dir.ColOffset);
        }

        // Override of Equals to check equality based on row and column values
        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Col == position.Col;
        }

        // Override of GetHashCode to generate a unique hash code based on row and column
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        // Overload of the == operator to compare two Position instances
        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        // Overload of the != operator to compare two Position instances
        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
