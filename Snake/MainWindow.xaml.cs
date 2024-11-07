using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml made by Liron Haber
    /// </summary>
    public partial class MainWindow : Window
    {
        // Maps grid values to corresponding images for display
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.Body },
            { GridValue.Food, Images.Food },
        };

        // Maps each direction to a rotation angle for snake head display
        private readonly Dictionary<Direction, int> dirToRotation = new()
        {
            { Direction.Up, 0 },
            { Direction.Right, 90 },
            { Direction.Down, 180 },
            { Direction.Left, 270 }
        };

        // Grid dimensions
        private readonly int rows = 15, cols = 15; // ****** You can change these!!! ******
        private readonly Image[,] gridImages;       // 2D array of images representing each cell in the grid
        private GameState gameState;                // Current game state object
        private bool gameRunning;                   // Flag indicating if the game is currently running

        // Constructor to initialize components and set up the grid
        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();               // Set up grid with images
            gameState = new GameState(rows, cols);  // Initialize game state
        }

        // Main game execution: countdown, gameplay, and game over handling
        private async Task RunGame()
        {
            Draw();                       // Initial draw of the grid and snake
            await ShowCountDown();        // Show countdown before starting the game
            Overlay.Visibility = Visibility.Hidden; // Hide overlay when game starts
            await GameLoop();             // Run main game loop
            await ShowGameOver();         // Show game over screen when loop ends
            gameState = new GameState(rows, cols); // Reset game state for next game
        }

        // Handles keyboard input to start game or prevent interaction during overlay
        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;  // Prevent input if overlay is visible
            }

            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }

        // Event handlers for each arrow button to change snake direction
        private async void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
            }
            gameState.ChangeDirection(Direction.Up);
        }

        private async void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
            }
            gameState.ChangeDirection(Direction.Down);
        }

        private async void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
            }
            gameState.ChangeDirection(Direction.Left);
        }

        private async void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
            }
            gameState.ChangeDirection(Direction.Right);
        }

        // Handles keyboard arrow keys to change snake direction while game is running
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return; // Ignore input if game is over
            }

            // Map keyboard arrow keys to snake direction changes
            switch (e.Key)
            {
                case Key.Left:
                    gameState.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    gameState.ChangeDirection(Direction.Right);
                    break;
                case Key.Up:
                    gameState.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    gameState.ChangeDirection(Direction.Down);
                    break;
            }
        }

        // Main game loop that moves the snake and redraws the grid at set intervals
        private async Task GameLoop()
        {
            while (!gameState.GameOver)
            {
                await Task.Delay(200);    // Delay between moves
                gameState.Move();         // Update game state
                Draw();                   // Redraw the grid and snake
            }
        }

        // Sets up the game grid with images, and adjusts grid dimensions
        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            // Adjusts grid width to maintain aspect ratio if rows and columns differ
            GameGrid.Width = GameGrid.Height * (cols / (double)rows);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty,            // Initialize cell with empty image
                        RenderTransformOrigin = new Point(0.5, 0.5) // Center rotation point
                    };

                    images[r, c] = image;
                    GameGrid.Children.Add(image);        // Add image to the grid UI
                }
            }

            return images;
        }

        // Draws the entire grid, updates the snake head, and shows current score
        private void Draw()
        {
            DrawGrid();                          // Draws grid cells based on their values
            DrawSnakeHead();                     // Draws and rotates the snake head
            ScoreText.Text = $"SCORE {gameState.Score}";  // Updates score display
        }

        // Redraws each cell in the grid based on the game state
        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridVal = gameState.Grid[r, c]; // Get cell value
                    gridImages[r, c].Source = gridValToImage[gridVal]; // Set cell image
                    gridImages[r, c].RenderTransform = Transform.Identity; // Reset any rotation
                }
            }
        }

        // Draws the snake's head with rotation based on current direction
        private void DrawSnakeHead()
        {
            Position headPos = gameState.HeadPosition();
            Image image = gridImages[headPos.Row, headPos.Col];
            image.Source = Images.Head;

            int rotation = dirToRotation[gameState.Dir];  // Get rotation for current direction
            image.RenderTransform = new RotateTransform(rotation); // Apply rotation to head image
        }

        // Draws the "dead" state of the snake cell by cell in animation
        private async Task DrawDeadSnake()
        {
            List<Position> positions = new List<Position>(gameState.SnakePositions());

            for (int i = 0; i < positions.Count; i++)
            {
                Position pos = positions[i];
                ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody;
                gridImages[pos.Row, pos.Col].Source = source;  // Set dead image
                await Task.Delay(50);                         // Delay for animation effect
            }
        }

        // Displays a countdown before the game starts
        private async Task ShowCountDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                OverlayText.Text = i.ToString();  // Show countdown number
                await Task.Delay(500);            // Half-second delay for each count
            }
        }

        // Shows the game over screen and prompts to restart
        private async Task ShowGameOver()
        {
            await DrawDeadSnake();                       // Animate dead snake
            await Task.Delay(1000);                      // Pause before showing overlay
            Overlay.Visibility = Visibility.Visible;     // Display overlay
            OverlayText.Text = "PRESS ANY KEY TO TRY AGAIN"; // Prompt for retry
        }
    }
}
