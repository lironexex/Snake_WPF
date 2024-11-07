using System;
using System.IO.Packaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake
{
    // Static class to load and store image resources for the game
    public static class Images
    {
        // Image representing an empty cell in the grid
        public readonly static ImageSource Empty = LoadImage("Empty.png");

        // Image representing a part of the snake's body (with a secrect message)
        public readonly static ImageSource Body = LoadImage("Body.png");

        // Image representing the snake's head
        public readonly static ImageSource Head = LoadImage("Head.png");

        // Image representing food that the snake can eat
        public readonly static ImageSource Food = LoadImage("Food.png");

        // Image representing a part of the snake's body when the game is over
        public readonly static ImageSource DeadBody = LoadImage("DeadBody.png");

        // Image representing the snake's head when the game is over
        public readonly static ImageSource DeadHead = LoadImage("DeadHead.png");

        // Private method to load an image from the Assets folder
        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.Relative));
        }
    }
}
