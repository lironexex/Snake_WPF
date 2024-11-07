using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Snake
{
    public class GameState
    {
        // Properties for grid dimensions, snake direction, score, and game state
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }       // 2D array representing the game grid
        public Direction Dir { get; private set; }  // Current direction of snake movement
        public int Score { get; private set; }      // Player's score
        public bool GameOver { get; private set; }  // Flag indicating if the game is over

        // Linked lists for direction changes and snake positions
        private readonly LinkedList<Direction> dirChanges = new LinkedList<Direction>();
        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();

        // Random instance for placing food at random positions
        private readonly Random random = new Random();

        // Constructor to initialize game state with grid dimensions
        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Right;  // Initial snake direction

            AddSnake(); // Set up initial snake position
            AddFood();  // Add first food item on the grid
        }

        // Adds the initial snake body to the grid, centered horizontally
        private void AddSnake()
        {
            int r = Rows / 2;  // Start in the middle row

            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;   // Set grid cells as part of the snake
                snakePositions.AddFirst(new Position(r, c));  // Add to snake positions list
            }
        }

        // Yields all empty positions in the grid, used for food placement
        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)  // Only yield empty cells
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        // Adds food to a random empty position on the grid
        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0)
            {
                return;  // No empty spaces left
            }
            Position pos = empty[random.Next(empty.Count)];  // Pick a random empty position
            Grid[pos.Row, pos.Col] = GridValue.Food;         // Place food on the grid
        }

        // Gets the current head position of the snake
        public Position HeadPosition()
        {
            return snakePositions.First.Value;
        }

        // Gets the current tail position of the snake
        public Position TailPosition()
        {
            return snakePositions.Last.Value;
        }

        // Returns all positions occupied by the snake
        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        // Adds a new head position for the snake, updating the grid and positions list
        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        // Removes the tail position of the snake, updating the grid and positions list
        private void RemoveTail()
        {
            Position tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        // Gets the last recorded direction change, or current direction if no changes are queued
        private Direction GetLastDirection()
        {
            if (dirChanges.Count == 0)
            {
                return Dir;
            }

            return dirChanges.Last.Value;
        }

        // Checks if a new direction change is valid (not reversing direction)
        private bool CanChangeDirection(Direction newDir)
        {
            if (dirChanges.Count == 2)
            {
                return false;  // Limit direction changes to 2 per move
            }

            Direction lastDir = GetLastDirection();
            return newDir != lastDir && newDir != lastDir.Opposite();  // Prevent reversing
        }

        // Adds a direction change to the queue if valid
        public void ChangeDirection(Direction dir)
        {
            if (CanChangeDirection(dir))
            {
                dirChanges.AddLast(dir);
            }
        }

        // Checks if a given position is outside the grid boundaries
        private bool OutsideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }

        // Determines what the snake will hit at the new head position
        private GridValue WillHit(Position newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }

            if (newHeadPos == TailPosition())
            {
                return GridValue.Empty;  // Allow movement to current tail position
            }

            return Grid[newHeadPos.Row, newHeadPos.Col];
        }

        // Moves the snake in the current direction and handles collisions or food consumption
        public void Move()
        {
            if (dirChanges.Count > 0)
            {
                Dir = dirChanges.First.Value;  // Update direction
                dirChanges.RemoveFirst();
            }

            Position newHeadPos = HeadPosition().Translate(Dir);  // New head position based on direction
            GridValue hit = WillHit(newHeadPos);  // Check what the snake will hit

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GameOver = true;  // End game if snake hits boundary or itself
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();         // Move forward by adding new head and removing tail
                AddHead(newHeadPos);
            }
            else if (hit == GridValue.Food)
            {
                AddHead(newHeadPos);  // Consume food, increase score, add new food
                Score++;
                AddFood();
            }
        }
    }
}
