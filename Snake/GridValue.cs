namespace Snake
{
    // Enum representing the possible values in each cell of the game grid
    public enum GridValue
    {
        Empty,    // Cell is empty and contains no snake or food
        Snake,    // Cell contains a part of the snake's body
        Food,     // Cell contains food that the snake can eat
        Outside   // Represents an out-of-bounds cell (used for boundary checks)
    }
}
