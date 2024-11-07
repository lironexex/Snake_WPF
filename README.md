Welcome to Snake_WPF, a modern take on the classic Snake game, developed using C# and Windows Presentation Foundation (WPF) with .NET 8.
This project offers an engaging gaming experience with intuitive controls and a sleek interface.

Features
Responsive Controls: Navigate the snake using keyboard arrow keys or on-screen buttons.
Dynamic Gameplay: The snake grows and accelerates as it consumes food, increasing the challenge.
Real-time Scoring: Keep track of your score as you play.
Game Over Detection: The game ends upon collision with the snake's body or the grid boundaries.
User-Friendly Interface: Clean design with responsive elements for various screen sizes.
Prerequisites
.NET 8 SDK: Ensure that .NET 8 SDK is installed on your machine. Download it from the official .NET website.

Getting Started
1. Clone the Repository:
git clone https://github.com/lironexex/Snake_WPF.git

2. Navigate to the Project Directory:
cd Snake_WPF

3. Build the Project:
   dotnet build
   
4. Run the Application:
   dotnet run

How to Play
Start the Game: Launch the application; the game starts immediately.
Control the Snake:
Keyboard: Use the arrow keys to change the snake's direction.
On-Screen Buttons: Click the directional buttons to navigate the snake.
Objective: Guide the snake to eat the food that appears on the grid. Each piece of food consumed increases the snake's length and speed.
Game Over: The game ends if the snake collides with its own body or the grid boundaries. Press any key to restart.
Project Structure
MainWindow.xaml: Defines the UI layout, including the game grid and control buttons.
MainWindow.xaml.cs: Contains the main logic for game initialization, input handling, and rendering.
GameState.cs: Manages the game's state, including the snake's position, direction, and collision detection.
Direction.cs: Represents possible movement directions and related utility methods.
Position.cs: Defines the coordinates within the game grid.
Images.cs: Handles the loading of image assets used in the game.
Customization
Grid Size: Modify the rows and cols variables in MainWindow.xaml.cs to change the grid dimensions.
Game Speed: Adjust the delay interval in the GameLoop method to control the snake's speed.
Assets: Replace images in the Assets folder to customize the appearance of the snake, food, and other elements.

Contributions are welcome! 

To contribute:
1. Fork the Repository: Click the "Fork" button at the top right of this page.
2. Create a New Branch:
git checkout -b feature/your-feature-name

3. Commit Your Changes:
git commit -m "Add your feature"

4. Push to Your Branch:
git push origin feature/your-feature-name

Acknowledgments
WPF Documentation: Microsoft WPF Documentation
.NET 8: Official .NET 8 Documentation
Enjoy playing Snake_WPF! Feel free to explore, modify, and enhance the game. Your contributions and feedback are highly appreciated.
